using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace loader_5_0_lib.NotificationEF
{
    public class NotificationOfAcceptanceWarningMessageConfiguration : IEntityTypeConfiguration<NotificationOfAcceptanceWarningMessageEF>
    {
        public void Configure(EntityTypeBuilder<NotificationOfAcceptanceWarningMessageEF> builder)
        {
            builder.ToTable("te_uf_NotificationOfAcceptanceWarningMessage");

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