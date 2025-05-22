using RealEstateAnalysis.Domain.DTOs.Writes;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Domain.Abstract
{
    public interface IContactUsService
    {
        Task SendEmailAsync(WriteContactUs model);
    }
}