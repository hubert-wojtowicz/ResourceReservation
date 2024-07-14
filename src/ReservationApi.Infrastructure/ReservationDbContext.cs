using Microsoft.EntityFrameworkCore;
using ReservationApi.Infrastructure.Entities;
using System.Resources;

namespace ReservationApi.Infrastructure
{
    public class ReservationDbContext : DbContext
    {
        public DbSet<ReservationDbEntity> Reservations { get; set; }
        public DbSet<ResourceDbEntity> Resources { get; set; }

        public ReservationDbContext(DbContextOptions<ReservationDbContext> options)
            : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                var connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=LibraryDb; Integrated Security=True;";

                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReservationDbEntity>()
                .ToTable("Reservations");

            modelBuilder.Entity<ResourceDbEntity>()
                .ToTable("Resources");

            modelBuilder.Entity<ReservationDbEntity>()
                .HasKey(e => e.ReservationId);

            modelBuilder.Entity<ResourceDbEntity>()
                .HasKey(e => e.ResourceId);

            modelBuilder.Entity<ReservationDbEntity>()
                .HasIndex(u => u.ResourceId)
                .IsUnique();

            modelBuilder.Entity<ResourceDbEntity>()
                .HasOne(r => r.Reservation)
                .WithOne(res => res.Resource)
                .HasForeignKey<ReservationDbEntity>(res => res.ResourceId)
                .OnDelete(DeleteBehavior.Cascade);

            // do not create separate entity for simplicity 
            modelBuilder.Entity<ResourceDbEntity>()
                .Property(e => e.Tags)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
        }
    }
}
