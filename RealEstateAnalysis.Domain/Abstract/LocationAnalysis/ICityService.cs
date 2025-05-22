using RealEstateAnalysis.Domain.DTOs.Reads.LocationAnalysis;
using RealEstateAnalysis.Domain.DTOs.Writes.LocationAnalysis;

namespace RealEstateAnalysis.Domain.Abstract.LocationAnalysis
{
    public interface ICityService
    {
        Task<ReadCity> AddAsync(WriteCity model);

        Task DeleteAsync(long cityId);

        Task<List<ReadCity>> GetAllAsync();

        Task<ReadCity> GetByIdAsync(long id);

        Task<ReadCity> UpdateAsync(long id, WriteCity model);

        Task HarvestCityData(int minimumPopulationCount);

        Task ToggleIsFavorite(long cityId);
    }
}