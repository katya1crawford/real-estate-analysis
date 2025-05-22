using Microsoft.EntityFrameworkCore;
using RealEstateAnalysis.Data.Abstract;
using RealEstateAnalysis.Data.Entities;
using RealEstateAnalysis.Domain;

namespace RealEstateAnalysis.Data.Repositories
{
    public class MonetaryTransactionRepository : IMonetaryTransactionRepository
    {
        private readonly EFDbContext dbContext;

        public MonetaryTransactionRepository(EFDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<MonetaryTransaction> GetRecent(string userId) =>
            await dbContext.MonetaryTransactions.Where(x => x.UserId == userId).OrderByDescending(x => x.Id).FirstOrDefaultAsync();

        public async Task SaveAsync(MonetaryTransaction monetaryTransaction)
        {
            dbContext.MonetaryTransactions.Add(monetaryTransaction);
            await dbContext.SaveChangesAsync();
        }
    }
}