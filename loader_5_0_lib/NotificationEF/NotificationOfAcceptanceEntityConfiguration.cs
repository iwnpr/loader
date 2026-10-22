using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace loader_5_0_lib.NotificationEF
{
    public class NotificationOfAcceptanceEntityConfiguration : IEntityTypeConfiguration<NotificationOfAcceptanceEF>
    {
        public void Configure(EntityTypeBuilder<NotificationOfAcceptanceEF> builder)
        {
            builder.ToTable("te_uf_NotificationOfAcceptance");

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