using RealEstateAnalysis.Domain.DTOs.Reads;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Domain.Abstract.RentalProperty
{
    public interface IPdfService
    {
        Task<ReadFile> GetPropertySummaryPdfAsync(long propertyId);
    }
}