using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Options;
using RealEstateAnalysis.Data.Abstract;
using RealEstateAnalysis.Data.Abstract.LocationAnalysis;
using RealEstateAnalysis.Domain.Abstract;
using RealEstateAnalysis.Domain.Abstract.LocationAnalysis;
using RealEstateAnalysis.Domain.DTOs.Reads.LocationAnalysis;
using RealEstateAnalysis.Domain.DTOs.Writes.LocationAnalysis;
using RealEstateAnalysis.Domain.Settings;
using RealEstateAnalysis.Domain.Validators.LocationAnalysis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Neighborhood = RealEstateAnalysis.Data.Entities.LocationAnalysis.Neighborhood;

namespace RealEstateAnalysis.Domain.Services.LocationAnalysis
{
    public class NeighborhoodService : INeighborhoodService
    {
        private readonly IMembershipService membershipService;
        private readonly INeighborhoodRepository neighborhoodRepository;
        private readonly ILookupRepository lookupRepository;
        private readonly LocationAnalysisSettings locationAnalysisSettings;

        public NeighborhoodService(IMembershipService membershipService,
            INeighborhoodRepository neighborhoodRepository,
            ILookupRepository lookupRepository,
            IOptions<LocationAnalysisSettings> locationAnalysisOptions)
        {
            this.membershipService = membershipService;
            this.neighborhoodRepository = neighborhoodRepository;
            this.lookupRepository = lookupRepository;
            locationAnalysisSettings = locationAnalysisOptions.Value;
        }

        public async Task<ReadNeighborhood> AddAsync(WriteNeighborhood model)
        {
            var validator = new NeighborhoodValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var state = await lookupRepository.GetStateByIdAsync(model.StateId);

            if (state == null)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid state ID.") });

            var loggedInUser = membershipService.GetLoggedInUser();

            var newNeighborhood = new Neighborhood(neighborhoodName: model.NeighborhoodName,
                city: model.City,
                state: state,
                medianHouseholdIncome: model.MedianHouseholdIncome,
                medianContractRent: model.MedianContractRent,
                cityUnemploymentRate: model.CityUnemploymentRate,
                neighborhoodUnemploymentRate: model.NeighborhoodUnemploymentRate,
                neighborhoodPovertyRate: model.PovertyRate,
                ethnicMixLargestSlicePercent: model.EthnicMixLargestSlicePercent,
                homesMedianDaysOnMarket: model.HomesMedianDaysOnMarket,
                userId: loggedInUser.Id);

            await neighborhoodRepository.SaveOrUpdateAsync(newNeighborhood);
            return BuildReadNeighborhood(newNeighborhood);
        }

        public async Task<ReadNeighborhood> UpdateAsync(long id, WriteNeighborhood model)
        {
            var validator = new NeighborhoodValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var loggedInUser = membershipService.GetLoggedInUser();
            var existingNeighborhood = await neighborhoodRepository.GetByIdAsync(id, loggedInUser.Id);

            if (existingNeighborhood == null)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid neighborhood ID.") });

            var state = await lookupRepository.GetStateByIdAsync(model.StateId);

            if (state == null)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid state ID.") });

            existingNeighborhood.Update(neighborhoodName: model.NeighborhoodName,
                city: model.City,
                state: state,
                medianHouseholdIncome: model.MedianHouseholdIncome,
                medianContractRent: model.MedianContractRent,
                cityUnemploymentRate: model.CityUnemploymentRate,
                neighborhoodUnemploymentRate: model.NeighborhoodUnemploymentRate,
                neighborhoodPovertyRate: model.PovertyRate,
                ethnicMixLargestSlicePercent: model.EthnicMixLargestSlicePercent,
                homesMedianDaysOnMarket: model.HomesMedianDaysOnMarket);

            await neighborhoodRepository.SaveOrUpdateAsync(existingNeighborhood);
            return BuildReadNeighborhood(existingNeighborhood);
        }

        public async Task DeleteAsync(long cityId)
        {
            var loggedInUser = membershipService.GetLoggedInUser();
            var neighborhood = await neighborhoodRepository.GetByIdAsync(cityId, loggedInUser.Id);

            if (neighborhood == null)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid neighborhood ID.") });

            await neighborhoodRepository.DeleteAsync(neighborhood);
        }

        public async Task<List<ReadNeighborhood>> GetAllAsync()
        {
            var loggedInUser = membershipService.GetLoggedInUser();
            var allNeighborhoods = await neighborhoodRepository.GetAllAsync(loggedInUser.Id, asNoTracking: true);
            var readCities = allNeighborhoods.Select(x => BuildReadNeighborhood(x)).ToList();

            return readCities.ToList();
        }

        public async Task<ReadNeighborhood> GetByIdAsync(long id)
        {
            var loggedInUser = membershipService.GetLoggedInUser();
            var neighborhood = await neighborhoodRepository.GetByIdAsync(id, loggedInUser.Id, asNoTracking: true);

            if (neighborhood == null)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid neighborhood ID.") });

            return BuildReadNeighborhood(neighborhood);
        }

        private ReadNeighborhood BuildReadNeighborhood(Neighborhood neighborhood)
        {
            var highestAcceptableCityToNeighborhoodUnemploymentRateDifference = locationAnalysisSettings.Neighborhood.HighestAcceptableCityToNeighborhoodUnemploymentRateDifference;
            var highestAcceptablePovertyRate = locationAnalysisSettings.Neighborhood.HighestAcceptablePovertyRate;
            var highestAcceptableEthnicMixLargestSlicePercent = locationAnalysisSettings.Neighborhood.HighestAcceptableEthnicMixLargestSlicePercent;
            var cityToNeighborhoodUnemploymentRateDifference = neighborhood.NeighborhoodUnemploymentRate - neighborhood.CityUnemploymentRate;
            var acceptableMedianHouseholdIncomeRange = locationAnalysisSettings.Neighborhood.MedianHouseholdIncomeRange;
            var acceptableMedianContractRentRange = locationAnalysisSettings.Neighborhood.MedianContractRentRange;
            var medianHouseholdIncomeIsGood = acceptableMedianHouseholdIncomeRange.IsWithinRange(neighborhood.MedianHouseholdIncome);
            var medianContractRentIsGood = acceptableMedianContractRentRange.IsWithinRange(neighborhood.MedianContractRent);

            return new ReadNeighborhood(neighborhood,
                cityToNeighborhoodUnemploymentRateDifference: cityToNeighborhoodUnemploymentRateDifference,
                cityToNeighborhoodUnemploymentRateDifferenceIsGood: cityToNeighborhoodUnemploymentRateDifference <= highestAcceptableCityToNeighborhoodUnemploymentRateDifference,
                povertyRateIsGood: neighborhood.PovertyRate <= highestAcceptablePovertyRate,
                ethnicMixLargestSlicePercentIsGood: neighborhood.EthnicMixLargestSlicePercent <= highestAcceptableEthnicMixLargestSlicePercent,
                highestAcceptableCityToNeighborhoodUnemploymentRateDifference: highestAcceptableCityToNeighborhoodUnemploymentRateDifference,
                highestAcceptablePovertyRate: highestAcceptablePovertyRate,
                highestAcceptableEthnicMixLargestSlicePercent: highestAcceptableEthnicMixLargestSlicePercent,
                acceptableMedianHouseholdIncomeRange: new ReadDecimalRange(from: acceptableMedianHouseholdIncomeRange.From, to: acceptableMedianHouseholdIncomeRange.To),
                acceptableMedianContractRentRange: new ReadDecimalRange(from: acceptableMedianContractRentRange.From, to: acceptableMedianContractRentRange.To),
                medianHouseholdIncomeIsGood: medianHouseholdIncomeIsGood,
                medianContractRentIsGood: medianContractRentIsGood);
        }
    }
}