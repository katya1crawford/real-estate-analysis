using RealEstateAnalysis.Domain.DTOs.Reads.DataScraper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Domain.Abstract
{
    public interface IDataScraperService
    {
        Task<ReadCityData> GetCityData(string cityDataUrl);
        Task<List<string>> GetCityDataCitiesUrls(int minimumPopulationCount);
    }
}