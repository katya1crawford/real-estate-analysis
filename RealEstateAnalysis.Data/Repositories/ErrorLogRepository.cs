using RealEstateAnalysis.Data.Abstract;
using RealEstateAnalysis.Data.Entities;
using RealEstateAnalysis.Domain;

namespace RealEstateAnalysis.Data.Repositories
{
    public class ErrorLogRepository : IErrorLogRepository
    {
        private readonly EFDbContext dbContext;

        public ErrorLogRepository(EFDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task SaveOrUpdateAsync(ErrorLog errorLog)
        {
            if (errorLog.Id == default)
                dbContext.Add(errorLog);

            await dbContext.SaveChangesAsync();
        }
    }
}