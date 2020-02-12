using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Domain;

namespace TMS.Infra.Data.Mappings
{
    public class TaskMapping : IEntityTypeConfiguration<TaskData>
    {
        public void Configure(EntityTypeBuilder<TaskData> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
               .HasColumnType("varchar(200)")
               .IsRequired();

            builder.Property(e => e.Description)
                .HasColumnType("varchar(max)");

            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.CascadeMode);

            builder.ToTable("Tasks");
        }
    }
}
