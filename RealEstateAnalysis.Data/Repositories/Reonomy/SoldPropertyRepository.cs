using Microsoft.EntityFrameworkCore;
using RealEstateAnalysis.Data.Abstract.Reonomy;
using RealEstateAnalysis.Data.DTOs;
using RealEstateAnalysis.Data.Entities.Reonomy;
using RealEstateAnalysis.Domain;

namespace RealEstateAnalysis.Data.Repositories.Reonomy
{
    public class SoldPropertyRepository : ISoldPropertyRepository
    {
        private readonly EFDbContext dbContext;

        public SoldPropertyRepository(EFDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<SoldProperty>> GetAllWithMissingLatLongAsync() =>
            await GetBaseQuery().Where(x => x.InvalidAddress == false && (x.Latitude.HasValue == false || x.Longitude.HasValue == false)).ToListAsync();

        public async Task<List<string>> GetAllReonomySoldPropertySourceIds() =>
            await GetBaseQuery().Select(x => x.SourceId).ToListAsync();

        public async Task SaveOrUpdateAsync(List<SoldProperty> soldProperties)
        {
            foreach (var soldProperty in soldProperties)
            {
                if (soldProperty.Id == default)
                    dbContext.Add(soldProperty);
            }

            await dbContext.SaveChangesAsync();
        }

        public async Task<List<SoldProperty>> SearchAsync(SoldPropertiesSearch searchFilters)
        {
            var query = GetBaseQuery(asNoTracking: true).Where(x => x.InvalidAddress == false).AsNoTracking();

            if (searchFilters.PropertyTypeId.HasValue)
            {
                query = query.Where(x => x.PropertyTypeId == searchFilters.PropertyTypeId);
            }

            if (searchFilters.UnitsMin.HasValue)
            {
                query = query.Where(x => x.TotalUnits >= searchFilters.UnitsMin.Value);
            }

            if (searchFilters.UnitsMax.HasValue)
            {
                query = query.Where(x => x.TotalUnits <= searchFilters.UnitsMax.Value);
            }

            if (searchFilters.YearMin.HasValue)
            {
                query = query.Where(x => x.YearBuilt >= searchFilters.YearMin.Value);
            }

            if (searchFilters.YearMax.HasValue)
            {
                query = query.Where(x => x.YearBuilt <= searchFilters.YearMax.Value);
            }

            if (searchFilters.ZipCodes?.Count() > 0)
            {
                query = query.Where(x => searchFilters.ZipCodes.Contains(x.ZipCode));
            }

            if (string.IsNullOrWhiteSpace(searchFilters.Address) == false)
            {
                query = query.Where(x => x.Address.Trim().ToLower() == searchFilters.Address.Trim().ToLower());
            }

            if (string.IsNullOrWhiteSpace(searchFilters.City) == false)
            {
                query = query.Where(x => x.City.Trim().ToLower().Contains(searchFilters.City.Trim().ToLower()));
            }

            if (searchFilters.StateId.HasValue)
            {
                query = query.Where(x => x.StateId == searchFilters.StateId);
            }

            return await query.OrderByDescending(x => x.SalesDate).Take(150).ToListAsync();
        }

        private IQueryable<SoldProperty> GetBaseQuery(bool asNoTracking = false)
        {
            var query = dbContext.Reonomy_SoldProperties
                .Include(x => x.PropertyType)
                .Include(x => x.State)
                .AsQueryable();

            if (asNoTracking)
                query.AsNoTracking();

            return query;
        }
    }
}