using RealEstateAnalysis.Data.Entities.RentalProperty;

namespace RealEstateAnalysis.Data.Abstract.RentalProperty
{
    public interface IGalleryImageRepository
    {
        Task<List<GalleryImage>> GetAllAsync(long propertyId, string userId, bool asNoTracking = false);

        Task<GalleryImage> GetAsync(long galleryImageId, long propertyId, string userId, bool asNoTracking = false);

        Task AddAsync(List<GalleryImage> galleryImages);

        Task DeleteAsync(GalleryImage galleryImage);
    }
}