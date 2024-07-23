using FormBuilder.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormBuilder.API.ModelConfigurations;

public class LovMasterConfiguration : IEntityTypeConfiguration<LovMaster>
{
    public void Configure(EntityTypeBuilder<LovMaster> builder)
    {
        builder.ToTable("LovMaster");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.ListId);
        builder.Property(e => e.ListValue);
    }
}
