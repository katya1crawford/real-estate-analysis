using RealEstateAnalysis.Data.Entities.Reonomy;
using System;

namespace RealEstateAnalysis.Domain.DTOs.Reads
{
    public class ReadSoldProperty
    {
        public ReadSoldProperty(ReadAddress address)
        {
            Address = address;
        }

        public ReadSoldProperty(SoldProperty soldProperty)
        {
            Address = new ReadAddress(soldProperty);
            PropertyType = new ReadLookup(id: soldProperty.PropertyType.Id, name: soldProperty.PropertyType.Name);
            BuildingSquareFootage = soldProperty.BuildingSquareFootage;
            LotSquareFootage = soldProperty.LotSquareFootage;
            MortgageAmount = soldProperty.MortgageAmount;
            MortgageRecordingDate = soldProperty.MortgageRecordingDate?.ToShortDateString();
            MortgageLenderName = soldProperty.MortgageLenderName;
            SalesDate = soldProperty.SalesDate?.ToShortDateString();
            StdLandUseCodeDescription = soldProperty.StdLandUseCodeDescription;
            TotalUnits = soldProperty.TotalUnits;
            YearBuilt = soldProperty.YearBuilt;
            PricePerBuildingSquareFootage = soldProperty.SalesPrice.HasValue && soldProperty.BuildingSquareFootage.HasValue
                ? Math.Round(soldProperty.SalesPrice.Value / soldProperty.BuildingSquareFootage.Value, 2)
                : default(decimal?);
            SalesPrice = soldProperty.SalesPrice;
        }

        public ReadAddress Address { get; }

        public int? BuildingSquareFootage { get; }

        public decimal? PricePerBuildingSquareFootage { get; }

        public int? LotSquareFootage { get; }

        public decimal? MortgageAmount { get; }

        public string MortgageLenderName { get; }

        public string MortgageRecordingDate { get; }

        public ReadLookup PropertyType { get; }

        public string SalesDate { get; }

        public decimal? SalesPrice { get; }

        public string StdLandUseCodeDescription { get; }

        public int? TotalUnits { get; }

        public int? YearBuilt { get; }
    }
}