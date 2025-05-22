using RealEstateAnalysis.Data.DTOs;
using RealEstateAnalysis.Data.Entities.Reonomy;

namespace RealEstateAnalysis.Data.Abstract.Reonomy
{
    public interface ISoldPropertyRepository
    {
        Task<List<string>> GetAllReonomySoldPropertySourceIds();

        Task SaveOrUpdateAsync(List<SoldProperty> soldProperties);

        Task<List<SoldProperty>> SearchAsync(SoldPropertiesSearch searchFilters);

        Task<List<SoldProperty>> GetAllWithMissingLatLongAsync();
    }
}