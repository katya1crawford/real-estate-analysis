namespace RealEstateAnalysis.Domain.DTOs.Writes.LocationAnalysis
{
    public class WriteCity
    {
        public string CityName { get; set; }

        public int PopulationInYearStart { get; set; }

        public int PopulationYearStart { get; set; }

        public int PopulationInYearEnd { get; set; }

        public decimal MedianHouseholdIncomeInYearStart { get; set; }

        public decimal MedianHouseholdIncomeInYearEnd { get; set; }

        public decimal MedianHouseOrCondoValueInYearStart { get; set; }

        public decimal MedianHouseOrCondoValueInYearEnd { get; set; }

        public decimal CrimeIndexInYearStart { get; set; }

        public decimal CrimeIndexInYearEnd { get; set; }

        public int CrimeIndexYearStart { get; set; }

        public int CrimeIndexYearEnd { get; set; }

        public decimal RecentYearJobsGrowthRate { get; set; }

        public int PopulationYearEnd { get; set; }

        public int MedianHouseholdIncomeYearStart { get; set; }

        public int MedianHouseholdIncomeYearEnd { get; set; }

        public int MedianHouseOrCondoValueYearStart { get; set; }

        public int MedianHouseOrCondoValueYearEnd { get; set; }

        public int NumberOfJobsAdded { get; set; }

        public long StateId { get; set; }
    }
}