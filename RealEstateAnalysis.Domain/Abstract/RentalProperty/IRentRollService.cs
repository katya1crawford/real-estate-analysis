using RealEstateAnalysis.Domain.DTOs.Reads.RentalProperty;
using RealEstateAnalysis.Domain.DTOs.Writes.RentalProperty;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Domain.Abstract.RentalProperty
{
    public interface IRentRollService
    {
        Task<ReadRentRollSummary> ImportRentRollCsv(WriteImportRentRollCsv model);

        Task<ReadRentRollSummary> GetRentRollSummary(long propertyId);
    }
}