using Microsoft.EntityFrameworkCore;
using RealEstateAnalysis.Data.Abstract.LocationAnalysis;
using RealEstateAnalysis.Data.Entities.LocationAnalysis;
using RealEstateAnalysis.Domain;

namespace RealEstateAnalysis.Data.Repositories.LocationAnalysis
{
    public class NeighborhoodRepository : INeighborhoodRepository
    {
        private readonly EFDbContext dbContext;

        public NeighborhoodRepository(EFDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task DeleteAsync(Neighborhood neighborhood)
        {
            dbContext.LocationAnalysis_Neighborhoods.Remove(neighborhood);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Neighborhood>> GetAllAsync(string userId, bool asNoTracking = false) =>
            await GetBaseQuery(asNoTracking).Where(x => x.UserId == userId).ToListAsync();

        public async Task<Neighborhood> GetByIdAsync(long id, string userId, bool asNoTracking = false) =>
           await GetBaseQuery(asNoTracking).FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

        public async Task SaveOrUpdateAsync(Neighborhood neighborhood)
        {
            if (neighborhood.Id == default)
                dbContext.Add(neighborhood);

            await dbContext.SaveChangesAsync();
        }

        private IQueryable<Neighborhood> GetBaseQuery(bool asNoTracking = false)
        {
            var query = dbContext.LocationAnalysis_Neighborhoods
                .Include(x => x.State).AsQueryable();

            if (asNoTracking)
                query.AsNoTracking();

            return query;
        }
    }
}