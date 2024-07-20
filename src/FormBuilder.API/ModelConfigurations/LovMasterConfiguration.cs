using FormBuilder.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormBuilder.API.ModelConfigurations;

public class LovMasterConfiguration : IEntityTypeConfiguration<LovMaster>
{
    public void Configure(EntityTypeBuilder<LovMaster> builder)
    {
        builder.ToTable("lov_master");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.ListId).HasColumnName("list_id");
        builder.Property(e => e.ListValue).HasColumnName("list_value");
    }
}
