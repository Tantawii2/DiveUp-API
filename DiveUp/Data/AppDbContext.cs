using Microsoft.EntityFrameworkCore;
using DiveUp.Models.SystemOperation.Codes.Functions;

namespace DiveUp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Agent> Agents => Set<Agent>();
        public DbSet<Boat> Boats => Set<Boat>();
        public DbSet<Excursion> Excursions => Set<Excursion>();
        public DbSet<ExcursionSupplier> ExcursionSuppliers => Set<ExcursionSupplier>();
        public DbSet<ExcursionCostSelling> ExcursionCostSellings => Set<ExcursionCostSelling>();
        public DbSet<Guide> Guides => Set<Guide>();
        public DbSet<Hotel> Hotels => Set<Hotel>();
        public DbSet<HotelDestination> HotelDestinations => Set<HotelDestination>();
        public DbSet<Nationality> Nationalities => Set<Nationality>();
        public DbSet<PriceList> PriceLists => Set<PriceList>();
        public DbSet<Rate> Rates => Set<Rate>();
        public DbSet<Rep> Reps => Set<Rep>();
        public DbSet<RepVoucher> RepVouchers => Set<RepVoucher>();
        public DbSet<TransportationType> TransportationTypes => Set<TransportationType>();
        public DbSet<TransportationSupplier> TransportationSuppliers => Set<TransportationSupplier>();
        public DbSet<TransportationCost> TransportationCosts => Set<TransportationCost>();
        public DbSet<Voucher> Vouchers => Set<Voucher>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Agent: AgentCode unique
            modelBuilder.Entity<Agent>().HasIndex(a => a.AgentCode).IsUnique();

            // Agent -> Nationality
            modelBuilder.Entity<Agent>()
                .HasOne(a => a.Nationality).WithMany()
                .HasForeignKey(a => a.NationalityId).OnDelete(DeleteBehavior.SetNull);

            // Rep -> Agent
            modelBuilder.Entity<Rep>()
                .HasOne(r => r.Agent).WithMany()
                .HasForeignKey(r => r.AgentId).OnDelete(DeleteBehavior.SetNull);

            // Excursion -> ExcursionSupplier
            modelBuilder.Entity<Excursion>()
                .HasOne(e => e.Supplier).WithMany()
                .HasForeignKey(e => e.SupplierId).OnDelete(DeleteBehavior.SetNull);

            // Hotel -> HotelDestination
            modelBuilder.Entity<Hotel>()
                .HasOne(h => h.Destination).WithMany()
                .HasForeignKey(h => h.DestinationId).OnDelete(DeleteBehavior.SetNull);

            // TransportationType -> TransportationSupplier
            modelBuilder.Entity<TransportationType>()
                .HasOne(t => t.Supplier).WithMany()
                .HasForeignKey(t => t.SupplierId).OnDelete(DeleteBehavior.SetNull);

            // TransportationCost -> Supplier, CarType, Destination
            modelBuilder.Entity<TransportationCost>()
                .HasOne(tc => tc.Supplier).WithMany()
                .HasForeignKey(tc => tc.SupplierId).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<TransportationCost>()
                .HasOne(tc => tc.CarType).WithMany()
                .HasForeignKey(tc => tc.CarTypeId).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<TransportationCost>()
                .HasOne(tc => tc.Destination).WithMany()
                .HasForeignKey(tc => tc.DestinationId).OnDelete(DeleteBehavior.SetNull);

            // Voucher -> Rep
            modelBuilder.Entity<Voucher>()
                .HasOne(v => v.Rep).WithMany()
                .HasForeignKey(v => v.RepId).OnDelete(DeleteBehavior.SetNull);

            // RepVoucher -> Rep
            modelBuilder.Entity<RepVoucher>()
                .HasOne(rv => rv.Rep).WithMany()
                .HasForeignKey(rv => rv.RepId).OnDelete(DeleteBehavior.SetNull);

            // ExcursionCostSelling FKs
            modelBuilder.Entity<ExcursionCostSelling>()
                .HasOne(e => e.PriceList).WithMany()
                .HasForeignKey(e => e.PriceListId).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<ExcursionCostSelling>()
                .HasOne(e => e.Excursion).WithMany()
                .HasForeignKey(e => e.ExcursionId).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<ExcursionCostSelling>()
                .HasOne(e => e.Destination).WithMany()
                .HasForeignKey(e => e.DestinationId).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<ExcursionCostSelling>()
                .HasOne(e => e.Agent).WithMany()
                .HasForeignKey(e => e.AgentId).OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<ExcursionCostSelling>()
                .HasOne(e => e.Supplier).WithMany()
                .HasForeignKey(e => e.SupplierId).OnDelete(DeleteBehavior.SetNull);

            // Rate precision
            modelBuilder.Entity<Rate>()
                .Property(r => r.RateValue).HasColumnType("decimal(18,4)");

            // TransportationCost precision
            modelBuilder.Entity<TransportationCost>()
                .Property(tc => tc.CostEGP).HasColumnType("decimal(18,2)");
        }
    }
}
