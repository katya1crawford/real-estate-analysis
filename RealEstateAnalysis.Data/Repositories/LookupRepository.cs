using Microsoft.EntityFrameworkCore;
using RealEstateAnalysis.Data.Abstract;
using RealEstateAnalysis.Data.Entities.Lookups;
using RealEstateAnalysis.Domain;

namespace RealEstateAnalysis.Data.Repositories
{
    public class LookupRepository : ILookupRepository
    {
        private readonly EFDbContext dbContext;

        public LookupRepository(EFDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<State>> GetAllStatesAsync() =>
            await dbContext.States.ToListAsync();

        public async Task<State> GetStateByIdAsync(long id) =>
            await dbContext.States.FindAsync(id);

        public async Task<List<PropertyType>> GetAllPropertyTypesAsync() =>
            await dbContext.PropertyTypes.ToListAsync();

        public async Task<PropertyType> GetPropertyTypeByIdAsync(long id) =>
            await dbContext.PropertyTypes.FindAsync(id);

        public async Task<List<OperatingExpenseType>> GetAllOperatingExpenseTypesAsync() =>
            await dbContext.OperatingExpenseTypes.ToListAsync();

        public async Task<List<InteriorRepairExpenseType>> GetAllInteriorRepairExpenseTypesAsync() =>
            await dbContext.InteriorRepairExpenseTypes.ToListAsync();

        public async Task<List<ExteriorRepairExpenseType>> GetAllExteriorRepairExpenseTypesAsync() =>
            await dbContext.ExteriorRepairExpenseTypes.ToListAsync();

        public async Task<List<GeneralRepairExpenseType>> GetAllGeneralRepairExpenseTypesAsync() =>
            await dbContext.GeneralRepairExpenseTypes.ToListAsync();

        public async Task<List<ClosingCostType>> GetAllClosingCostTypesAsync() =>
            await dbContext.ClosingCostTypes.ToListAsync();

        public async Task<PropertyStatus> GetPropertyStatusByIdAsync(long id) =>
            await dbContext.PropertyStatuses.FindAsync(id);

        public async Task<List<PropertyStatus>> GetAllPropertyStatusesAsync() =>
            await dbContext.PropertyStatuses.ToListAsync();
    }
}