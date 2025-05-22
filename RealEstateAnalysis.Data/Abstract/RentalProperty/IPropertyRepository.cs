using RealEstateAnalysis.Data.Entities.RentalProperty;

namespace RealEstateAnalysis.Data.Abstract.RentalProperty
{
    public interface IPropertyRepository
    {
        Task DeleteAsync(Property property);

        Task<List<Property>> GetByStatusId(string userId, long propertyStatusId, bool asNoTracking = false);

        Task<List<string>> GetAllGroupNamesAsync(string userId);

        Task<List<Property>> GetByGroupNameAsync(string groupName, string userId, bool asNoTracking = false);

        Task<Property> GetByIdAsync(long id, string userId, bool asNoTracking = false);

        Task SaveOrUpdateAsync(Property property);

        Task<Dictionary<long, string>> GetAllSubjectPropertyLookupsAsync(string userId);
    }
}