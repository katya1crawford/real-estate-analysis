namespace RealEstateAnalysis.Domain.DTOs.Reads.LocationAnalysis
{
    public class ReadPopulationGrowthSummary
    {
        public ReadPopulationGrowthSummary(int populationInYearStart, int populationInYearEnd, int populationYearStart, int populationYearEnd)
        {
            PopulationInYearStart = populationInYearStart;
            PopulationInYearEnd = populationInYearEnd;
            PopulationYearStart = populationYearStart;
            PopulationYearEnd = populationYearEnd;
        }

        public int PopulationInYearStart { get; }

        public int PopulationInYearEnd { get; }

        public int PopulationYearStart { get; }

        public int PopulationYearEnd { get; }
    }
}