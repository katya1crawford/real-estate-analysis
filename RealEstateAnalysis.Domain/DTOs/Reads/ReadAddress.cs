using RealEstateAnalysis.Data.Entities.RentalProperty;
using RealEstateAnalysis.Data.Entities.Reonomy;

namespace RealEstateAnalysis.Domain.DTOs.Reads
{
    public class ReadAddress
    {
        public ReadAddress(Property property)
        {
            Address = property.Address;
            City = property.City;
            State = new ReadState(property.State);
            ZipCode = property.ZipCode;
            Latitude = property.Latitude;
            Longitude = property.Longitude;
            Neighborhood = property.Neighborhood;
            County = property.County;
        }

        public ReadAddress(SoldProperty property)
        {
            Address = property.Address;
            City = property.City;
            State = new ReadState(property.State);
            ZipCode = property.ZipCode;
            Latitude = property.Latitude ?? 0;
            Longitude = property.Longitude ?? 0;
            Neighborhood = property.Neighborhood;
            County = property.County;
        }

        public ReadAddress(string address, string city, ReadState state, string zipCode, double latitude, double longitude, string neighborhood, string county)
        {
            Address = address;
            City = city;
            State = state;
            ZipCode = zipCode;
            Latitude = latitude;
            Longitude = longitude;
            Neighborhood = neighborhood;
            County = county;
        }

        public string Address { get; }

        public string City { get; }

        public ReadState State { get; }

        public string ZipCode { get; }

        public double Latitude { get; }

        public double Longitude { get; }

        public string Neighborhood { get; }

        public string County { get; }
    }
}