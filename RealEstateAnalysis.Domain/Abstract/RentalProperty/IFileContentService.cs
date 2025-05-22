using RealEstateAnalysis.Domain.DTOs.Reads;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Domain.Abstract.RentalProperty
{
    public interface IFileContentService
    {
        Task<ReadFile> GetAsync(long propertyId, long fileId);
    }
}