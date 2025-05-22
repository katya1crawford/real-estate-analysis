using FluentValidation;
using FluentValidation.Results;
using RealEstateAnalysis.Data.Abstract;
using RealEstateAnalysis.Data.Abstract.RentalProperty;
using RealEstateAnalysis.Data.Entities.RentalProperty;
using RealEstateAnalysis.Domain.Abstract;
using RealEstateAnalysis.Domain.Abstract.RentalProperty;
using RealEstateAnalysis.Domain.DTOs.Reads;
using RealEstateAnalysis.Domain.DTOs.Reads.RentalProperty;
using RealEstateAnalysis.Domain.DTOs.Writes;
using RealEstateAnalysis.Domain.DTOs.Writes.RentalProperty;
using RealEstateAnalysis.Domain.Enums;
using RealEstateAnalysis.Domain.Validators;
using RealEstateAnalysis.Domain.Validators.RentalProperty;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using File = RealEstateAnalysis.Data.Entities.RentalProperty.File;
using GooglePlacesDTOs = RealEstateAnalysis.Domain.DTOs.ThirdParty.Google.Places;

namespace RealEstateAnalysis.Domain.Services.RentalProperty
{
    public class PropertyService : IPropertyService
    {
        private readonly ICalculatorService calculatorService;
        private readonly IGoogleGeocodeApiService googleGeocodeApiService;
        private readonly IGooglePlaceApiService googlePlaceApiService;
        private readonly IImageService imageService;
        private readonly ILookupRepository lookupRepository;
        private readonly IMembershipService membershipService;
        private readonly IPropertyRepository propertyRepository;

        public PropertyService(IPropertyRepository propertyRepository,
            ILookupRepository lookupRepository,
            ICalculatorService calculatorService,
            IMembershipService membershipService,
            IGoogleGeocodeApiService googleGeocodeApiService,
            IImageService imageService,
            IGooglePlaceApiService googlePlaceApiService)
        {
            this.propertyRepository = propertyRepository;
            this.lookupRepository = lookupRepository;
            this.calculatorService = calculatorService;
            this.membershipService = membershipService;
            this.googleGeocodeApiService = googleGeocodeApiService;
            this.imageService = imageService;
            this.googlePlaceApiService = googlePlaceApiService;
        }

        public async Task<ReadProperty> AddAsync(WriteProperty model)
        {
            var validator = new PropertyValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var loggedInUser = membershipService.GetLoggedInUser();
            var geocode = await googleGeocodeApiService.GetGeocodeAsync(model.Address);

            if (googleGeocodeApiService.AddressIsValid(geocode.Places) == false)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid property address.") });

            var state = await lookupRepository.GetStateByIdAsync(model.Address.StateId);

            if (state == null)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid state ID.") });

            var address = googleGeocodeApiService.GeocodeToReadAddress(geocode.Places[0], state);

            var propertyType = await lookupRepository.GetPropertyTypeByIdAsync(model.PropertyTypeId);

            if (propertyType == null)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid property type ID.") });

            var propertyStatus = await lookupRepository.GetPropertyStatusByIdAsync(model.PropertyStatusId);

            if (propertyStatus == null)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid property status ID.") });

            var unitGroups = new List<UnitGroup>();
            model.UnitGroups.Where(x => x.NumberOfUnits != 0).ToList()
                .ForEach(x => unitGroups.Add(new UnitGroup(numberOfUnits: x.NumberOfUnits,
                    bedrooms: x.Bedrooms,
                    bathrooms: x.Bathrooms,
                    squareFootage: x.SquareFootage)));

            var operatingExpenseTypes = await lookupRepository.GetAllOperatingExpenseTypesAsync();
            var annualOperatingExpenses = new List<AnnualOperatingExpense>();
            var annualOperatingExpensesGroups = model.AnnualOperatingExpenses
                .Where(x => x.Amount != 0)
                .GroupBy(x => x.OperatingExpenseTypeId)
                .ToList();

            foreach (var operatingExpenseGroup in annualOperatingExpensesGroups)
            {
                var operatingExpenseType = operatingExpenseTypes.FirstOrDefault(x => x.Id == operatingExpenseGroup.Key);

                if (operatingExpenseType == null)
                    throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid operating expense type ID.") });

                annualOperatingExpenses.Add(new AnnualOperatingExpense(amount: operatingExpenseGroup.Sum(y => y.Amount),
                    operatingExpenseType: operatingExpenseType));
            }

            var closingCostTypes = await lookupRepository.GetAllClosingCostTypesAsync();
            var closingCosts = new List<ClosingCost>();
            var closingCostsGroups = model.ClosingCosts
                .Where(x => x.Amount != 0)
                .GroupBy(x => x.ClosingCostTypeId)
                .ToList();

            foreach (var closingCostsGroup in closingCostsGroups)
            {
                var closingCostType = closingCostTypes.FirstOrDefault(x => x.Id == closingCostsGroup.Key);

                if (closingCostType == null)
                    throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid closing cost type ID.") });

                closingCosts.Add(new ClosingCost(amount: closingCostsGroup.Sum(y => y.Amount),
                   closingCostType: closingCostType));
            }

            var interiorRepairExpenseTypes = await lookupRepository.GetAllInteriorRepairExpenseTypesAsync();
            var interiorRepairExpenses = new List<InteriorRepairExpense>();
            var interiorRepairExpensesGroups = model.InteriorRepairExpenses
                .Where(x => x.Amount != 0)
                .GroupBy(x => x.InteriorRepairExpenseTypeId)
                .ToList();

            foreach (var interiorRepairExpensesGroup in interiorRepairExpensesGroups)
            {
                var interiorRepairExpenseType = interiorRepairExpenseTypes.FirstOrDefault(x => x.Id == interiorRepairExpensesGroup.Key);

                if (interiorRepairExpenseType == null)
                    throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid interior repair expense type ID.") });

                interiorRepairExpenses.Add(new InteriorRepairExpense(amount: interiorRepairExpensesGroup.Sum(y => y.Amount),
                   interiorRepairExpenseType: interiorRepairExpenseType));
            }

            var exteriorRepairExpenseTypes = await lookupRepository.GetAllExteriorRepairExpenseTypesAsync();
            var exteriorRepairExpenses = new List<ExteriorRepairExpense>();
            var exteriorRepairExpensesGroups = model.ExteriorRepairExpenses
                .Where(x => x.Amount != 0)
                .GroupBy(x => x.ExteriorRepairExpenseTypeId)
                .ToList();

            foreach (var exteriorRepairExpensesGroup in exteriorRepairExpensesGroups)
            {
                var exteriorRepairExpenseType = exteriorRepairExpenseTypes.FirstOrDefault(x => x.Id == exteriorRepairExpensesGroup.Key);

                if (exteriorRepairExpenseType == null)
                    throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid exterior repair expense type ID.") });

                exteriorRepairExpenses.Add(new ExteriorRepairExpense(amount: exteriorRepairExpensesGroup.Sum(y => y.Amount),
                   exteriorRepairExpenseType: exteriorRepairExpenseType));
            }

            var generalRepairExpenseTypes = await lookupRepository.GetAllGeneralRepairExpenseTypesAsync();
            var generalRepairExpenses = new List<GeneralRepairExpense>();
            var generalRepairExpensesGroups = model.GeneralRepairExpenses
                .Where(x => x.Amount != 0)
                .GroupBy(x => x.GeneralRepairExpenseTypeId)
                .ToList();

            foreach (var generalRepairExpensesGroup in generalRepairExpensesGroups)
            {
                var generalRepairExpenseType = generalRepairExpenseTypes.FirstOrDefault(x => x.Id == generalRepairExpensesGroup.Key);

                if (generalRepairExpenseType == null)
                    throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid general repair expense type ID.") });

                generalRepairExpenses.Add(new GeneralRepairExpense(amount: generalRepairExpensesGroup.Sum(y => y.Amount),
                   generalRepairExpenseType: generalRepairExpenseType));
            }

            byte[] thumbnailImage = default;
            string thumbnailImageContentType = default;

            if (model.ThumbnailImage != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await model.ThumbnailImage.CopyToAsync(memoryStream);
                    (var FileBytes, var ContentType) = imageService.ConvertToThumbnailImage(memoryStream.ToArray());

                    thumbnailImage = FileBytes;
                    thumbnailImageContentType = ContentType;
                }
            }

            var newProperty = new Property(address: address.Address,
                city: address.City,
                state: state,
                zipCode: address.ZipCode,
                latitude: address.Latitude,
                longitude: address.Longitude,
                neighborhood: address.Neighborhood,
                county: address.County,
                propertyType: propertyType,
                propertyStatus: propertyStatus,
                yearBuiltIn: model.YearBuiltIn,
                lotSquareFootage: model.LotSquareFootage,
                buildingSquareFootage: model.BuildingSquareFootage,
                purchasePrice: Math.Round(model.PurchasePrice, 2),
                downPayment: Math.Round(model.DownPayment, 2),
                annualGrossScheduledRentalIncome: Math.Round(model.AnnualGrossScheduledRentalIncome, 2),
                otherAnnualIncome: Math.Round(model.OtherAnnualIncome, 2),
                annualVacancyRate: Math.Round(model.AnnualVacancyRate, 2),
                annualPropertyManagementFeeRate: Math.Round(model.AnnualPropertyManagementFeeRate, 2),
                loanApr: Math.Round(model.LoanApr, 2),
                loanYears: model.LoanYears,
                notes: model.Notes,
                unitGroups: unitGroups,
                closingCosts: closingCosts,
                interiorRepairExpenses: interiorRepairExpenses,
                exteriorRepairExpenses: exteriorRepairExpenses,
                generalRepairExpenses: generalRepairExpenses,
                annualOperatingExpenses: annualOperatingExpenses,
                annualGrossScheduledRentalIncomeGrowthRate: Math.Round(model.AnnualGrossScheduledRentalIncomeGrowthRate, 2),
                marketCapitalizationRate: Math.Round(model.MarketCapitalizationRate, 2),
                annualOperatingExpensesGrowthRate: Math.Round(model.AnnualOperatingExpensesGrowthRate, 2),
                thumbnailImage: thumbnailImage,
                thumbnailImageContentType: thumbnailImageContentType,
                userId: loggedInUser.Id,
                reportGroupName: model.GroupName?.Trim().ToUpper());

            await propertyRepository.SaveOrUpdateAsync(newProperty);
            var readProperty = GetReadProperty(newProperty);
            return readProperty;
        }

        public async Task<List<ReadFile>> AddFilesAsync(WriteFiles model)
        {
            var validator = new FilesValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var loggedInUser = membershipService.GetLoggedInUser();
            var property = await propertyRepository.GetByIdAsync(model.PropertyId, loggedInUser.Id);

            if (property == null)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid property ID.") });

            var files = new List<File>();

            foreach (var uploadedFile in model.Files)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await uploadedFile.CopyToAsync(memoryStream);
                    var file = new File(name: uploadedFile.FileName,
                        contentType: uploadedFile.ContentType,
                        content: memoryStream.ToArray());
                    files.Add(file);
                }
            }

            property.AddFiles(files);
            await propertyRepository.SaveOrUpdateAsync(property);
            var readFiles = property.Files.Select(x => new ReadFile(id: x.Id,
                name: x.Name,
                bytes: default,
                mimeType: x.ContentType,
                createdDate: x.CreatedDate)).ToList();

            return readFiles;
        }

        public async Task DeleteAsync(long propertyId)
        {
            var loggedInUser = membershipService.GetLoggedInUser();
            var property = await propertyRepository.GetByIdAsync(propertyId, loggedInUser.Id);

            if (property == null)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid property ID.") });

            await propertyRepository.DeleteAsync(property);
        }

        public async Task DeleteFileAsync(long propertyId, long fileId)
        {
            var loggedInUser = membershipService.GetLoggedInUser();
            var property = await propertyRepository.GetByIdAsync(propertyId, loggedInUser.Id);

            if (property == null)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid property ID.") });

            var fileToRemove = property.Files.FirstOrDefault(x => x.Id == fileId);

            if (fileToRemove == null)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid file ID.") });

            property.RemoveFile(fileToRemove.Id);
            await propertyRepository.SaveOrUpdateAsync(property);
        }

        public async Task DeleteThumbnailImageAsync(long propertyId)
        {
            var loggedInUser = membershipService.GetLoggedInUser();
            var property = await propertyRepository.GetByIdAsync(propertyId, loggedInUser.Id);

            if (property == null)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid property ID.") });

            property.RemoveThumbnailImage();
            await propertyRepository.SaveOrUpdateAsync(property);
        }

        public async Task<List<ReadProperty>> GetByStatus(PropertyStatusEnum propertyStatus)
        {
            var loggedInUser = membershipService.GetLoggedInUser();
            var allProperties = await propertyRepository.GetByStatusId(loggedInUser.Id, (long)propertyStatus, asNoTracking: true);

            var readProperties = new List<ReadProperty>();

            foreach (var property in allProperties)
            {
                var readProperty = GetReadProperty(property);
                readProperties.Add(readProperty);
            }

            return readProperties.ToList();
        }

        public async Task<List<string>> GetAllGroupNamesAsync()
        {
            var loggedInUser = membershipService.GetLoggedInUser();
            return await propertyRepository.GetAllGroupNamesAsync(loggedInUser.Id);
        }

        public async Task<List<ReadLookup>> GetAllSubjectPropertyLookupsAsync()
        {
            var loggedInUser = membershipService.GetLoggedInUser();
            var subjectProperties = await propertyRepository.GetAllSubjectPropertyLookupsAsync(loggedInUser.Id);
            return subjectProperties.Select(x => new ReadLookup(x.Key, x.Value)).ToList();
        }

        public async Task<List<ReadProperty>> GetByGroupNameAsync(string groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName))
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Report group name is required.") });

            var loggedInUser = membershipService.GetLoggedInUser();
            var properties = await propertyRepository.GetByGroupNameAsync(groupName, loggedInUser.Id, asNoTracking: true);

            var readProperties = new List<ReadProperty>();

            foreach (var property in properties)
            {
                var readProperty = GetReadProperty(property);
                readProperties.Add(readProperty);
            }

            return readProperties.ToList();
        }

        public async Task<ReadProperty> GetByIdAsync(long id, bool includeNearbyPlaces)
        {
            var loggedInUser = membershipService.GetLoggedInUser();
            var property = await propertyRepository.GetByIdAsync(id, loggedInUser.Id, asNoTracking: true);

            if (property == null)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid property ID.") });

            GooglePlacesDTOs.ReadNearbySearch nearbyGroceryOrSupermarkets = null;
            GooglePlacesDTOs.ReadNearbySearch nearbyStarbuckses = null;
            GooglePlacesDTOs.ReadNearbySearch nearbyPawnShops = null;
            GooglePlacesDTOs.ReadNearbySearch nearbyCheckCashingPlaces = null;

            if (includeNearbyPlaces)
            {
                double radiusInMiles = 3;

                var nearbyGroceryOrSupermarketsTask = googlePlaceApiService.GetNearbyGroceryOrSupermarkets(radiusInMiles: radiusInMiles,
                    nearbyLat: property.Latitude,
                    nearbyLong: property.Longitude);

                var nearbyStarbucksesTask = googlePlaceApiService.GetNearbyStarbuckses(radiusInMiles: radiusInMiles,
                    nearbyLat: property.Latitude,
                    nearbyLong: property.Longitude);

                var nearbyPawnShopsTask = googlePlaceApiService.GetNearbyPawnShops(radiusInMiles: radiusInMiles,
                    nearbyLat: property.Latitude,
                    nearbyLong: property.Longitude);

                var nearbyCheckCashingPlacesTask = googlePlaceApiService.GetNearbyCheckCashingPlaces(radiusInMiles: radiusInMiles,
                    nearbyLat: property.Latitude,
                    nearbyLong: property.Longitude);

                await Task.WhenAll(nearbyGroceryOrSupermarketsTask, nearbyStarbucksesTask, nearbyPawnShopsTask, nearbyCheckCashingPlacesTask);

                nearbyGroceryOrSupermarkets = nearbyGroceryOrSupermarketsTask.Result;
                nearbyStarbuckses = nearbyStarbucksesTask.Result;
                nearbyPawnShops = nearbyPawnShopsTask.Result;
                nearbyCheckCashingPlaces = nearbyCheckCashingPlacesTask.Result;
            }

            var propertyDto = GetReadProperty(property,
                ToReadReadNearbyPlaces(nearbyGroceryOrSupermarkets),
                ToReadReadNearbyPlaces(nearbyStarbuckses),
                ToReadReadNearbyPlaces(nearbyPawnShops),
                ToReadReadNearbyPlaces(nearbyCheckCashingPlaces));
            return propertyDto;
        }

        public async Task<ReadProperty> UpdateAsync(long id, WriteProperty model)
        {
            var validator = new PropertyValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var loggedInUser = membershipService.GetLoggedInUser();
            var existingProperty = await propertyRepository.GetByIdAsync(id, loggedInUser.Id);

            if (existingProperty == null)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid property ID.") });

            var geocode = await googleGeocodeApiService.GetGeocodeAsync(model.Address);

            if (googleGeocodeApiService.AddressIsValid(geocode.Places) == false)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid property address.") });

            var state = await lookupRepository.GetStateByIdAsync(model.Address.StateId);
            var address = googleGeocodeApiService.GeocodeToReadAddress(geocode.Places[0], state);

            if (state == null)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid state ID.") });

            var propertyType = await lookupRepository.GetPropertyTypeByIdAsync(model.PropertyTypeId);

            if (propertyType == null)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid property type ID.") });

            var propertyStatus = await lookupRepository.GetPropertyStatusByIdAsync(model.PropertyStatusId);

            if (propertyStatus == null)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid property status ID.") });

            var unitGroupIdsToRemove = existingProperty.UnitGroups
                .Select(x => x.Id)
                .Where(x => model.UnitGroups.Select(y => y.Id).Contains(x) == false)
                .ToList();

            if (unitGroupIdsToRemove?.Count() > 0)
                existingProperty.RemoveUnitGroups(unitGroupIdsToRemove);

            model.UnitGroups.Where(x => x.NumberOfUnits != 0).ToList()
                .ForEach(x =>
                {
                    if (x.Id == default)
                        existingProperty.AddUnitGroup(new UnitGroup(numberOfUnits: x.NumberOfUnits, bedrooms: x.Bedrooms, bathrooms: x.Bathrooms, squareFootage: x.SquareFootage));
                    else
                    {
                        var existingUnitGroup = existingProperty.UnitGroups.FirstOrDefault(y => y.Id == x.Id);

                        if (existingUnitGroup == null)
                            throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid unit group ID.") });

                        existingUnitGroup.Update(numberOfUnits: x.NumberOfUnits, bedrooms: x.Bedrooms, bathrooms: x.Bathrooms, squareFootage: x.SquareFootage);
                    }
                });

            var operatingExpenseTypes = await lookupRepository.GetAllOperatingExpenseTypesAsync();
            var annualOperatingExpensesGroups = model.AnnualOperatingExpenses
                .Where(x => x.Amount != 0)
                .GroupBy(x => x.OperatingExpenseTypeId)
                .ToList();

            var annualOperatingExpenseTypeIdsToRemove = existingProperty.AnnualOperatingExpenses
                .Select(x => x.OperatingExpenseType.Id)
                .Where(x => annualOperatingExpensesGroups.Select(y => y.Key).Contains(x) == false)
                .ToList();

            var annualOperatingExpenseIdsToRemove = existingProperty.AnnualOperatingExpenses
                .Where(x => annualOperatingExpenseTypeIdsToRemove.Contains(x.OperatingExpenseType.Id))
                .Select(x => x.Id)
                .ToList();

            if (annualOperatingExpenseIdsToRemove?.Count() > 0)
                existingProperty.RemoveAnnualOperatingExpenses(annualOperatingExpenseIdsToRemove);

            foreach (var annualOperatingExpensesGroup in annualOperatingExpensesGroups)
            {
                var operatingExpenseType = operatingExpenseTypes.FirstOrDefault(x => x.Id == annualOperatingExpensesGroup.Key);

                if (operatingExpenseType == null)
                    throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid operating expense type ID.") });

                var existingAnnualOperatingExpense = existingProperty.AnnualOperatingExpenses.FirstOrDefault(x => x.OperatingExpenseType.Id == operatingExpenseType.Id);

                if (existingAnnualOperatingExpense == null)
                    existingProperty.AddAnnualOperatingExpense(new AnnualOperatingExpense(amount: annualOperatingExpensesGroup.Sum(x => x.Amount), operatingExpenseType: operatingExpenseType));
                else
                    existingAnnualOperatingExpense.Update(amount: annualOperatingExpensesGroup.Sum(x => x.Amount));
            }

            var closingCostTypes = await lookupRepository.GetAllClosingCostTypesAsync();
            var closingCostsGroups = model.ClosingCosts
                .Where(x => x.Amount != 0)
                .GroupBy(x => x.ClosingCostTypeId)
                .ToList();

            var closingCostTypeIdsToRemove = existingProperty.ClosingCosts
                .Select(x => x.ClosingCostType.Id)
                .Where(x => closingCostsGroups.Select(y => y.Key).Contains(x) == false)
                .ToList();

            var closingCostIdsToRemove = existingProperty.ClosingCosts
                .Where(x => closingCostTypeIdsToRemove.Contains(x.ClosingCostType.Id))
                .Select(x => x.Id)
                .ToList();

            if (closingCostIdsToRemove?.Count() > 0)
                existingProperty.RemoveClosingCosts(closingCostIdsToRemove);

            foreach (var closingCostsGroup in closingCostsGroups)
            {
                var closingCostsGroupType = closingCostTypes.FirstOrDefault(x => x.Id == closingCostsGroup.Key);

                if (closingCostsGroupType == null)
                    throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid closing cost type ID.") });

                var existingClosingCost = existingProperty.ClosingCosts.FirstOrDefault(x => x.ClosingCostType.Id == closingCostsGroupType.Id);

                if (existingClosingCost == null)
                    existingProperty.AddClosingCost(new ClosingCost(amount: closingCostsGroup.Sum(x => x.Amount), closingCostType: closingCostsGroupType));
                else
                    existingClosingCost.Update(amount: closingCostsGroup.Sum(x => x.Amount));
            }

            var exteriorRepairExpenseTypes = await lookupRepository.GetAllExteriorRepairExpenseTypesAsync();
            var exteriorRepairExpensesGroups = model.ExteriorRepairExpenses
                .Where(x => x.Amount != 0)
                .GroupBy(x => x.ExteriorRepairExpenseTypeId)
                .ToList();

            var exteriorRepairExpenseTypeIdsToRemove = existingProperty.ExteriorRepairExpenses
                .Select(x => x.ExteriorRepairExpenseType.Id)
                .Where(x => exteriorRepairExpensesGroups.Select(y => y.Key).Contains(x) == false)
                .ToList();

            var exteriorRepairExpenseIdsToRemove = existingProperty.ExteriorRepairExpenses
                .Where(x => exteriorRepairExpenseTypeIdsToRemove.Contains(x.ExteriorRepairExpenseType.Id))
                .Select(x => x.Id)
                .ToList();

            if (exteriorRepairExpenseIdsToRemove?.Count() > 0)
                existingProperty.RemoveExteriorRepairExpenses(exteriorRepairExpenseIdsToRemove);

            foreach (var exteriorRepairExpensesGroup in exteriorRepairExpensesGroups)
            {
                var exteriorRepairExpenseType = exteriorRepairExpenseTypes.FirstOrDefault(x => x.Id == exteriorRepairExpensesGroup.Key);

                if (exteriorRepairExpenseType == null)
                    throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid exterior repair expense type ID.") });

                var existingExteriorRepairExpense = existingProperty.ExteriorRepairExpenses.FirstOrDefault(x => x.ExteriorRepairExpenseType.Id == exteriorRepairExpenseType.Id);

                if (existingExteriorRepairExpense == null)
                    existingProperty.AddExteriorRepairExpense(new ExteriorRepairExpense(amount: exteriorRepairExpensesGroup.Sum(x => x.Amount), exteriorRepairExpenseType: exteriorRepairExpenseType));
                else
                    existingExteriorRepairExpense.Update(amount: exteriorRepairExpensesGroup.Sum(x => x.Amount));
            }

            var interiorRepairExpenseTypes = await lookupRepository.GetAllInteriorRepairExpenseTypesAsync();
            var interiorRepairExpensesGroups = model.InteriorRepairExpenses
                .Where(x => x.Amount != 0)
                .GroupBy(x => x.InteriorRepairExpenseTypeId)
                .ToList();

            var interiorRepairExpenseTypeIdsToRemove = existingProperty.InteriorRepairExpenses
                .Select(x => x.InteriorRepairExpenseType.Id)
                .Where(x => interiorRepairExpensesGroups.Select(y => y.Key).Contains(x) == false)
                .ToList();

            var interiorRepairExpenseIdsToRemove = existingProperty.InteriorRepairExpenses
                .Where(x => interiorRepairExpenseTypeIdsToRemove.Contains(x.InteriorRepairExpenseType.Id))
                .Select(x => x.Id)
                .ToList();

            if (interiorRepairExpenseIdsToRemove?.Count() > 0)
                existingProperty.RemoveInteriorRepairExpenses(interiorRepairExpenseIdsToRemove);

            foreach (var interiorRepairExpensesGroup in interiorRepairExpensesGroups)
            {
                var interiorRepairExpenseType = interiorRepairExpenseTypes.FirstOrDefault(x => x.Id == interiorRepairExpensesGroup.Key);

                if (interiorRepairExpenseType == null)
                    throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid interior repair expense type ID.") });

                var existingInteriorRepairExpense = existingProperty.InteriorRepairExpenses.FirstOrDefault(x => x.InteriorRepairExpenseType.Id == interiorRepairExpenseType.Id);

                if (existingInteriorRepairExpense == null)
                    existingProperty.AddInteriorRepairExpense(new InteriorRepairExpense(amount: interiorRepairExpensesGroup.Sum(x => x.Amount), interiorRepairExpenseType: interiorRepairExpenseType));
                else
                    existingInteriorRepairExpense.Update(amount: interiorRepairExpensesGroup.Sum(x => x.Amount));
            }

            var generalRepairExpenseTypes = await lookupRepository.GetAllGeneralRepairExpenseTypesAsync();
            var generalRepairExpensesGroups = model.GeneralRepairExpenses
                .Where(x => x.Amount != 0)
                .GroupBy(x => x.GeneralRepairExpenseTypeId)
                .ToList();

            var generalRepairExpenseTypeIdsToRemove = existingProperty.GeneralRepairExpenses
                .Select(x => x.GeneralRepairExpenseType.Id)
                .Where(x => generalRepairExpensesGroups.Select(y => y.Key).Contains(x) == false)
                .ToList();

            var generalRepairExpenseIdsToRemove = existingProperty.GeneralRepairExpenses
                .Where(x => generalRepairExpenseTypeIdsToRemove.Contains(x.GeneralRepairExpenseType.Id))
                .Select(x => x.Id)
                .ToList();

            if (generalRepairExpenseIdsToRemove?.Count() > 0)
                existingProperty.RemoveGeneralRepairExpenses(generalRepairExpenseIdsToRemove);

            foreach (var generalRepairExpensesGroup in generalRepairExpensesGroups)
            {
                var generalRepairExpenseType = generalRepairExpenseTypes.FirstOrDefault(x => x.Id == generalRepairExpensesGroup.Key);

                if (generalRepairExpenseType == null)
                    throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid general repair expense type ID.") });

                var existingGeneralExpense = existingProperty.GeneralRepairExpenses.FirstOrDefault(x => x.GeneralRepairExpenseType.Id == generalRepairExpenseType.Id);

                if (existingGeneralExpense == null)
                    existingProperty.AddGeneralRepairExpense(new GeneralRepairExpense(amount: generalRepairExpensesGroup.Sum(x => x.Amount), generalRepairExpenseType: generalRepairExpenseType));
                else
                    existingGeneralExpense.Update(amount: generalRepairExpensesGroup.Sum(x => x.Amount));
            }

            var thumbnailImage = existingProperty.ThumbnailImage;
            var thumbnailImageContentType = existingProperty.ThumbnailImageContentType;

            if (model.ThumbnailImage != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await model.ThumbnailImage.CopyToAsync(memoryStream);
                    (var FileBytes, var ContentType) = imageService.ConvertToThumbnailImage(memoryStream.ToArray());

                    thumbnailImage = FileBytes;
                    thumbnailImageContentType = ContentType;
                }
            }

            existingProperty.Update(address: address.Address,
                city: address.City,
                state: state,
                zipCode: address.ZipCode,
                latitude: address.Latitude,
                longitude: address.Longitude,
                neighborhood: address.Neighborhood,
                county: address.County,
                propertyType: propertyType,
                propertyStatus: propertyStatus,
                yearBuiltIn: model.YearBuiltIn,
                lotSquareFootage: model.LotSquareFootage,
                buildingSquareFootage: model.BuildingSquareFootage,
                purchasePrice: Math.Round(model.PurchasePrice, 2),
                downPayment: Math.Round(model.DownPayment, 2),
                annualGrossScheduledRentalIncome: Math.Round(model.AnnualGrossScheduledRentalIncome, 2),
                otherAnnualIncome: Math.Round(model.OtherAnnualIncome, 2),
                annualVacancyRate: Math.Round(model.AnnualVacancyRate, 2),
                annualPropertyManagementFeeRate: Math.Round(model.AnnualPropertyManagementFeeRate, 2),
                loanApr: Math.Round(model.LoanApr, 2),
                loanYears: model.LoanYears,
                notes: model.Notes,
                annualGrossScheduledRentalIncomeGrowthRate: Math.Round(model.AnnualGrossScheduledRentalIncomeGrowthRate, 2),
                annualOperatingExpensesGrowthRate: Math.Round(model.AnnualOperatingExpensesGrowthRate, 2),
                marketCapitalizationRate: Math.Round(model.MarketCapitalizationRate, 2),
                thumbnailImage: thumbnailImage,
                thumbnailImageContentType: thumbnailImageContentType,
                reportGroupName: model.GroupName?.Trim().ToUpper());

            await propertyRepository.SaveOrUpdateAsync(existingProperty);
            var readProperty = GetReadProperty(existingProperty);
            return readProperty;
        }

        private ReadProperty GetReadProperty(Property property,
            List<ReadNearbyPlace> nearbyGroceryOrSupermarkets = null,
            List<ReadNearbyPlace> nearbyStarbuckses = null,
            List<ReadNearbyPlace> nearbyPawnShops = null,
            List<ReadNearbyPlace> nearbyCheckCashingPlaces = null)
        {
            var annualFinancialDetailsModel = new WriteFinancialSummary(purchasePrice: property.PurchasePrice,
                downPayment: property.DownPayment,
                annualGrossScheduledRentalIncome: property.AnnualGrossScheduledRentalIncome,
                otherAnnualIncome: property.OtherAnnualIncome,
                annualVacancyRate: property.AnnualVacancyRate,
                annualPropertyManagementFeeRate: property.AnnualPropertyManagementFeeRate,
                loanApr: property.LoanApr,
                loanYears: property.LoanYears,
                annualGrossScheduledRentalIncomeGrowthRate: property.AnnualGrossScheduledRentalIncomeGrowthRate,
                annualOperatingExpensesGrowthRate: property.AnnualOperatingExpensesGrowthRate,
                marketCapitalizationRate: property.MarketCapitalizationRate,
                closingCosts: property.ClosingCosts.Select(x => new WriteClosingCost(closingCostTypeId: x.ClosingCostType.Id, amount: x.Amount)).ToList(),
                annualOperatingExpenses: property.AnnualOperatingExpenses.Select(x => new WriteAnnualOperatingExpense(operatingExpenseTypeId: x.OperatingExpenseType.Id, amount: x.Amount)).ToList(),
                exteriorRepairExpenses: property.ExteriorRepairExpenses.Select(x => new WriteExteriorRepairExpense(exteriorRepairExpenseTypeId: x.ExteriorRepairExpenseType.Id, amount: x.Amount)).ToList(),
                generalRepairExpenses: property.GeneralRepairExpenses.Select(x => new WriteGeneralRepairExpense(generalRepairExpenseTypeId: x.GeneralRepairExpenseType.Id, amount: x.Amount)).ToList(),
                interiorRepairExpenses: property.InteriorRepairExpenses.Select(x => new WriteInteriorRepairExpense(interiorRepairExpenseTypeId: x.InteriorRepairExpenseType.Id, amount: x.Amount)).ToList());

            var financialDetails = calculatorService.GetFinancialSummary(annualFinancialDetailsModel);
            return new ReadProperty(property,
                financialDetails,
                nearbyGroceryOrSupermarkets,
                nearbyStarbuckses,
                nearbyPawnShops,
                nearbyCheckCashingPlaces);
        }

        private List<ReadNearbyPlace> ToReadReadNearbyPlaces(GooglePlacesDTOs.ReadNearbySearch source)
        {
            if (source?.Places?.Count() > 0)
            {
                return source.Places
                    .GroupBy(x => x.Vicinity)
                    .Select(x => new ReadNearbyPlace(name: x.First().Name,
                          latitude: x.First().Geometry.Location.Latitude,
                          longitude: x.First().Geometry.Location.Longitude))
                    .ToList();
            }
            else
            {
                return new List<ReadNearbyPlace>();
            }
        }
    }
}