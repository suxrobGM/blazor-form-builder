using Company.Project.Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using FormBuilder.API.ModelConfigurations;

namespace FormBuilder.API.Data;

public class ApplicationDbContext : DbContext
{
    private readonly ApplicationDbContextOptions _dbContextOptions;
    
    public ApplicationDbContext(ApplicationDbContextOptions dbContextOptions)
    {
        _dbContextOptions = dbContextOptions;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (string.IsNullOrEmpty(_dbContextOptions.ConnectionString))
        {
            throw new ArgumentException("The connection string is not specified");
        }
        
        optionsBuilder.ConfigurePostgreSql(_dbContextOptions.ConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new FormConfiguration());
        modelBuilder.ApplyConfiguration(new LovMasterConfiguration());  
    }
}
