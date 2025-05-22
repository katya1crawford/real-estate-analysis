namespace RealEstateAnalysis.Domain.DTOs.Reads.DataScraper
{
    public class ReadCityData
    {
        public ReadCityData(string stateName, string cityName, int populationInYearStart, int populationYearStart, int populationInYearEnd, decimal medianHouseholdIncomeInYearStart,
            decimal medianHouseholdIncomeInYearEnd, decimal medianHouseOrCondoValueInYearStart, decimal medianHouseOrCondoValueInYearEnd, int crimeIndexYearStart,
            int crimeIndexYearEnd, decimal crimeIndexInYearStart, decimal crimeIndexInYearEnd, decimal recentYearJobsGrowthRate, int populationYearEnd,
            int medianHouseholdIncomeYearStart, int medianHouseholdIncomeYearEnd, int medianHouseOrCondoValueYearStart, int medianHouseOrCondoValueYearEnd, int numberOfJobsAdded)
        {
            StateName = stateName;
            CityName = cityName;
            PopulationInYearStart = populationInYearStart;
            PopulationInYearEnd = populationInYearEnd;
            PopulationYearStart = populationYearStart;
            MedianHouseholdIncomeInYearStart = medianHouseholdIncomeInYearStart;
            MedianHouseholdIncomeInYearEnd = medianHouseholdIncomeInYearEnd;
            MedianHouseOrCondoValueInYearStart = medianHouseOrCondoValueInYearStart;
            MedianHouseOrCondoValueInYearEnd = medianHouseOrCondoValueInYearEnd;
            CrimeIndexYearStart = crimeIndexYearStart;
            CrimeIndexYearEnd = crimeIndexYearEnd;
            CrimeIndexInYearStart = crimeIndexInYearStart;
            CrimeIndexInYearEnd = crimeIndexInYearEnd;
            RecentYearJobsGrowthRate = recentYearJobsGrowthRate;
            PopulationYearEnd = populationYearEnd;
            MedianHouseholdIncomeYearEnd = medianHouseholdIncomeYearEnd;
            MedianHouseOrCondoValueYearEnd = medianHouseOrCondoValueYearEnd;
            NumberOfJobsAdded = numberOfJobsAdded;
            MedianHouseholdIncomeYearStart = medianHouseholdIncomeYearStart;
            MedianHouseOrCondoValueYearStart = medianHouseOrCondoValueYearStart;
        }

        public string StateName { get; }

        public string CityName { get; }

        public int PopulationInYearStart { get; }

        public int PopulationInYearEnd { get; }

        public decimal MedianHouseholdIncomeInYearStart { get; }

        public decimal MedianHouseholdIncomeInYearEnd { get; }

        public decimal MedianHouseOrCondoValueInYearStart { get; }

        public decimal MedianHouseOrCondoValueInYearEnd { get; }

        public int CrimeIndexYearStart { get; }

        public int CrimeIndexYearEnd { get; }

        public decimal CrimeIndexInYearStart { get; }

        public decimal CrimeIndexInYearEnd { get; }

        public decimal RecentYearJobsGrowthRate { get; }

        public int PopulationYearStart { get; }

        public int PopulationYearEnd { get; }

        public int MedianHouseholdIncomeYearStart { get; }

        public int MedianHouseholdIncomeYearEnd { get; }

        public int MedianHouseOrCondoValueYearStart { get; }

        public int MedianHouseOrCondoValueYearEnd { get; }

        public int NumberOfJobsAdded { get; }
    }
}