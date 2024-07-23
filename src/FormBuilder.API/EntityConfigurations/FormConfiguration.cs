using FormBuilder.API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FormBuilder.API.ModelConfigurations;

public class FormConfiguration : IEntityTypeConfiguration<Form>
{
    public void Configure(EntityTypeBuilder<Form> builder)
    {
        builder.ToTable("Forms");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.FormName);
        builder.Property(e => e.FormDesign);
    }
}
