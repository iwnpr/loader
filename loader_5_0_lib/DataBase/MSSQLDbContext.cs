using loader_5_0_lib.NotificationEF;
using Microsoft.EntityFrameworkCore;

namespace loader_5_0_lib.DataBase
{
    public class MSSQLDbContext : DbContext
    {
        public DbSet<NotificationOfReceiptEF> NotificationOfReceipt { get; set; }
        public DbSet<NotificationOfReceiptErrorEF> NotificationOfReceiptErrors { get; set; }
        public DbSet<NotificationOfAcceptanceEF> NotificationOfAcceptance { get; set; }
        public DbSet<NotificationOfAcceptanceErrorEF> NotificationOfAcceptanceErrors { get; set; }
        public MSSQLDbContext(DbContextOptions<MSSQLDbContext> options)
            : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new NotificationOfReceiptEntityConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationOfReceiptErrorEntityConfiguration());

            modelBuilder.ApplyConfiguration(new NotificationOfAcceptanceEntityConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationOfAcceptanceErrorEntityConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationOfAcceptanceWarningMessageConfiguration());
        }
    }
}