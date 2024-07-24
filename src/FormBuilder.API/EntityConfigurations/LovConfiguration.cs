using FormBuilder.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormBuilder.API.ModelConfigurations;

public class LovConfiguration : IEntityTypeConfiguration<Lov>
{
    public void Configure(EntityTypeBuilder<Lov> builder)
    {
        builder.ToTable("LovMaster");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.ListId);
        builder.Property(e => e.ListValue);
    }
}
