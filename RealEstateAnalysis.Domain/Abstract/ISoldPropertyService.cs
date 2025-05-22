using RealEstateAnalysis.Domain.DTOs.Reads;
using RealEstateAnalysis.Domain.DTOs.Writes;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Domain.Abstract
{
    public interface ISoldPropertyService
    {
        Task LoadDataIntoDatabase();

        Task UpdateSoldPropertiesWithGeocodeData();

        Task<ReadSoldProprtySearchResults> Search(WriteSoldPropertiesSearch model);
    }
}