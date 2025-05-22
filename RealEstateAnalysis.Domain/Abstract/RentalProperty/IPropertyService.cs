using RealEstateAnalysis.Domain.DTOs.Reads;
using RealEstateAnalysis.Domain.DTOs.Reads.RentalProperty;
using RealEstateAnalysis.Domain.DTOs.Writes;
using RealEstateAnalysis.Domain.DTOs.Writes.RentalProperty;
using RealEstateAnalysis.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Domain.Abstract.RentalProperty
{
    public interface IPropertyService
    {
        Task<ReadProperty> AddAsync(WriteProperty model);

        Task DeleteAsync(long propertyId);

        Task<List<ReadProperty>> GetByStatus(PropertyStatusEnum propertyStatus);

        Task<List<ReadProperty>> GetByGroupNameAsync(string groupName);

        Task<ReadProperty> GetByIdAsync(long id, bool includeNearbyPlaces);

        Task<ReadProperty> UpdateAsync(long id, WriteProperty model);

        Task DeleteThumbnailImageAsync(long propertyId);

        Task<List<ReadFile>> AddFilesAsync(WriteFiles model);

        Task DeleteFileAsync(long propertyId, long fileId);

        Task<List<string>> GetAllGroupNamesAsync();

        Task<List<ReadLookup>> GetAllSubjectPropertyLookupsAsync();
    }
}