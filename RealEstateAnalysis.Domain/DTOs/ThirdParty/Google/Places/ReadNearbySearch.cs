using Newtonsoft.Json;
using System.Collections.Generic;

namespace RealEstateAnalysis.Domain.DTOs.ThirdParty.Google.Places
{
    public class ReadNearbySearch
    {
        [JsonProperty("results")]
        public List<Place> Places { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class Place
    {
        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("icon")]
        public string Icon { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        [JsonProperty("types")]
        public List<string> PlaceTypes { get; set; }

        [JsonProperty("vicinity")]
        public string Vicinity { get; set; }
    }

    public class Geometry
    {
        [JsonProperty("location")]
        public Location Location { get; set; }
    }

    public class Location
    {
        [JsonProperty("lat")]
        public float Latitude { get; set; }

        [JsonProperty("lng")]
        public float Longitude { get; set; }
    }
}