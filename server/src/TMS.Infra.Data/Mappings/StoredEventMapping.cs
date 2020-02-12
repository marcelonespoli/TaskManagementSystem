using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TMS.Domain.Core.Events;

namespace TMS.Infra.Data.Mappings
{
    public class StoredEventMapping : IEntityTypeConfiguration<StoredEvent>
    {
        public void Configure(EntityTypeBuilder<StoredEvent> builder)
        {
            builder.Property(p => p.Timestamp)
                .HasColumnName("CreationDate");

            builder.Property(p => p.MessageType)
                .HasColumnName("Action")
                .HasColumnType("varchar(100)");
        }
    }
}
