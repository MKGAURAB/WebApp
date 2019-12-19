using Microsoft.EntityFrameworkCore;

namespace WebApp.EntityModels
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(){}
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options)
            : base(options) {}
        public virtual DbSet<TransactionStatus> TransactionStatus { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<TransactionStatus>(entity =>
            {
                entity.HasKey(e => e.Status);

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .ValueGeneratedNever();

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(3);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("numeric(10, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CurrencyCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.Property(e => e.TransactionId)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
