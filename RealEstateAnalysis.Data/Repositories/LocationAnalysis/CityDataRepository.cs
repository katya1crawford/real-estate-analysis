using Microsoft.EntityFrameworkCore;
using RealEstateAnalysis.Data.Abstract.LocationAnalysis;
using RealEstateAnalysis.Data.Entities.LocationAnalysis;
using RealEstateAnalysis.Domain;

namespace RealEstateAnalysis.Data.Repositories.LocationAnalysis
{
    public class CityDataRepository : ICityDataRepository
    {
        private readonly EFDbContext dbContext;

        public CityDataRepository(EFDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<CityDataStateUrl>> GetAllStatesUrlsAsync()
        {
            return await dbContext.LocationAnalysis_CityDataStateUrls.AsNoTracking().ToListAsync();
        }
    }
}