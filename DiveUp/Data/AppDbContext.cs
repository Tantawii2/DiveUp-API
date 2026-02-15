using Microsoft.EntityFrameworkCore;
using DiveUp.Models;

namespace DiveUp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Agent> Agents => Set<Agent>();
        public DbSet<Boat> Boats => Set<Boat>();
        public DbSet<Excursion> Excursions => Set<Excursion>();
        public DbSet<ExcursionSupplier> ExcursionSuppliers => Set<ExcursionSupplier>();
        public DbSet<Guide> Guides => Set<Guide>();
        public DbSet<Hotel> Hotels => Set<Hotel>();
        public DbSet<HotelDestination> HotelDestinations => Set<HotelDestination>();
        public DbSet<Nationality> Nationalities => Set<Nationality>();
        public DbSet<PriceList> PriceLists => Set<PriceList>();
        public DbSet<Rate> Rates => Set<Rate>();
        public DbSet<Rep> Reps => Set<Rep>();
        public DbSet<TransportationType> TransportationTypes => Set<TransportationType>();
        public DbSet<TransportationSupplier> TransportationSuppliers => Set<TransportationSupplier>();
        public DbSet<TransportationCost> TransportationCosts => Set<TransportationCost>();
        public DbSet<Voucher> Vouchers => Set<Voucher>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Agent: AgentCode must be unique
            modelBuilder.Entity<Agent>()
                .HasIndex(a => a.AgentCode)
                .IsUnique();

            // Excursion -> ExcursionSupplier (optional FK, no cascade delete)
            modelBuilder.Entity<Excursion>()
                .HasOne(e => e.Supplier)
                .WithMany()
                .HasForeignKey(e => e.SupplierId)
                .OnDelete(DeleteBehavior.SetNull);

            // Hotel -> HotelDestination (optional FK)
            modelBuilder.Entity<Hotel>()
                .HasOne(h => h.Destination)
                .WithMany()
                .HasForeignKey(h => h.DestinationId)
                .OnDelete(DeleteBehavior.SetNull);

            // Rep -> Agent (optional FK)
            modelBuilder.Entity<Rep>()
                .HasOne(r => r.Agent)
                .WithMany()
                .HasForeignKey(r => r.AgentId)
                .OnDelete(DeleteBehavior.SetNull);

            // TransportationType -> TransportationSupplier (optional FK)
            modelBuilder.Entity<TransportationType>()
                .HasOne(t => t.Supplier)
                .WithMany()
                .HasForeignKey(t => t.SupplierId)
                .OnDelete(DeleteBehavior.SetNull);

            // TransportationCost -> TransportationType (optional FK)
            modelBuilder.Entity<TransportationCost>()
                .HasOne(tc => tc.Type)
                .WithMany()
                .HasForeignKey(tc => tc.TypeId)
                .OnDelete(DeleteBehavior.SetNull);

            // Voucher -> Rep (optional FK)
            modelBuilder.Entity<Voucher>()
                .HasOne(v => v.Rep)
                .WithMany()
                .HasForeignKey(v => v.RepId)
                .OnDelete(DeleteBehavior.SetNull);

            // Rate: decimal precision
            modelBuilder.Entity<Rate>()
                .Property(r => r.RateValue)
                .HasColumnType("decimal(18,4)");

            // TransportationCost: decimal precision
            modelBuilder.Entity<TransportationCost>()
                .Property(tc => tc.CostValue)
                .HasColumnType("decimal(18,2)");
        }
    }
}
