using System.Threading.Tasks;

namespace RealEstateAnalysis.Domain.Abstract
{
    public interface IMonetaryTransactionService
    {
        Task AddMoneyAsync(decimal amount, string transactionNumber, string description);
    }
}