using Newtonsoft.Json;
using System.Collections.Generic;

namespace RealEstateAnalysis.Domain.DTOs.ThirdParty.Reonomy
{
    public class Reonomy
    {
        [JsonProperty("items")]
        public List<SoldProperty> SoldProperties { get; set; }
    }

    public class SoldProperty
    {
        [JsonProperty("asset_category")]
        public string AssetCategory { get; set; }

        [JsonProperty("building_area")]
        public double? BuildingArea { get; set; }

        [JsonProperty("building_update_time")]
        public string BuildingUpdateTime { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("direction_left")]
        public string DirectionLeft { get; set; }

        [JsonProperty("fips")]
        public string Fips { get; set; }

        [JsonProperty("house_nbr")]
        public string HouseNbr { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("lot_size_acres")]
        public double? LotSizeAcres { get; set; }

        [JsonProperty("lot_size_sqft")]
        public double? LotSizeSqft { get; set; }

        [JsonProperty("master_update_time")]
        public string MasterUpdateTime { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("mortgage_amount")]
        public decimal? MortgageAmount { get; set; }

        [JsonProperty("mortgage_lender_name")]
        public string MortgageLenderName { get; set; }

        [JsonProperty("mortgage_recording_date")]
        public string MortgageRecordingDate { get; set; }

        [JsonProperty("mtg_update_time")]
        public string MtgUpdateTime { get; set; }

        [JsonProperty("owner_update_time")]
        public string OwnerUpdateTime { get; set; }

        [JsonProperty("parcel_shape_ids")]
        public List<string> ParcelShapeIds { get; set; }

        [JsonProperty("reported_owners")]
        public List<ReportedOwners> ReportedOwners { get; set; }

        [JsonProperty("sale_update_time")]
        public string SaleUpdateTime { get; set; }

        [JsonProperty("sales_date")]
        public string SalesDate { get; set; }

        [JsonProperty("shape_update_time")]
        public string ShapeUpdateTime { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("std_land_use_code_description")]
        public string StdLandUseCodeDescription { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("sum_building_sqft")]
        public double? SumBuildingSqft { get; set; }

        [JsonProperty("tax_amount")]
        public decimal? TaxAmount { get; set; }

        [JsonProperty("tax_update_time")]
        public string TaxUpdateTime { get; set; }

        [JsonProperty("tax_year")]
        public int? TaxYear { get; set; }

        [JsonProperty("total_units")]
        public int? TotalUnits { get; set; }

        [JsonProperty("year_built")]
        public int? YearBuilt { get; set; }

        [JsonProperty("zip4")]
        public string Zip4 { get; set; }

        [JsonProperty("zip5")]
        public string Zip5 { get; set; }

        [JsonProperty("sales_price")]
        public decimal? SalesPrice { get; set; }
    }

    public class ReportedOwners
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}