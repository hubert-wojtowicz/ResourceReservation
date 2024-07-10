using Microsoft.EntityFrameworkCore;
using ReservationApi.Infrastructure.Entities;

namespace ReservationApi.Infrastructure
{
    public class ReservationDbContext : DbContext
    {
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Resource> Resources { get; set; }

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
            modelBuilder.Entity<Reservation>()
                .HasKey(e => e.ReservationId);

            modelBuilder.Entity<Resource>()
                .HasKey(e => e.ResourceId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Resource)
                .WithOne(res => res.Reservation)
                .HasForeignKey<Reservation>(r => r.ReservationId);

            // do not create separate entity for simplicity 
            modelBuilder.Entity<Resource>()
                .Property(e => e.Tags)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList());
        }
    }
}
