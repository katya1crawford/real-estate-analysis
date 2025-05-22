using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RealEstateAnalysis.Data.Entities;
using RealEstateAnalysis.Data.Entities.Lookups;
using LocationAnalysis = RealEstateAnalysis.Data.Entities.LocationAnalysis;
using RentalProperty = RealEstateAnalysis.Data.Entities.RentalProperty;
using Reonomy = RealEstateAnalysis.Data.Entities.Reonomy;

namespace RealEstateAnalysis.Domain
{
    public class EFDbContext : IdentityDbContext<User>
    {
        public EFDbContext(DbContextOptions<EFDbContext> options) : base(options)
        {
        }

        public DbSet<ClosingCostType> ClosingCostTypes { get; set; }

        public DbSet<ErrorLog> ErrorsLog { get; set; }

        public DbSet<ExteriorRepairExpenseType> ExteriorRepairExpenseTypes { get; set; }

        public DbSet<GeneralRepairExpenseType> GeneralRepairExpenseTypes { get; set; }

        public DbSet<InteriorRepairExpenseType> InteriorRepairExpenseTypes { get; set; }

        public DbSet<LocationAnalysis.City> LocationAnalysis_Cities { get; set; }

        public DbSet<LocationAnalysis.CityDataStateUrl> LocationAnalysis_CityDataStateUrls { get; set; }

        public DbSet<LocationAnalysis.Neighborhood> LocationAnalysis_Neighborhoods { get; set; }

        public DbSet<MonetaryTransaction> MonetaryTransactions { get; set; }

        public DbSet<OperatingExpenseType> OperatingExpenseTypes { get; set; }

        public DbSet<PropertyType> PropertyTypes { get; set; }

        public DbSet<PropertyStatus> PropertyStatuses { get; set; }

        public DbSet<RentalProperty.ClosingCost> RentalProperty_ClosingCosts { get; set; }

        public DbSet<RentalProperty.ExteriorRepairExpense> RentalProperty_ExteriorRepairExpenses { get; set; }

        public DbSet<RentalProperty.File> RentalProperty_Files { get; set; }

        public DbSet<RentalProperty.FileContent> RentalProperty_FilesContent { get; set; }

        public DbSet<RentalProperty.GalleryImage> RentalProperty_GalleryImages { get; set; }

        public DbSet<RentalProperty.GeneralRepairExpense> RentalProperty_GeneralRepairExpenses { get; set; }

        public DbSet<RentalProperty.InteriorRepairExpense> RentalProperty_InteriorRepairExpenses { get; set; }

        public DbSet<RentalProperty.AnnualOperatingExpense> RentalProperty_OperatingExpenses { get; set; }

        public DbSet<RentalProperty.Property> RentalProperty_Properties { get; set; }

        public DbSet<RentalProperty.RentRollItem> RentalProperty_RentRollItems { get; set; }

        public DbSet<RentalProperty.UnitGroup> RentalProperty_UnitGroups { get; set; }

        public DbSet<Reonomy.SoldProperty> Reonomy_SoldProperties { get; set; }

        public DbSet<State> States { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedDate = DateTime.Now;
                }
                else
                {
                    ((BaseEntity)entity.Entity).ModifiedDate = DateTime.Now;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ClosingCostTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ErrorLogConfiguration());
            modelBuilder.ApplyConfiguration(new ExteriorRepairExpenseTypeConfiguration());
            modelBuilder.ApplyConfiguration(new GeneralRepairExpenseTypeConfiguration());
            modelBuilder.ApplyConfiguration(new InteriorRepairExpenseTypeConfiguration());
            modelBuilder.ApplyConfiguration(new LocationAnalysis.CityDataStateUrlConfiguration());
            modelBuilder.ApplyConfiguration(new LocationAnalysis.CityConfiguration());
            modelBuilder.ApplyConfiguration(new LocationAnalysis.NeighborhoodConfiguration());
            modelBuilder.ApplyConfiguration(new MonetaryTransactionConfiguration());
            modelBuilder.ApplyConfiguration(new OperatingExpenseTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PropertyStatusConfiguration());
            modelBuilder.ApplyConfiguration(new RentalProperty.AnnualOperatingExpenseConfiguration());
            modelBuilder.ApplyConfiguration(new RentalProperty.ClosingCostConfiguration());
            modelBuilder.ApplyConfiguration(new RentalProperty.ExteriorRepairExpenseConfiguration());
            modelBuilder.ApplyConfiguration(new RentalProperty.FileConfiguration());
            modelBuilder.ApplyConfiguration(new RentalProperty.FileContentConfiguration());
            modelBuilder.ApplyConfiguration(new RentalProperty.GalleryImageConfiguration());
            modelBuilder.ApplyConfiguration(new RentalProperty.GeneralRepairExpenseConfiguration());
            modelBuilder.ApplyConfiguration(new RentalProperty.InteriorRepairExpenseConfiguration());
            modelBuilder.ApplyConfiguration(new RentalProperty.PropertyConfiguration());
            modelBuilder.ApplyConfiguration(new RentalProperty.UnitGroupConfiguration());
            modelBuilder.ApplyConfiguration(new RentalProperty.RentRollItemConfiguration());
            modelBuilder.ApplyConfiguration(new Reonomy.SoldPropertyConfiguration());
            modelBuilder.ApplyConfiguration(new StateConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}