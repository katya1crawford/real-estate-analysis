using RealEstateAnalysis.Domain.DTOs.ThirdParty.Google.Places;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Domain.Abstract
{
    public interface IGooglePlaceApiService
    {
        Task<ReadNearbySearch> GetNearbyGroceryOrSupermarkets(double radiusInMiles, double nearbyLat, double nearbyLong);

        Task<ReadNearbySearch> GetNearbyStarbuckses(double radiusInMiles, double nearbyLat, double nearbyLong);

        Task<ReadNearbySearch> GetNearbyPawnShops(double radiusInMiles, double nearbyLat, double nearbyLong);

        Task<ReadNearbySearch> GetNearbyCheckCashingPlaces(double radiusInMiles, double nearbyLat, double nearbyLong);
    }
}