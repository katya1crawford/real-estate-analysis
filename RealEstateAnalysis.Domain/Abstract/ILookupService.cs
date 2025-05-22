using RealEstateAnalysis.Domain.DTOs.Reads;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Domain.Abstract
{
    public interface ILookupService
    {
        Task<List<ReadLookup>> GetAllClosingCostTypesAsync();

        Task<List<ReadLookup>> GetAllExteriorRepairExpenseTypesAsync();

        Task<List<ReadLookup>> GetAllGeneralRepairExpenseTypesAsync();

        Task<List<ReadLookup>> GetAllInteriorRepairExpenseTypesAsync();

        Task<List<ReadLookup>> GetAllOperatingExpenseTypesAsync();

        Task<List<ReadLookup>> GetAllPropertyStatusesAsync();

        Task<List<ReadLookup>> GetAllPropertyTypesAsync();

        Task<List<ReadLookup>> GetAllStatesAsync();
    }
}