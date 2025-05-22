using System.Collections.Generic;

namespace RealEstateAnalysis.Domain.Settings
{
    public class LocationAnalysisSettings
    {
        public City City { get; set; }

        public Neighborhood Neighborhood { get; set; }
    }

    #region Children Classes

    public class City
    {
        public int HighestAcceptableCrimeIndex { get; set; }

        public decimal HouseholdIncomeAnnualGrowthRate { get; set; }

        public decimal HouseholdIncomeGrowthRateMultiplier { get; set; }

        public decimal MedianHouseOrCondoValueAnnualGrowthRate { get; set; }

        public List<PopulationGrowthRate> PopulationGrowthRates { get; set; }

        public List<JobsGrowthRate> JobsGrowthRates { get; set; }

        public int CityDataPopulationYearStart { get; set; }

        public int GooglePopulationYearStart { get; set; }

        public int MedianHouseholdIncomeYearStart { get; set; }

        public int MedianHouseOrCondoValueYearStart { get; set; }
    }

    public class DecimalRange
    {
        public decimal From { get; set; }

        public decimal To { get; set; }

        public bool IsWithinRange(decimal value) =>
            From <= value && value <= To;
    }

    public class Neighborhood
    {
        public decimal HighestAcceptableCityToNeighborhoodUnemploymentRateDifference { get; set; }

        public int HighestAcceptableEthnicMixLargestSlicePercent { get; set; }

        public decimal HighestAcceptablePovertyRate { get; set; }

        public DecimalRange MedianContractRentRange { get; set; }

        public DecimalRange MedianHouseholdIncomeRange { get; set; }
    }

    public class PopulationGrowthRate
    {
        public int PopulationFrom { get; set; }

        public long PopulationTo { get; set; }

        public decimal AnnualMultiplier { get; set; }
    }

    public class JobsGrowthRate
    {
        public int PopulationFrom { get; set; }

        public long PopulationTo { get; set; }

        public decimal GrowthRate { get; set; }
    }

    #endregion Children Classes
}