namespace RealEstateAnalysis.Data.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; private set; }

        public DateTime CreatedDate { get; internal set; }

        public DateTime? ModifiedDate { get; internal set; }
    }
}