using FluentValidation;
using FluentValidation.Results;
using RealEstateAnalysis.Data.Abstract.RentalProperty;
using RealEstateAnalysis.Data.Entities.RentalProperty;
using RealEstateAnalysis.Domain.Abstract;
using RealEstateAnalysis.Domain.Abstract.RentalProperty;
using RealEstateAnalysis.Domain.DTOs.Reads;
using RealEstateAnalysis.Domain.DTOs.Writes;
using RealEstateAnalysis.Domain.Validators;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Domain.Services.RentalProperty
{
    public class GalleryImageService : IGalleryImageService
    {
        private readonly IMembershipService membershipService;
        private readonly IPropertyRepository propertyRepository;
        private readonly IGalleryImageRepository galleryImageRepository;
        private readonly IImageService imageService;

        public GalleryImageService(IMembershipService membershipService,
            IPropertyRepository propertyRepository,
            IGalleryImageRepository galleryImageRepository,
            IImageService imageService)
        {
            this.membershipService = membershipService;
            this.propertyRepository = propertyRepository;
            this.galleryImageRepository = galleryImageRepository;
            this.imageService = imageService;
        }

        public async Task<List<ReadFile>> GetAllSmallAsync(long propertyId)
        {
            var loggedInUser = membershipService.GetLoggedInUser();
            var galleryImages = await galleryImageRepository.GetAllAsync(propertyId, loggedInUser.Id, asNoTracking: true);

            var readFiles = new List<ReadFile>();

            foreach (var image in galleryImages)
            {
                var smallImage = imageService.ConvertToSmallGalleryImage(image.Content);

                readFiles.Add(new ReadFile(id: image.Id,
                    name: image.Name,
                    bytes: smallImage.fileBytes,
                    mimeType: smallImage.contentType,
                    createdDate: image.CreatedDate));
            }

            return readFiles;
        }

        public async Task<List<ReadFile>> GetAllLargeAsync(long propertyId)
        {
            var loggedInUser = membershipService.GetLoggedInUser();
            var galleryImages = await galleryImageRepository.GetAllAsync(propertyId, loggedInUser.Id, asNoTracking: true);

            var readFiles = new List<ReadFile>();

            foreach (var image in galleryImages)
            {
                readFiles.Add(new ReadFile(id: image.Id,
                    name: image.Name,
                    bytes: image.Content,
                    mimeType: image.ContentType,
                    createdDate: image.CreatedDate));
            }

            return readFiles;
        }

        public async Task<List<ReadFile>> AddAsync(WriteFiles model)
        {
            var validator = new ImageFilesValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var loggedInUser = membershipService.GetLoggedInUser();
            var property = await propertyRepository.GetByIdAsync(model.PropertyId, loggedInUser.Id);

            if (property == null)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid property ID.") });

            var galleryImages = new List<GalleryImage>();

            foreach (var uploadedFile in model.Files)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await uploadedFile.CopyToAsync(memoryStream);
                    var imageBytes = memoryStream.ToArray();
                    var largeImage = imageService.ConvertToLargeGalleryImage(imageBytes);

                    var file = new GalleryImage(name: uploadedFile.FileName,
                        contentType: largeImage.contentType,
                        content: largeImage.fileBytes,
                        propertyId: model.PropertyId);

                    galleryImages.Add(file);
                }
            }

            await galleryImageRepository.AddAsync(galleryImages);

            var readFiles = new List<ReadFile>();

            foreach (var image in galleryImages)
            {
                var smallImage = imageService.ConvertToSmallGalleryImage(image.Content);

                readFiles.Add(new ReadFile(id: image.Id,
                    name: image.Name,
                    bytes: smallImage.fileBytes,
                    mimeType: smallImage.contentType,
                    createdDate: image.CreatedDate));
            }

            return readFiles;
        }

        public async Task DeleteAsync(long galleryImageId, long propertyId)
        {
            var loggedInUser = membershipService.GetLoggedInUser();
            var galleryImage = await galleryImageRepository.GetAsync(galleryImageId, propertyId, loggedInUser.Id);

            if (galleryImage == null)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid gallery image ID or property type ID.") });

            await galleryImageRepository.DeleteAsync(galleryImage);
        }
    }
}