using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace loader_5_0_lib.NotificationEF
{
    public class NotificationOfReceiptEntityConfiguration : IEntityTypeConfiguration<NotificationOfReceiptEF>
    {
        public void Configure(EntityTypeBuilder<NotificationOfReceiptEF> builder)
        {
            builder.ToTable("te_uf_NotificationOfReceipt");

            /*
            builder
                .Ignore(d => d.Specialization)
                .HasOne(d => d.Specialization)
                .WithMany(d => d.Doctors)
                .HasForeignKey(d => d.SpecializationId);

            builder
                .Ignore(d => d.Region)
                .HasOne(d => d.Region)
                .WithMany(d => d.Doctors)
                .HasForeignKey(d => d.RegionId);
            */
        }
    }
}