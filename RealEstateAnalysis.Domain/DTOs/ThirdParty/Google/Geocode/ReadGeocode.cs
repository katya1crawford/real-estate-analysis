using Newtonsoft.Json;

namespace RealEstateAnalysis.Domain.DTOs.ThirdParty.Google.Geocode
{
    public class ReadGeocode
    {
        [JsonProperty("results")]
        public Place[] Places { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class Address
    {
        [JsonProperty("long_name")]
        public string LongName { get; set; }

        [JsonProperty("short_name")]
        public string ShortName { get; set; }

        [JsonProperty("types")]
        public string[] Types { get; set; }
    }

    public class Bounds
    {
        [JsonProperty("northeast")]
        public Coordinates Northeast { get; set; }

        [JsonProperty("southwest")]
        public Coordinates Southwest { get; set; }
    }

    public class Coordinates
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lng")]
        public double Longitude { get; set; }
    }

    public class Geometry
    {
        [JsonProperty("bounds")]
        public Bounds Bounds { get; set; }

        [JsonProperty("location")]
        public Coordinates Location { get; set; }

        [JsonProperty("location_type")]
        public string LocationType { get; set; }

        [JsonProperty("viewport")]
        public Viewport Viewport { get; set; }
    }

    public class Place
    {
        [JsonProperty("address_components")]
        public Address[] Address { get; set; }

        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        [JsonProperty("types")]
        public string[] Types { get; set; }
    }

    public class Viewport
    {
        [JsonProperty("northeast")]
        public Coordinates Northeast { get; set; }

        [JsonProperty("southwest")]
        public Coordinates Southwest { get; set; }
    }
}