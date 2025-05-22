using FluentValidation;
using FluentValidation.Results;
using RealEstateAnalysis.Data.Abstract.RentalProperty;
using RealEstateAnalysis.Domain.Abstract;
using RealEstateAnalysis.Domain.Abstract.RentalProperty;
using RealEstateAnalysis.Domain.DTOs.Reads;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Domain.Services.RentalProperty
{
    public class FileContentService : IFileContentService
    {
        private readonly IMembershipService membershipService;
        private readonly IPropertyRepository propertyRepository;
        private readonly IFileContentRepository fileContentRepository;

        public FileContentService(IMembershipService membershipService, IPropertyRepository propertyRepository, IFileContentRepository fileContentRepository)
        {
            this.membershipService = membershipService;
            this.propertyRepository = propertyRepository;
            this.fileContentRepository = fileContentRepository;
        }

        public async Task<ReadFile> GetAsync(long propertyId, long fileId)
        {
            var loggedInUser = membershipService.GetLoggedInUser();
            var property = await propertyRepository.GetByIdAsync(propertyId, loggedInUser.Id);

            if (property == null)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid property ID.") });

            var file = property.Files.FirstOrDefault(x => x.Id == fileId);

            if (file == null)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid file ID.") });

            var fileContent = await fileContentRepository.GetAsync(file.Id);

            return new ReadFile(id: file.Id,
                name: file.Name,
                bytes: fileContent,
                mimeType: file.ContentType,
                createdDate: file.CreatedDate);
        }
    }
}