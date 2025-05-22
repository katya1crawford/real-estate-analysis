using RealEstateAnalysis.Data.Abstract;
using RealEstateAnalysis.Domain.Abstract;
using RealEstateAnalysis.Domain.DTOs.Reads;
using RealEstateAnalysis.Domain.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Domain.Services
{
    public class LookupService : ILookupService
    {
        private readonly ILookupRepository lookupRepository;

        public LookupService(ILookupRepository lookupRepository)
        {
            this.lookupRepository = lookupRepository;
        }

        public async Task<List<ReadLookup>> GetAllClosingCostTypesAsync()
        {
            var closingCostTypes = await lookupRepository.GetAllClosingCostTypesAsync();
            var closingCostTypesLookup = closingCostTypes
                .OrderBy(x => x.Id == (long)ClosingCostTypeEnum.OtherFeesAndCharges)
                .ThenBy(x => x.Name)
                .Select(x => new ReadLookup(id: x.Id, name: x.Name))
                .ToList();

            return closingCostTypesLookup;
        }

        public async Task<List<ReadLookup>> GetAllExteriorRepairExpenseTypesAsync()
        {
            var exteriorRepairExpenseTypes = await lookupRepository.GetAllExteriorRepairExpenseTypesAsync();
            var exteriorRepairExpenseLookup = exteriorRepairExpenseTypes
                .OrderBy(x => x.Id == (long)ExteriorRepairExpenseTypeEnum.Other)
                .ThenBy(x => x.Name)
                .Select(x => new ReadLookup(id: x.Id, name: x.Name))
                .ToList();

            return exteriorRepairExpenseLookup;
        }

        public async Task<List<ReadLookup>> GetAllGeneralRepairExpenseTypesAsync()
        {
            var generalRepairExpenseTypes = await lookupRepository.GetAllGeneralRepairExpenseTypesAsync();
            var generalRepairExpenseLookup = generalRepairExpenseTypes
                .OrderBy(x => x.Id == (long)GeneralRepairExpenseTypeEnum.Other)
                .ThenBy(x => x.Name)
                .Select(x => new ReadLookup(id: x.Id, name: x.Name))
                .ToList();

            return generalRepairExpenseLookup;
        }

        public async Task<List<ReadLookup>> GetAllInteriorRepairExpenseTypesAsync()
        {
            var interiorRepairExpenseTypes = await lookupRepository.GetAllInteriorRepairExpenseTypesAsync();
            var interiorRepairExpenseLookup = interiorRepairExpenseTypes
                .OrderBy(x => x.Id == (long)InteriorRepairExpenseTypeEnum.Other)
                .ThenBy(x => x.Name)
                .Select(x => new ReadLookup(id: x.Id, name: x.Name))
                .ToList();

            return interiorRepairExpenseLookup;
        }

        public async Task<List<ReadLookup>> GetAllOperatingExpenseTypesAsync()
        {
            var operatingExpenseTypes = await lookupRepository.GetAllOperatingExpenseTypesAsync();
            var operatingExpenseLookup = operatingExpenseTypes
                .OrderBy(x => x.Id == (long)OperatingExpenseTypeEnum.Other)
                .ThenBy(x => x.Name)
                .Select(x => new ReadLookup(id: x.Id, name: x.Name))
                .ToList();

            return operatingExpenseLookup;
        }

        public async Task<List<ReadLookup>> GetAllPropertyTypesAsync() =>
            (await lookupRepository.GetAllPropertyTypesAsync()).Select(x => new ReadLookup(id: x.Id, name: x.Name)).OrderBy(x => x.Name).ToList();

        public async Task<List<ReadLookup>> GetAllStatesAsync() =>
            (await lookupRepository.GetAllStatesAsync()).Select(x => new ReadLookup(id: x.Id, name: x.Name)).OrderBy(x => x.Name).ToList();

        public async Task<List<ReadLookup>> GetAllPropertyStatusesAsync() =>
            (await lookupRepository.GetAllPropertyStatusesAsync()).Select(x => new ReadLookup(id: x.Id, name: x.Name)).OrderBy(x => x.Name).ToList();
    }
}