using RealEstateAnalysis.Domain.DTOs.Reads.LocationAnalysis;
using RealEstateAnalysis.Domain.DTOs.Writes.LocationAnalysis;

namespace RealEstateAnalysis.Domain.Abstract.LocationAnalysis
{
    public interface INeighborhoodService
    {
        Task<ReadNeighborhood> AddAsync(WriteNeighborhood model);

        Task DeleteAsync(long cityId);

        Task<List<ReadNeighborhood>> GetAllAsync();

        Task<ReadNeighborhood> GetByIdAsync(long id);

        Task<ReadNeighborhood> UpdateAsync(long id, WriteNeighborhood model);
    }
}