using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RealEstateAnalysis.Data.Abstract;
using RealEstateAnalysis.Data.Entities.Lookups;
using RealEstateAnalysis.Domain.Abstract;
using RealEstateAnalysis.Domain.DTOs.Reads;
using RealEstateAnalysis.Domain.DTOs.ThirdParty.Google;
using RealEstateAnalysis.Domain.DTOs.ThirdParty.Google.Geocode;
using RealEstateAnalysis.Domain.DTOs.Writes;
using RealEstateAnalysis.Domain.Settings;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Domain.Services
{
    public class GoogleGeocodeApiService : IGoogleGeocodeApiService
    {
        private readonly ApiSettings apiSettings;
        private readonly ILookupRepository lookupRepository;

        public GoogleGeocodeApiService(ILookupRepository lookupRepository, IOptions<ApiSettings> apiOptions)
        {
            this.lookupRepository = lookupRepository;
            apiSettings = apiOptions.Value;
        }

        public bool AddressIsValid(Place[] results)
        {
            if (results == null
                || results.Count() == 0
                || results.Count() > 1
                || results[0].Address.Any(x => x.Types.Any(y => y == AddressTypes.StreetNumber)) == false
                || results[0].Address.Any(x => x.Types.Any(y => y == AddressTypes.Route)) == false
                || (results[0].Address.FirstOrDefault(x => x.Types.Contains(AddressTypes.Locality) && x.Types.Contains(AddressTypes.Political)) == null
                        && results[0].Address.FirstOrDefault(x => x.Types.Contains(AddressTypes.Neighborhood) && x.Types.Contains(AddressTypes.Political)) == null)
                || results[0].Address.FirstOrDefault(x => x.Types.Contains(AddressTypes.AdministrativeAreaLevelTwo) && x.Types.Contains(AddressTypes.Political)) == null
                || results[0].Address.Any(x => x.Types.Any(y => y == AddressTypes.PostalCode)) == false)
                return false;
            else
                return true;
        }

        public ReadAddress GeocodeToReadAddress(Place result, State state)
        {
            var location = result.Geometry.Location;
            var streetNumber = result.Address.FirstOrDefault(x => x.Types.Contains(AddressTypes.StreetNumber));
            var streetName = result.Address.FirstOrDefault(x => x.Types.Contains(AddressTypes.Route));
            var city = result.Address.FirstOrDefault(x => x.Types.Contains(AddressTypes.Locality) && x.Types.Contains(AddressTypes.Political));
            var postalCode = result.Address.FirstOrDefault(x => x.Types.Contains(AddressTypes.PostalCode));
            var neighborhood = result.Address.FirstOrDefault(x => x.Types.Contains(AddressTypes.Neighborhood) && x.Types.Contains(AddressTypes.Political));
            var county = result.Address.FirstOrDefault(x => x.Types.Contains(AddressTypes.AdministrativeAreaLevelTwo) && x.Types.Contains(AddressTypes.Political));

            if (city == null)
            {
                city = result.Address.FirstOrDefault(x => x.Types.Contains(AddressTypes.Neighborhood) && x.Types.Contains(AddressTypes.Political));
            }

            return new ReadAddress(address: $"{streetNumber.ShortName} {streetName.ShortName}",
                city: city.ShortName,
                state: new ReadState(state),
                zipCode: postalCode.ShortName,
                latitude: location.Latitude,
                longitude: location.Longitude,
                neighborhood: neighborhood?.LongName,
                county: county.LongName);
        }

        public async Task<ReadGeocode> GetGeocodeAsync(WriteAddress addressModel)
        {
            using (var client = new HttpClient())
            {
                var state = await lookupRepository.GetStateByIdAsync(addressModel.StateId);
                var uri = Uri.EscapeUriString($"{apiSettings.GoogleGeocodeBaseUrl}?address={addressModel.Address},+{addressModel.City},+{state.Abbreviation}&key={apiSettings.GoogleApiKey}");
                var response = await client.GetAsync(uri);
                var geocodeJson = await response.Content.ReadAsStringAsync();
                var geocode = JsonConvert.DeserializeObject<ReadGeocode>(geocodeJson);

                return geocode;
            }
        }

        public async Task<ReadGeocode> GetGeocodeAsync(string address, string city, string stateAbbreviation)
        {
            using (var client = new HttpClient())
            {
                var uri = Uri.EscapeUriString($"{apiSettings.GoogleGeocodeBaseUrl}?address={address.Replace(" ", "+")},+{city},+{stateAbbreviation}&key={apiSettings.GoogleApiKey}");
                var response = await client.GetAsync(uri);
                var geocodeJson = await response.Content.ReadAsStringAsync();
                var geocode = JsonConvert.DeserializeObject<ReadGeocode>(geocodeJson);

                return geocode;
            }
        }
    }
}