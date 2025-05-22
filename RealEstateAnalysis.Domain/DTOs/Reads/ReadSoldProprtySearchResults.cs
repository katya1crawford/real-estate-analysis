using System.Collections.Generic;

namespace RealEstateAnalysis.Domain.DTOs.Reads
{
    public class ReadSoldProprtySearchResults
    {
        public ReadSoldProprtySearchResults(List<ReadSoldProperty> soldProperties,
            ReadSoldProperty subjectProperty,
            List<ReadNearbyPlace> nearbyGroceryOrSupermarkets,
            List<ReadNearbyPlace> nearbyStarbuckses,
            List<ReadNearbyPlace> nearbyPawnShops,
            List<ReadNearbyPlace> nearbyCheckCashingPlaces)
        {
            SoldProperties = soldProperties;
            SubjectProperty = subjectProperty;
            NearbyGroceryOrSupermarkets = nearbyGroceryOrSupermarkets;
            NearbyStarbuckses = nearbyStarbuckses;
            NearbyPawnShops = nearbyPawnShops;
            NearbyCheckCashingPlaces = nearbyCheckCashingPlaces;
        }

        public List<ReadSoldProperty> SoldProperties { get; }

        public ReadSoldProperty SubjectProperty { get; }

        public List<ReadNearbyPlace> NearbyGroceryOrSupermarkets { get; }

        public List<ReadNearbyPlace> NearbyStarbuckses { get; }

        public List<ReadNearbyPlace> NearbyPawnShops { get; }

        public List<ReadNearbyPlace> NearbyCheckCashingPlaces { get; }
    }
}