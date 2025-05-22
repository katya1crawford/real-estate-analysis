using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;
using RealEstateAnalysis.Data.Abstract;
using RealEstateAnalysis.Data.Abstract.Reonomy;
using RealEstateAnalysis.Data.Entities.Lookups;
using RealEstateAnalysis.Domain.Abstract;
using RealEstateAnalysis.Domain.DTOs.Reads;
using RealEstateAnalysis.Domain.DTOs.Writes;
using RealEstateAnalysis.Domain.Enums;
using RealEstateAnalysis.Domain.Validators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GoogleGeocodeDTOs = RealEstateAnalysis.Domain.DTOs.ThirdParty.Google.Geocode;
using GooglePlacesDTOs = RealEstateAnalysis.Domain.DTOs.ThirdParty.Google.Places;
using ReonomyDTOs = RealEstateAnalysis.Domain.DTOs.ThirdParty.Reonomy;
using ReonomyEntities = RealEstateAnalysis.Data.Entities.Reonomy;

namespace RealEstateAnalysis.Domain.Services
{
    public class SoldPropertyService : ISoldPropertyService
    {
        private readonly ISoldPropertyRepository soldPropertyRepository;
        private readonly IGoogleGeocodeApiService googleGeocodeApiService;
        private readonly IGooglePlaceApiService googlePlaceApiService;
        private readonly IZipwiseApiService zipwiseApiService;
        private readonly ILookupRepository lookupRepository;

        public SoldPropertyService(ISoldPropertyRepository soldPropertyRepository,
            ILookupRepository lookupRepository,
            IZipwiseApiService zipwiseApiService,
            IGoogleGeocodeApiService googleGeocodeApiService,
            IGooglePlaceApiService googlePlaceApiService)
        {
            this.soldPropertyRepository = soldPropertyRepository;
            this.lookupRepository = lookupRepository;
            this.googleGeocodeApiService = googleGeocodeApiService;
            this.googlePlaceApiService = googlePlaceApiService;
            this.zipwiseApiService = zipwiseApiService;
        }

        public async Task LoadDataIntoDatabase()
        {
            var path = @"C:\Temp\Reonomy";
            var searchPattern = "*.json";
            var jsonFiles = Directory.GetFiles(path, searchPattern, SearchOption.TopDirectoryOnly);

            var reonomies = new List<ReonomyDTOs.Reonomy>();

            foreach (var jsonFile in jsonFiles)
            {
                try
                {
                    var reonomyJson = File.ReadAllText(jsonFile);
                    var reonomy = JsonConvert.DeserializeObject<ReonomyDTOs.Reonomy>(reonomyJson);
                    reonomies.Add(reonomy);
                }
                catch (Exception)
                {
                    throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Error reading and deserializing Reonomy Json.") });
                }
            }

            var states = await lookupRepository.GetAllStatesAsync();
            var propertyTypes = await lookupRepository.GetAllPropertyTypesAsync();
            var soldProperties = new List<ReonomyEntities.SoldProperty>();
            var existingSoldPropertySourceIds = await soldPropertyRepository.GetAllReonomySoldPropertySourceIds();
            var reonomySoldProperties = reonomies
                .SelectMany(x => x.SoldProperties)
                .Where(x => existingSoldPropertySourceIds.Contains(x.Id) == false && string.IsNullOrWhiteSpace(x.State) == false)
                .ToList();

            foreach (var reonomySoldProperty in reonomySoldProperties)
            {
                soldProperties.Add(TranslateToSoldProperties(reonomySoldProperty, states, propertyTypes));
            }

            if (soldProperties.Count() > 0)
            {
                await soldPropertyRepository.SaveOrUpdateAsync(soldProperties);
            }
        }

        public async Task UpdateSoldPropertiesWithGeocodeData()
        {
            var soldWithMissingLatLongProperties = (await soldPropertyRepository.GetAllWithMissingLatLongAsync()).ToDictionary(x => x.Id, x => x);
            var tasks = new Dictionary<long, Task<GoogleGeocodeDTOs.ReadGeocode>>();
            var counter = 0;

            try
            {
                foreach (var soldProperty in soldWithMissingLatLongProperties)
                {
                    counter++;

                    tasks.Add(key: soldProperty.Value.Id, googleGeocodeApiService.GetGeocodeAsync(
                        address: soldProperty.Value.Address,
                        city: soldProperty.Value.City,
                        stateAbbreviation: soldProperty.Value.State.Abbreviation));

                    if (counter == 45 || soldWithMissingLatLongProperties.Last().Value.Id == soldProperty.Value.Id)
                    {
                        await Task.WhenAll(tasks.Select(x => x.Value));

                        foreach (var task in tasks)
                        {
                            var isValidAddress = googleGeocodeApiService.AddressIsValid(task.Value.Result.Places);
                            var soldPropertyToUpdate = soldWithMissingLatLongProperties[task.Key];

                            if (isValidAddress)
                            {
                                var address = googleGeocodeApiService.GeocodeToReadAddress(task.Value.Result.Places[0], soldPropertyToUpdate.State);
                                soldPropertyToUpdate.UpdateAddressParts(address.Latitude, address.Longitude, address.Neighborhood, address.County);
                            }
                            else
                            {
                                soldPropertyToUpdate.MarkAddresInvalid();
                            }
                        }

                        counter = 0;
                        Thread.Sleep(2000);
                    }
                }
            }
            catch (Exception)
            {
                await soldPropertyRepository.SaveOrUpdateAsync(soldWithMissingLatLongProperties.Select(x => x.Value).ToList());
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", $"{nameof(UpdateSoldPropertiesWithGeocodeData)} failed.") });
            }

            await soldPropertyRepository.SaveOrUpdateAsync(soldWithMissingLatLongProperties.Select(x => x.Value).ToList());
        }

        public async Task<ReadSoldProprtySearchResults> Search(WriteSoldPropertiesSearch model)
        {
            var validator = new SoldPropertiesSearchValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            ReadSoldProperty subjectProperty = null;
            GooglePlacesDTOs.ReadNearbySearch nearbyGroceryOrSupermarkets = null;
            GooglePlacesDTOs.ReadNearbySearch nearbyStarbuckses = null;
            GooglePlacesDTOs.ReadNearbySearch nearbyPawnShops = null;
            GooglePlacesDTOs.ReadNearbySearch nearbyCheckCashingPlaces = null;

            if (model.SubjectProperty != null && string.IsNullOrWhiteSpace(model.SubjectProperty.Address) == false)
            {
                var geocode = await googleGeocodeApiService.GetGeocodeAsync(model.SubjectProperty);

                if (googleGeocodeApiService.AddressIsValid(geocode.Places) == false)
                    throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid subject property address.") });

                var state = await lookupRepository.GetStateByIdAsync(model.SubjectProperty.StateId);

                if (state == null)
                    throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid subject state ID.") });

                var subjectPropertyAddress = googleGeocodeApiService.GeocodeToReadAddress(geocode.Places[0], state);
                var subjectPropertySaleData = (await soldPropertyRepository.SearchAsync(new Data.DTOs.SoldPropertiesSearch
                {
                    Address = subjectPropertyAddress.Address,
                    City = subjectPropertyAddress.City,
                    StateId = subjectPropertyAddress.State.Id
                })).OrderByDescending(x => x.SalesDate).FirstOrDefault();

                if (subjectPropertySaleData == null)
                {
                    subjectProperty = new ReadSoldProperty(subjectPropertyAddress);
                }
                else
                {
                    subjectProperty = new ReadSoldProperty(subjectPropertySaleData);
                }

                double radiusInMiles = model.ZipCodeRadiusInMiles ?? 2;

                var nearbyGroceryOrSupermarketsTask = googlePlaceApiService.GetNearbyGroceryOrSupermarkets(radiusInMiles: radiusInMiles,
                    nearbyLat: subjectPropertyAddress.Latitude,
                    nearbyLong: subjectPropertyAddress.Longitude);

                var nearbyStarbucksesTask = googlePlaceApiService.GetNearbyStarbuckses(radiusInMiles: radiusInMiles,
                    nearbyLat: subjectPropertyAddress.Latitude,
                    nearbyLong: subjectPropertyAddress.Longitude);

                var nearbyPawnShopsTask = googlePlaceApiService.GetNearbyPawnShops(radiusInMiles: radiusInMiles,
                    nearbyLat: subjectPropertyAddress.Latitude,
                    nearbyLong: subjectPropertyAddress.Longitude);

                var nearbyCheckCashingPlacesTask = googlePlaceApiService.GetNearbyCheckCashingPlaces(radiusInMiles: radiusInMiles,
                    nearbyLat: subjectPropertyAddress.Latitude,
                    nearbyLong: subjectPropertyAddress.Longitude);

                await Task.WhenAll(nearbyGroceryOrSupermarketsTask, nearbyStarbucksesTask, nearbyPawnShopsTask, nearbyCheckCashingPlacesTask);

                nearbyGroceryOrSupermarkets = nearbyGroceryOrSupermarketsTask.Result;
                nearbyStarbuckses = nearbyStarbucksesTask.Result;
                nearbyPawnShops = nearbyPawnShopsTask.Result;
                nearbyCheckCashingPlaces = nearbyCheckCashingPlacesTask.Result;
            }

            var zipCodesInRadius = model.ZipCodeRadiusInMiles > 0
                ? await zipwiseApiService.GetZipCodesInRadiusAsync(model.ZipCode, model.ZipCodeRadiusInMiles.Value)
                : new List<string> { model.ZipCode };

            var soldProperties = await soldPropertyRepository.SearchAsync(new Data.DTOs.SoldPropertiesSearch
            {
                PropertyTypeId = model.PropertyTypeId.Value,
                UnitsMax = model.UnitsMax,
                UnitsMin = model.UnitsMin,
                YearMax = model.YearMax,
                YearMin = model.YearMin,
                ZipCodes = zipCodesInRadius
            });

            var readSoldProperties = soldProperties.Select(x => new ReadSoldProperty(x)).ToList();

            return new ReadSoldProprtySearchResults(soldProperties: readSoldProperties,
                subjectProperty: subjectProperty,
                nearbyGroceryOrSupermarkets: ToReadReadNearbyPlaces(nearbyGroceryOrSupermarkets),
                nearbyStarbuckses: ToReadReadNearbyPlaces(nearbyStarbuckses),
                nearbyPawnShops: ToReadReadNearbyPlaces(nearbyPawnShops),
                nearbyCheckCashingPlaces: ToReadReadNearbyPlaces(nearbyCheckCashingPlaces));
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

        private ReonomyEntities.SoldProperty TranslateToSoldProperties(ReonomyDTOs.SoldProperty source, List<State> states, List<PropertyType> propertyTypes)
        {
            var mortgageRecordingDateSuccess = DateTime.TryParse(source.MortgageRecordingDate, out var mortgageRecordingDate);
            var salesDateSuccess = DateTime.TryParse(source.SalesDate, out var salesDate);
            var state = states.First(x => x.Abbreviation == source.State.Trim().ToUpper());
            var propertyTypeEnum = GetPropertyTypeEnum(source.AssetCategory);

            return new ReonomyEntities.SoldProperty(address: $"{source.HouseNbr} {source.DirectionLeft} {source.Street} {source.Mode}".Replace("  ", " "),
                city: source.City,
                zipCode: source.Zip5,
                buildingSquareFootage: source.BuildingArea.HasValue ? Convert.ToInt32(source.BuildingArea) : default(int?),
                fips: source.Fips,
                sourceId: source.Id,
                lotSquareFootage: source.LotSizeSqft.HasValue ? Convert.ToInt32(source.LotSizeSqft) : default(int?),
                mortgageAmount: source.MortgageAmount.HasValue ? Math.Round(source.MortgageAmount.Value, 2) : default(decimal?),
                mortgageLenderName: source.MortgageLenderName,
                mortgageRecordingDate: mortgageRecordingDateSuccess ? mortgageRecordingDate : default(DateTime?),
                salesDate: salesDateSuccess ? salesDate : default(DateTime?),
                stdLandUseCodeDescription: source.StdLandUseCodeDescription,
                totalUnits: source.TotalUnits,
                yearBuilt: source.YearBuilt,
                salesPrice: source.SalesPrice.HasValue ? Math.Round(source.SalesPrice.Value, 2) : default(decimal?),
                state: state,
                propertyType: propertyTypes.First(x => x.Id == (long)propertyTypeEnum));
        }

        private PropertyTypeEnum GetPropertyTypeEnum(string propertyType)
        {
            switch (propertyType.ToLower())
            {
                case "multifamily":
                    return PropertyTypeEnum.Multifamily;

                default:
                    return PropertyTypeEnum.Multifamily;
            }
        }
    }
}