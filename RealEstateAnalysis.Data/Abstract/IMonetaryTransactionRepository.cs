using RealEstateAnalysis.Data.Entities;

namespace RealEstateAnalysis.Data.Abstract
{
    public interface IMonetaryTransactionRepository
    {
        Task<MonetaryTransaction> GetRecent(string userId);

        Task SaveAsync(MonetaryTransaction monetaryTransaction);
    }
}