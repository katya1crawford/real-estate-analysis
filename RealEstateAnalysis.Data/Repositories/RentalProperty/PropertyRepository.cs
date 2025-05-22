using Microsoft.EntityFrameworkCore;
using RealEstateAnalysis.Data.Abstract.RentalProperty;
using RealEstateAnalysis.Data.Entities.RentalProperty;
using RealEstateAnalysis.Domain;

namespace RealEstateAnalysis.Data.Repositories.RentalProperty
{
    public class PropertyRepository : IPropertyRepository
    {
        private readonly EFDbContext dbContext;

        public PropertyRepository(EFDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task DeleteAsync(Property property)
        {
            dbContext.RentalProperty_Properties.Remove(property);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<Property>> GetByStatusId(string userId, long propertyStatusId, bool asNoTracking = false) =>
            await GetBaseQuery(asNoTracking)
                .Where(x => x.UserId == userId && x.PropertyStatusId == propertyStatusId && string.IsNullOrWhiteSpace(x.GroupName))
                .ToListAsync();

        public async Task<List<string>> GetAllGroupNamesAsync(string userId) =>
            await dbContext.RentalProperty_Properties
                .AsNoTracking()
                .Where(x => x.UserId == userId && string.IsNullOrWhiteSpace(x.GroupName) == false)
                .Select(x => x.GroupName)
                .GroupBy(x => x)
                .Select(x => x.Key)
                .OrderBy(x => x)
                .ToListAsync();

        public async Task<Dictionary<long, string>> GetAllSubjectPropertyLookupsAsync(string userId) =>
            await dbContext.RentalProperty_Properties
                .AsNoTracking()
                .Where(x => x.UserId == userId && string.IsNullOrWhiteSpace(x.GroupName))
                .Select(x => new { x.Id, x.Address })
                .OrderBy(x => x.Address)
                .ToDictionaryAsync(x => x.Id, x => x.Address);

        public async Task<List<Property>> GetByGroupNameAsync(string groupName, string userId, bool asNoTracking = false) =>
                    await GetBaseQuery(asNoTracking).Where(x => x.UserId == userId && x.GroupName == groupName).ToListAsync();

        public async Task<Property> GetByIdAsync(long id, string userId, bool asNoTracking = false) =>
           await GetBaseQuery(asNoTracking).FirstOrDefaultAsync(x => x.Id == id && x.UserId == userId);

        public async Task SaveOrUpdateAsync(Property property)
        {
            if (property.Id == default)
                dbContext.Add(property);

            await dbContext.SaveChangesAsync();
        }

        private IQueryable<Property> GetBaseQuery(bool asNoTracking = false)
        {
            var query = dbContext.RentalProperty_Properties
                .Include(x => x.UnitGroups)
                .Include(x => x.AnnualOperatingExpenses).ThenInclude(x => x.OperatingExpenseType)
                .Include(x => x.ClosingCosts).ThenInclude(x => x.ClosingCostType)
                .Include(x => x.InteriorRepairExpenses).ThenInclude(x => x.InteriorRepairExpenseType)
                .Include(x => x.ExteriorRepairExpenses).ThenInclude(x => x.ExteriorRepairExpenseType)
                .Include(x => x.GeneralRepairExpenses).ThenInclude(x => x.GeneralRepairExpenseType)
                .Include(x => x.Files)
                .Include(x => x.RentRollItems)
                .Include(x => x.PropertyType)
                .Include(x => x.PropertyStatus)
                .Include(x => x.State).AsQueryable();

            if (asNoTracking)
                query.AsNoTracking();

            return query;
        }
    }
}