using RealEstateAnalysis.Data.Entities.Lookups;
using RealEstateAnalysis.Domain.DTOs.Reads;
using RealEstateAnalysis.Domain.DTOs.ThirdParty.Google.Geocode;
using RealEstateAnalysis.Domain.DTOs.Writes;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Domain.Abstract
{
    public interface IGoogleGeocodeApiService
    {
        Task<ReadGeocode> GetGeocodeAsync(WriteAddress addressModel);

        Task<ReadGeocode> GetGeocodeAsync(string address, string city, string stateAbbreviation);

        bool AddressIsValid(Place[] results);

        ReadAddress GeocodeToReadAddress(Place result, State state);
    }
}