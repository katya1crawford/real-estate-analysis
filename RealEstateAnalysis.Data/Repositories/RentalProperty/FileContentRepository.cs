using RealEstateAnalysis.Data.Abstract.RentalProperty;
using RealEstateAnalysis.Domain;

namespace RealEstateAnalysis.Data.Repositories.RentalProperty
{
    public class FileContentRepository : IFileContentRepository
    {
        private readonly EFDbContext dbContext;

        public FileContentRepository(EFDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<byte[]> GetAsync(long fileId)
        {
            var fileContent = await dbContext.RentalProperty_FilesContent.FindAsync(fileId);
            return fileContent.Content;
        }
    }
}