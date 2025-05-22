using RealEstateAnalysis.Domain.DTOs.Writes;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Domain.Abstract
{
    public interface IEmailService
    {
        Task SendAsync(WriteEmail model);
    }
}