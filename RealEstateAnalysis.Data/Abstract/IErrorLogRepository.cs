using RealEstateAnalysis.Data.Entities;

namespace RealEstateAnalysis.Data.Abstract
{
    public interface IErrorLogRepository
    {
        Task SaveOrUpdateAsync(ErrorLog errorLog);
    }
}