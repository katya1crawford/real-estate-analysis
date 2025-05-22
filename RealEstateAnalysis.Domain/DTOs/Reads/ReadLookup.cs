namespace RealEstateAnalysis.Domain.DTOs.Reads
{
    public class ReadLookup
    {
        public ReadLookup(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public long Id { get; }

        public string Name { get; }
    }
}