using RealEstateAnalysis.Domain.Abstract;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace RealEstateAnalysis.Domain.Services
{
    public class ImageService : IImageService
    {
        public (byte[] fileBytes, string contentType) ConvertToThumbnailImage(byte[] imageBytes)
        {
            var destinationWidth = 328;
            var destinationHeight = 271;

            return GetResizedImage(imageBytes, destinationWidth, destinationHeight);
        }

        public (byte[] fileBytes, string contentType) ConvertToLargeGalleryImage(byte[] imageBytes)
        {
            var destinationWidth = 640;
            var destinationHeight = 480;

            return GetResizedImage(imageBytes, destinationWidth, destinationHeight);
        }

        public (byte[] fileBytes, string contentType) ConvertToSmallGalleryImage(byte[] imageBytes)
        {
            var destinationWidth = 200;
            var destinationHeight = 165;

            return GetResizedImage(imageBytes, destinationWidth, destinationHeight);
        }

        private (byte[] fileBytes, string contentType) GetResizedImage(byte[] imageBytes, int destinationWidth, int destinationHeight)
        {
            using (var inStream = new MemoryStream(imageBytes))
            {
                using (var outStream = new MemoryStream())
                {
                    using (var imageToResize = new Bitmap(inStream))
                    {
                        var originalWidth = imageToResize.Width;
                        var originalHeight = imageToResize.Height;

                        //how many units are there to make the original length
                        var hRatio = (float)originalHeight / destinationHeight;
                        var wRatio = (float)originalWidth / destinationWidth;

                        //get the shorter side
                        var ratio = Math.Min(hRatio, wRatio);

                        var hScale = Convert.ToInt32(destinationHeight * ratio);
                        var wScale = Convert.ToInt32(destinationWidth * ratio);

                        //start cropping from the center
                        var startX = (originalWidth - wScale) / 2;
                        var startY = (originalHeight - hScale) / 2;

                        //crop the image from the specified location and size
                        var sourceRectangle = new Rectangle(startX, startY, wScale, hScale);

                        //the future size of the image
                        var bitmap = new Bitmap(destinationWidth, destinationHeight);

                        //fill-in the whole bitmap
                        var destinationRectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

                        //generate the new image
                        using (var graphics = Graphics.FromImage(bitmap))
                        {
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.DrawImage(imageToResize, destinationRectangle, sourceRectangle, GraphicsUnit.Pixel);
                        }

                        bitmap.Save(outStream, ImageFormat.Jpeg);
                        return (outStream.ToArray(), "image/jpeg");
                    }
                }
            }
        }
    }
}