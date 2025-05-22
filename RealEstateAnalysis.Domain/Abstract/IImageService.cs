namespace RealEstateAnalysis.Domain.Abstract
{
    public interface IImageService
    {
        (byte[] fileBytes, string contentType) ConvertToThumbnailImage(byte[] imageBytes);

        (byte[] fileBytes, string contentType) ConvertToSmallGalleryImage(byte[] imageBytes);

        (byte[] fileBytes, string contentType) ConvertToLargeGalleryImage(byte[] imageBytes);
    }
}