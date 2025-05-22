using RealEstateAnalysis.Data.Entities.LocationAnalysis;

namespace RealEstateAnalysis.Data.Abstract.LocationAnalysis
{
    public interface INeighborhoodRepository
    {
        Task DeleteAsync(Neighborhood neighborhood);

        Task<List<Neighborhood>> GetAllAsync(string userId, bool asNoTracking = false);

        Task<Neighborhood> GetByIdAsync(long id, string userId, bool asNoTracking = false);

        Task SaveOrUpdateAsync(Neighborhood city);
    }
}