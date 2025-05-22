namespace RealEstateAnalysis.Data.Abstract.RentalProperty
{
    public interface IFileContentRepository
    {
        Task<byte[]> GetAsync(long fileId);
    }
}