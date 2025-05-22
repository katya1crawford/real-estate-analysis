using RealEstateAnalysis.Domain.DTOs.Reads;
using RealEstateAnalysis.Domain.DTOs.Writes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Domain.Abstract.RentalProperty
{
    public interface IGalleryImageService
    {
        Task<List<ReadFile>> GetAllSmallAsync(long propertyId);

        Task<List<ReadFile>> GetAllLargeAsync(long propertyId);

        Task DeleteAsync(long galleryImageId, long propertyId);

        Task<List<ReadFile>> AddAsync(WriteFiles model);
    }
}