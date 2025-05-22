namespace RealEstateAnalysis.Domain.DTOs.Reads
{
    public class ReadNearbyPlace
    {
        public ReadNearbyPlace(string name, double latitude, double longitude)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
        }

        public string Name { get; }

        public double Latitude { get; }

        public double Longitude { get; }
    }
}