using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RealEstateAnalysis.Domain.Abstract;
using RealEstateAnalysis.Domain.DTOs.ThirdParty.Google;
using RealEstateAnalysis.Domain.DTOs.ThirdParty.Google.Places;
using RealEstateAnalysis.Domain.Settings;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace RealEstateAnalysis.Domain.Services
{
    public class GooglePlaceApiService : IGooglePlaceApiService
    {
        private readonly ApiSettings apiSettings;

        public GooglePlaceApiService(IOptions<ApiSettings> apiOptions)
        {
            apiSettings = apiOptions.Value;
        }

        public async Task<ReadNearbySearch> GetNearbyGroceryOrSupermarkets(double radiusInMiles, double nearbyLat, double nearbyLong)
        {
            var nearbySearchResult = await GetNearbyPlaceByNameAndType(placeName: "",
                placeType: "",
                keyword: "grocery supermarket",
                radiusInMiles: radiusInMiles,
                nearbyLat: nearbyLat,
                nearbyLong: nearbyLong);

            return nearbySearchResult;
        }

        public async Task<ReadNearbySearch> GetNearbyStarbuckses(double radiusInMiles, double nearbyLat, double nearbyLong)
        {
            var nearbySearchResult = await GetNearbyPlaceByNameAndType(placeName: "starbucks",
                placeType: PlaceTypes.Cafe,
                keyword: "",
                radiusInMiles: radiusInMiles,
                nearbyLat: nearbyLat,
                nearbyLong: nearbyLong);

            return nearbySearchResult;
        }

        public async Task<ReadNearbySearch> GetNearbyPawnShops(double radiusInMiles, double nearbyLat, double nearbyLong)
        {
            var nearbySearchResult = await GetNearbyPlaceByNameAndType(placeName: "",
                placeType: "",
                keyword: "pawn shop",
                radiusInMiles: radiusInMiles,
                nearbyLat: nearbyLat,
                nearbyLong: nearbyLong);

            return nearbySearchResult;
        }

        public async Task<ReadNearbySearch> GetNearbyCheckCashingPlaces(double radiusInMiles, double nearbyLat, double nearbyLong)
        {
            var nearbySearchResult = await GetNearbyPlaceByNameAndType(placeName: "",
                placeType: "",
                keyword: "check cashing",
                radiusInMiles: radiusInMiles,
                nearbyLat: nearbyLat,
                nearbyLong: nearbyLong);

            return nearbySearchResult;
        }

        private async Task<ReadNearbySearch> GetNearbyPlaceByNameAndType(string placeName,
            string placeType,
            string keyword,
            double radiusInMiles,
            double nearbyLat,
            double nearbyLong)
        {
            var radiusInMeters = MilesToMeters(radiusInMiles);
            var uriBuilder = new UriBuilder(apiSettings.GooglePlaceNearbySearchBaseUrl)
            {
                Port = -1
            };
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (string.IsNullOrWhiteSpace(placeName) == false)
                query["name"] = placeName.Replace(" ", "+");

            if (string.IsNullOrWhiteSpace(placeType) == false)
                query["type"] = placeType;

            if (string.IsNullOrWhiteSpace(keyword) == false)
                query["keyword"] = keyword;

            query["location"] = $"{nearbyLat},{nearbyLong}";
            query["radius"] = radiusInMeters.ToString();
            query["key"] = apiSettings.GoogleApiKey;
            uriBuilder.Query = query.ToString();
            var url = uriBuilder.ToString();

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var nearbySearchResultJson = await response.Content.ReadAsStringAsync();
                var nearbySearchResult = JsonConvert.DeserializeObject<ReadNearbySearch>(nearbySearchResultJson);

                return nearbySearchResult;
            }
        }

        private double MilesToMeters(double miles)
        {
            var meters = miles * 1609.34;
            return Math.Round(meters, 2);
        }
    }
}