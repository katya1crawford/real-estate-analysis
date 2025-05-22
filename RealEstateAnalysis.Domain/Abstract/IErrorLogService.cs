using RealEstateAnalysis.Domain.DTOs.Writes;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Domain.Abstract
{
    public interface IErrorLogService
    {
        Task LogErrorAsync(WriteErrorLog model);
    }
}