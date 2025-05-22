using Microsoft.EntityFrameworkCore;
using RealEstateAnalysis.Data.Abstract.RentalProperty;
using RealEstateAnalysis.Data.Entities.RentalProperty;
using RealEstateAnalysis.Domain;

namespace RealEstateAnalysis.Data.Repositories.RentalProperty
{
    public class GalleryImageRepository : IGalleryImageRepository
    {
        private readonly EFDbContext dbContext;

        public GalleryImageRepository(EFDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GalleryImage>> GetAllAsync(long propertyId, string userId, bool asNoTracking = false) =>
            await GetBaseQuery(asNoTracking).Where(x => x.PropertyId == propertyId && x.Property.UserId == userId).ToListAsync();

        public async Task<GalleryImage> GetAsync(long galleryImageId, long propertyId, string userId, bool asNoTracking = false) =>
            await GetBaseQuery(asNoTracking).FirstOrDefaultAsync(x => x.Id == galleryImageId && x.PropertyId == propertyId && x.Property.UserId == userId);

        public async Task AddAsync(List<GalleryImage> galleryImages)
        {
            await dbContext.AddRangeAsync(galleryImages);
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(GalleryImage galleryImage)
        {
            dbContext.RentalProperty_GalleryImages.Remove(galleryImage);
            await dbContext.SaveChangesAsync();
        }

        private IQueryable<GalleryImage> GetBaseQuery(bool asNoTracking = false)
        {
            var query = dbContext.RentalProperty_GalleryImages;

            if (asNoTracking)
                query.AsNoTracking();

            return query;
        }
    }
}