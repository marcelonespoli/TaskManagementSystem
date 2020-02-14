using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Domain;

namespace TMS.Infra.Data.Mappings
{
    public class SubtaskMapping : IEntityTypeConfiguration<Subtask>
    {
        public void Configure(EntityTypeBuilder<Subtask> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
               .HasColumnType("varchar(250)")
               .IsRequired();

            builder.Property(e => e.Description)
                .HasColumnType("varchar(max)");

            builder.Ignore(e => e.ValidationResult);
            builder.Ignore(e => e.CascadeMode);

            builder.ToTable("Subtasks");

            builder.HasOne(e => e.Task)
                .WithMany(c => c.Subtasks)
                .HasForeignKey(e => e.TaskId);
        }
    }
}
