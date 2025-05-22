using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RealEstateAnalysis.Data.Abstract;
using RealEstateAnalysis.Data.Abstract.LocationAnalysis;
using RealEstateAnalysis.Data.Entities.Lookups;
using RealEstateAnalysis.Domain.Abstract;
using RealEstateAnalysis.Domain.Abstract.LocationAnalysis;
using RealEstateAnalysis.Domain.DTOs.Reads;
using RealEstateAnalysis.Domain.DTOs.Reads.DataScraper;
using RealEstateAnalysis.Domain.DTOs.Reads.LocationAnalysis;
using RealEstateAnalysis.Domain.DTOs.Writes;
using RealEstateAnalysis.Domain.DTOs.Writes.LocationAnalysis;
using RealEstateAnalysis.Domain.Settings;
using RealEstateAnalysis.Domain.Validators.LocationAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using City = RealEstateAnalysis.Data.Entities.LocationAnalysis.City;

namespace RealEstateAnalysis.Domain.Services.LocationAnalysis
{
    public class CityService : ICityService
    {
        private readonly IMembershipService membershipService;
        private readonly ICityRepository cityRepository;
        private readonly ILookupRepository lookupRepository;
        private readonly LocationAnalysisSettings locationAnalysisSettings;
        private readonly IDataScraperService dataScraperService;
        private readonly IErrorLogService errorLogService;

        public CityService(IMembershipService membershipService,
            ICityRepository cityRepository,
            ILookupRepository lookupRepository,
            IOptions<LocationAnalysisSettings> locationAnalysisOptions,
            IDataScraperService dataScraperService,
            IErrorLogService errorLogService)
        {
            this.membershipService = membershipService;
            this.cityRepository = cityRepository;
            this.lookupRepository = lookupRepository;
            this.dataScraperService = dataScraperService;
            this.errorLogService = errorLogService;
            locationAnalysisSettings = locationAnalysisOptions.Value;
        }

        public async Task<ReadCity> AddAsync(WriteCity model)
        {
            var state = await lookupRepository.GetStateByIdAsync(model.StateId);

            ValidateWriteCity(model, state);

            var loggedInUser = membershipService.GetLoggedInUser();
            var newCity = BuildCity(model, state, loggedInUser);

            await cityRepository.SaveOrUpdateAsync(newCity);
            return BuildReadCity(newCity);
        }

        public async Task<ReadCity> UpdateAsync(long id, WriteCity model)
        {
            var state = await lookupRepository.GetStateByIdAsync(model.StateId);

            ValidateWriteCity(model, state);

            var loggedInUser = membershipService.GetLoggedInUser();
            var existingCity = await cityRepository.GetByIdAsync(id, loggedInUser.Id);

            if (existingCity == null)
            {
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid city ID.") });
            }

            existingCity.Update(cityName: model.CityName,
                state: state,
                populationYearStart: model.PopulationYearStart,
                medianHouseOrCondoValueYearStart: model.MedianHouseOrCondoValueYearStart,
                medianHouseholdIncomeYearStart: model.MedianHouseholdIncomeYearStart,
                populationInYearStart: model.PopulationInYearStart,
                populationInYearEnd: model.PopulationInYearEnd,
                medianHouseholdIncomeInYearStart: model.MedianHouseholdIncomeInYearStart,
                medianHouseholdIncomeInYearEnd: model.MedianHouseholdIncomeInYearEnd,
                medianHouseOrCondoValueInYearStart: model.MedianHouseOrCondoValueInYearStart,
                medianHouseOrCondoValueInYearEnd: model.MedianHouseOrCondoValueInYearEnd,
                crimeIndexInYearStart: model.CrimeIndexInYearStart,
                crimeIndexInYearEnd: model.CrimeIndexInYearEnd,
                recentYearJobsGrowthRate: model.RecentYearJobsGrowthRate,
                populationYearEnd: model.PopulationYearEnd,
                medianHouseholdIncomeYearEnd: model.MedianHouseholdIncomeYearEnd,
                medianHouseOrCondoValueYearEnd: model.MedianHouseOrCondoValueYearEnd,
                numberOfJobsAdded: model.NumberOfJobsAdded,
                crimeIndexYearStart: model.CrimeIndexYearStart,
                crimeIndexYearEnd: model.CrimeIndexYearEnd);

            await cityRepository.SaveOrUpdateAsync(existingCity);
            return BuildReadCity(existingCity);
        }

        public async Task DeleteAsync(long cityId)
        {
            var loggedInUser = membershipService.GetLoggedInUser();
            var city = await cityRepository.GetByIdAsync(cityId, loggedInUser.Id);

            if (city == null)
            {
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid city ID.") });
            }

            await cityRepository.DeleteAsync(city);
        }

        public async Task<List<ReadCity>> GetAllAsync()
        {
            var loggedInUser = membershipService.GetLoggedInUser();
            var allCities = await cityRepository.GetAllAsync(loggedInUser.Id, asNoTracking: true);
            var readCities = allCities.Select(x => BuildReadCity(x)).ToList();

            return readCities.ToList();
        }

        public async Task<ReadCity> GetByIdAsync(long id)
        {
            var loggedInUser = membershipService.GetLoggedInUser();
            var city = await cityRepository.GetByIdAsync(id, loggedInUser.Id, asNoTracking: true);

            if (city == null)
            {
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid city ID.") });
            }

            return BuildReadCity(city);
        }

        public async Task ToggleIsFavorite(long cityId)
        {
            var loggedInUser = membershipService.GetLoggedInUser();
            var city = await cityRepository.GetByIdAsync(cityId, loggedInUser.Id);

            var validator = new ToggleIsFavoriteValidator();
            validator.Validate(new ToggleIsFavoriteValidator.ValidationState
            {
                City = city
            });

            city.ToggleIsFavorite();
            await cityRepository.SaveOrUpdateAsync(city);
        }

        private void ValidateWriteCity(WriteCity model, State state)
        {
            var validator = new WriteCityValidator();
            validator.Validate(new WriteCityValidator.ValidationState
            {
                Model = model,
                State = state
            });
        }

        private decimal GetMinimumAcceptablePopulationGrowthRate(City city)
        {
            var growthRateSettings = locationAnalysisSettings.City.PopulationGrowthRates.First(x => city.PopulationInYearStart >= x.PopulationFrom && city.PopulationInYearStart <= x.PopulationTo);
            var minimumAcceptablePopulationGrowthRate = (city.PopulationYearEnd - city.PopulationYearStart) * growthRateSettings.AnnualMultiplier;
            return Math.Round(minimumAcceptablePopulationGrowthRate, 2);
        }

        private decimal GetMinimumAcceptableMedianHouseholdIncomeGrowthRate(City city)
        {
            var minimumAcceptableMedianHouseholdIncomeGrowthRate = (city.MedianHouseholdIncomeYearEnd - city.MedianHouseholdIncomeYearStart) * locationAnalysisSettings.City.HouseholdIncomeAnnualGrowthRate;
            return Math.Round(minimumAcceptableMedianHouseholdIncomeGrowthRate, 2);
        }

        private decimal GetMinimumAcceptableMedianHouseOrCondoValueGrowthRate(City city)
        {
            var minimumAcceptableMedianHouseOrCondoValueGrowthRate = (city.MedianHouseOrCondoValueYearEnd - city.MedianHouseOrCondoValueYearStart) * locationAnalysisSettings.City.MedianHouseOrCondoValueAnnualGrowthRate;
            return Math.Round(minimumAcceptableMedianHouseOrCondoValueGrowthRate, 2);
        }

        public async Task HarvestCityData(int minimumPopulationCount)
        {
            var cityDataUrls = await dataScraperService.GetCityDataCitiesUrls(minimumPopulationCount);
            var states = (await lookupRepository.GetAllStatesAsync()).ToDictionary(x => x.Name);
            var loggedInUser = membershipService.GetLoggedInUser();
            var today = DateTime.Today;

            var allCities = await cityRepository.GetAllAsync(loggedInUser.Id);

            foreach (var cityDataUrl in cityDataUrls)
            {
                try
                {
                    var cityData = await dataScraperService.GetCityData(cityDataUrl);
                    var state = states[cityData.StateName];
                    var writeCity = BuildWriteCity(cityData, state);
                    ValidateWriteCity(writeCity, state);

                    var existingCity = allCities.FirstOrDefault(x => x.State.Id == state.Id && x.CityName.ToLower() == writeCity.CityName.ToLower());
                    City cityToSave;

                    if (existingCity != null)
                    {
                        UpdateCity(existingCity, writeCity, state, autoGeneratedDate: today);
                        cityToSave = existingCity;
                    }
                    else
                    {
                        cityToSave = BuildCity(writeCity, state, loggedInUser, autoGeneratedDate: today);
                    }

                    await cityRepository.SaveOrUpdateAsync(cityToSave);

                    await Task.Delay(TimeSpan.FromSeconds(6));
                }
                catch (Exception ex)
                {
                    await errorLogService.LogErrorAsync(new WriteErrorLog()
                    {
                        ClassName = nameof(CityService),
                        MethodName = nameof(HarvestCityData),
                        Values = JsonConvert.SerializeObject(new Dictionary<string, object> { { "URL", cityDataUrl } }),
                        Error = JsonConvert.SerializeObject(ex)
                    });
                }
            }
        }

        private WriteCity BuildWriteCity(ReadCityData readCityData, State state) =>
            new WriteCity
            {
                CityName = readCityData.CityName,
                StateId = state.Id,
                PopulationInYearStart = readCityData.PopulationInYearStart,
                PopulationInYearEnd = readCityData.PopulationInYearEnd,
                MedianHouseholdIncomeInYearStart = readCityData.MedianHouseholdIncomeInYearStart,
                MedianHouseholdIncomeInYearEnd = readCityData.MedianHouseholdIncomeInYearEnd,
                MedianHouseOrCondoValueInYearStart = readCityData.MedianHouseOrCondoValueInYearStart,
                MedianHouseOrCondoValueInYearEnd = readCityData.MedianHouseOrCondoValueInYearEnd,
                CrimeIndexInYearStart = readCityData.CrimeIndexInYearStart,
                CrimeIndexInYearEnd = readCityData.CrimeIndexInYearEnd,
                RecentYearJobsGrowthRate = readCityData.RecentYearJobsGrowthRate,
                PopulationYearEnd = readCityData.PopulationYearEnd,
                MedianHouseholdIncomeYearEnd = readCityData.MedianHouseholdIncomeYearEnd,
                MedianHouseOrCondoValueYearEnd = readCityData.MedianHouseOrCondoValueYearEnd,
                NumberOfJobsAdded = readCityData.NumberOfJobsAdded,
                CrimeIndexYearEnd = readCityData.CrimeIndexYearEnd,
                CrimeIndexYearStart = readCityData.CrimeIndexYearStart,
                MedianHouseholdIncomeYearStart = readCityData.MedianHouseholdIncomeYearStart,
                MedianHouseOrCondoValueYearStart = readCityData.MedianHouseOrCondoValueYearStart,
                PopulationYearStart = readCityData.PopulationYearStart
            };

        private City BuildCity(WriteCity writeCity, State state, ReadUser loggedInUser, DateTime? autoGeneratedDate = null) =>
            new City(cityName: writeCity.CityName,
                state: state,
                populationInYearStart: writeCity.PopulationInYearStart,
                populationYearStart: writeCity.PopulationYearStart,
                populationInYearEnd: writeCity.PopulationInYearEnd,
                medianHouseholdIncomeInYearStart: writeCity.MedianHouseholdIncomeInYearStart,
                medianHouseholdIncomeInYearEnd: writeCity.MedianHouseholdIncomeInYearEnd,
                medianHouseOrCondoValueInYearStart: writeCity.MedianHouseOrCondoValueInYearStart,
                medianHouseOrCondoValueInYearEnd: writeCity.MedianHouseOrCondoValueInYearEnd,
                crimeIndexInYearStart: writeCity.CrimeIndexInYearStart,
                crimeIndexInYearEnd: writeCity.CrimeIndexInYearEnd,
                recentYearJobsGrowthRate: writeCity.RecentYearJobsGrowthRate,
                populationYearEnd: writeCity.PopulationYearEnd,
                medianHouseholdIncomeYearStart: writeCity.MedianHouseholdIncomeYearStart,
                medianHouseholdIncomeYearEnd: writeCity.MedianHouseholdIncomeYearEnd,
                medianHouseOrCondoValueYearStart: writeCity.MedianHouseOrCondoValueYearStart,
                medianHouseOrCondoValueYearEnd: writeCity.MedianHouseOrCondoValueYearEnd,
                numberOfJobsAdded: writeCity.NumberOfJobsAdded,
                crimeIndexYearEnd: writeCity.CrimeIndexYearEnd,
                crimeIndexYearStart: writeCity.CrimeIndexYearStart,
                autoGeneratedDate: autoGeneratedDate,
                userId: loggedInUser.Id);

        private void UpdateCity(City city, WriteCity writeCity, State state, DateTime? autoGeneratedDate)
        {
            city.Update(cityName: writeCity.CityName,
                state: state,
                populationInYearStart: writeCity.PopulationInYearStart,
                populationYearStart: writeCity.PopulationYearStart,
                populationInYearEnd: writeCity.PopulationInYearEnd,
                medianHouseholdIncomeInYearStart: writeCity.MedianHouseholdIncomeInYearStart,
                medianHouseholdIncomeInYearEnd: writeCity.MedianHouseholdIncomeInYearEnd,
                medianHouseOrCondoValueInYearStart: writeCity.MedianHouseOrCondoValueInYearStart,
                medianHouseOrCondoValueInYearEnd: writeCity.MedianHouseOrCondoValueInYearEnd,
                crimeIndexInYearStart: writeCity.CrimeIndexInYearStart,
                crimeIndexInYearEnd: writeCity.CrimeIndexInYearEnd,
                recentYearJobsGrowthRate: writeCity.RecentYearJobsGrowthRate,
                populationYearEnd: writeCity.PopulationYearEnd,
                medianHouseholdIncomeYearStart: writeCity.MedianHouseholdIncomeYearStart,
                medianHouseholdIncomeYearEnd: writeCity.MedianHouseholdIncomeYearEnd,
                medianHouseOrCondoValueYearStart: writeCity.MedianHouseOrCondoValueYearStart,
                medianHouseOrCondoValueYearEnd: writeCity.MedianHouseOrCondoValueYearEnd,
                numberOfJobsAdded: writeCity.NumberOfJobsAdded,
                crimeIndexYearEnd: writeCity.CrimeIndexYearEnd,
                crimeIndexYearStart: writeCity.CrimeIndexYearStart,
                autoGeneratedDate: autoGeneratedDate);
        }

        private ReadCity BuildReadCity(City city)
        {
            var minimumAcceptablePopulationGrowthRate = GetMinimumAcceptablePopulationGrowthRate(city);
            var populationGrowthRate = GetGrowthRate(city.PopulationInYearStart, city.PopulationInYearEnd);

            var minimumAcceptableMedianHouseholdIncomeGrowthRate = GetMinimumAcceptableMedianHouseholdIncomeGrowthRate(city);
            var medianHouseholdIncomeGrowthRate = GetGrowthRate(city.MedianHouseholdIncomeInYearStart, city.MedianHouseholdIncomeInYearEnd);

            var minimumAcceptableMedianHouseOrCondoValueGrowthRate = GetMinimumAcceptableMedianHouseOrCondoValueGrowthRate(city);
            var medianHouseOrCondoValueGrowthRate = GetGrowthRate(city.MedianHouseOrCondoValueInYearStart, city.MedianHouseOrCondoValueInYearEnd);

            decimal highestAcceptableCrimeIndex = locationAnalysisSettings.City.HighestAcceptableCrimeIndex;
            var crimeReduction = city.CrimeIndexInYearStart - city.CrimeIndexInYearEnd;

            var minimumAcceptableRecentYearJobsGrowthRate = locationAnalysisSettings.City.JobsGrowthRates
                .First(x => city.PopulationInYearStart >= x.PopulationFrom && city.PopulationInYearStart <= x.PopulationTo)
                .GrowthRate;

            return new ReadCity(city: city,
                populationGrowthRate: populationGrowthRate,
                populationGrowthRateIsGood: populationGrowthRate >= minimumAcceptablePopulationGrowthRate,
                medianHouseholdIncomeGrowthRate: medianHouseholdIncomeGrowthRate,
                medianHouseholdIncomeGrowthRateIsGood: medianHouseholdIncomeGrowthRate >= minimumAcceptableMedianHouseholdIncomeGrowthRate,
                medianHouseOrCondoValueGrowthRate: medianHouseOrCondoValueGrowthRate,
                medianHouseOrCondoValueGrowthRateIsGood: medianHouseOrCondoValueGrowthRate >= minimumAcceptableMedianHouseOrCondoValueGrowthRate,
                crimeReduction: crimeReduction,
                crimeIndexInYearEndIsGood: city.CrimeIndexInYearEnd <= highestAcceptableCrimeIndex,
                recentYearJobsGrowthRateIsGood: city.RecentYearJobsGrowthRate >= minimumAcceptableRecentYearJobsGrowthRate,
                minimumAcceptableMedianHouseholdIncomeGrowthRate: minimumAcceptableMedianHouseholdIncomeGrowthRate,
                minimumAcceptableMedianHouseOrCondoValueGrowthRate: minimumAcceptableMedianHouseOrCondoValueGrowthRate,
                minimumAcceptablePopulationGrowthRate: minimumAcceptablePopulationGrowthRate,
                highestAcceptableCrimeIndex: highestAcceptableCrimeIndex,
                minimumAcceptableRecentYearJobsGrowthRate: minimumAcceptableRecentYearJobsGrowthRate);
        }

        private decimal GetGrowthRate(decimal startingValue, decimal endingValue)
        {
            var result = ((endingValue - startingValue) / startingValue) * 100M;
            return Math.Round(result, 2);
        }
    }
}