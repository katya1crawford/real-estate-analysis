using RealEstateAnalysis.Data.Entities.Lookups;

namespace RealEstateAnalysis.Data.Abstract
{
    public interface ILookupRepository
    {
        Task<List<ClosingCostType>> GetAllClosingCostTypesAsync();

        Task<List<ExteriorRepairExpenseType>> GetAllExteriorRepairExpenseTypesAsync();

        Task<List<GeneralRepairExpenseType>> GetAllGeneralRepairExpenseTypesAsync();

        Task<List<InteriorRepairExpenseType>> GetAllInteriorRepairExpenseTypesAsync();

        Task<List<OperatingExpenseType>> GetAllOperatingExpenseTypesAsync();

        Task<List<PropertyType>> GetAllPropertyTypesAsync();

        Task<List<State>> GetAllStatesAsync();

        Task<PropertyType> GetPropertyTypeByIdAsync(long id);

        Task<List<PropertyStatus>> GetAllPropertyStatusesAsync();

        Task<PropertyStatus> GetPropertyStatusByIdAsync(long id);

        Task<State> GetStateByIdAsync(long id);
    }
}