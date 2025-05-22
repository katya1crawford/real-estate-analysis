using RealEstateAnalysis.Data.Entities.LocationAnalysis;

namespace RealEstateAnalysis.Data.Abstract.LocationAnalysis
{
    public interface ICityDataRepository
    {
        Task<List<CityDataStateUrl>> GetAllStatesUrlsAsync();
    }
}