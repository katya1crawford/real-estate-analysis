using Newtonsoft.Json;
using System.Collections.Generic;

namespace RealEstateAnalysis.Domain.DTOs.ThirdParty.Zipwise
{
    public class Zipwise
    {
        [JsonProperty("results")]
        public List<Result> Results { get; set; }
    }

    public class Result
    {
        [JsonProperty("zip")]
        public string ZipCode { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("county")]
        public string County { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("distance")]
        public string Distance { get; set; }
    }
}