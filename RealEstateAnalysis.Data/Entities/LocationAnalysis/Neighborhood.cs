using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateAnalysis.Data.Entities.Lookups;

namespace RealEstateAnalysis.Data.Entities.LocationAnalysis
{
    public class Neighborhood : BaseEntity
    {
        #region Constructors

        internal Neighborhood()
        {
        }

        public Neighborhood(string neighborhoodName,
            string city,
            State state,
            decimal medianHouseholdIncome,
            decimal medianContractRent,
            decimal cityUnemploymentRate,
            decimal neighborhoodUnemploymentRate,
            decimal neighborhoodPovertyRate,
            decimal ethnicMixLargestSlicePercent,
            int homesMedianDaysOnMarket,
            string userId)
        {
            UserId = userId;

            Update(neighborhoodName: neighborhoodName,
                city: city,
                state: state,
                medianHouseholdIncome: medianHouseholdIncome,
                medianContractRent: medianContractRent,
                cityUnemploymentRate: cityUnemploymentRate,
                neighborhoodUnemploymentRate: neighborhoodUnemploymentRate,
                neighborhoodPovertyRate: neighborhoodPovertyRate,
                ethnicMixLargestSlicePercent: ethnicMixLargestSlicePercent,
                homesMedianDaysOnMarket: homesMedianDaysOnMarket);
        }

        #endregion Constructors

        #region Properties

        public string NeighborhoodName { get; private set; }

        public string City { get; private set; }

        public decimal MedianHouseholdIncome { get; private set; }

        public decimal MedianContractRent { get; private set; }

        public decimal CityUnemploymentRate { get; private set; }

        public decimal NeighborhoodUnemploymentRate { get; private set; }

        public decimal PovertyRate { get; private set; }

        public decimal EthnicMixLargestSlicePercent { get; private set; }

        public int HomesMedianDaysOnMarket { get; private set; }

        internal string UserId { get; private set; }

        internal User User { get; private set; }

        internal long StateId { get; private set; }

        public State State { get; private set; }

        #endregion Properties

        #region Commands

        public void Update(string neighborhoodName,
            string city,
            State state,
            decimal medianHouseholdIncome,
            decimal medianContractRent,
            decimal cityUnemploymentRate,
            decimal neighborhoodUnemploymentRate,
            decimal neighborhoodPovertyRate,
            decimal ethnicMixLargestSlicePercent,
            int homesMedianDaysOnMarket)
        {
            NeighborhoodName = neighborhoodName;
            City = city;
            State = state;
            MedianHouseholdIncome = medianHouseholdIncome;
            MedianContractRent = medianContractRent;
            CityUnemploymentRate = cityUnemploymentRate;
            NeighborhoodUnemploymentRate = neighborhoodUnemploymentRate;
            PovertyRate = neighborhoodPovertyRate;
            EthnicMixLargestSlicePercent = ethnicMixLargestSlicePercent;
            HomesMedianDaysOnMarket = homesMedianDaysOnMarket;
        }

        #endregion Commands
    }

    internal class NeighborhoodConfiguration : IEntityTypeConfiguration<Neighborhood>
    {
        public void Configure(EntityTypeBuilder<Neighborhood> entity)
        {
            //Primary Key
            entity.HasKey(x => x.Id);

            //Properties
            entity.Property(x => x.NeighborhoodName)
                .HasMaxLength(250)
                .IsRequired();

            entity.Property(x => x.City)
                .HasMaxLength(500)
                .IsRequired();

            //Relationships
            entity.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            entity.HasOne(x => x.State)
                .WithMany()
                .HasForeignKey(x => x.StateId)
                .IsRequired();

            //Table & Columns
            entity.ToTable("Neighborhoods", "LocationAnalysis");
        }
    }
}