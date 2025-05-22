using RealEstateAnalysis.Data.Entities.Lookups;

namespace RealEstateAnalysis.Domain.DTOs.Reads
{
    public class ReadState
    {
        public ReadState(State state)
        {
            Id = state.Id;
            Abbreviation = state.Abbreviation;
            Name = state.Name;
        }

        public long Id { get; }

        public string Abbreviation { get; }

        public string Name { get; }
    }
}