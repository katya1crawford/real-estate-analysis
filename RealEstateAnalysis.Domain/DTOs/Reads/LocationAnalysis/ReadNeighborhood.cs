using RealEstateAnalysis.Data.Entities.LocationAnalysis;

namespace RealEstateAnalysis.Domain.DTOs.Reads.LocationAnalysis
{
    public class ReadNeighborhood
    {
        public ReadNeighborhood(Neighborhood neighborhood,
            decimal cityToNeighborhoodUnemploymentRateDifference,
            bool cityToNeighborhoodUnemploymentRateDifferenceIsGood,
            bool povertyRateIsGood,
            bool ethnicMixLargestSlicePercentIsGood,
            decimal highestAcceptableCityToNeighborhoodUnemploymentRateDifference,
            decimal highestAcceptablePovertyRate,
            decimal highestAcceptableEthnicMixLargestSlicePercent,
            ReadDecimalRange acceptableMedianHouseholdIncomeRange,
            ReadDecimalRange acceptableMedianContractRentRange,
            bool medianHouseholdIncomeIsGood,
            bool medianContractRentIsGood)
        {
            Id = neighborhood.Id;
            AcceptableMedianContractRentRange = acceptableMedianContractRentRange;
            AcceptableMedianHouseholdIncomeRange = acceptableMedianHouseholdIncomeRange;
            City = neighborhood.City;
            CityToNeighborhoodUnemploymentRateDifference = cityToNeighborhoodUnemploymentRateDifference;
            CityToNeighborhoodUnemploymentRateDifferenceIsGood = cityToNeighborhoodUnemploymentRateDifferenceIsGood;
            CityUnemploymentRate = neighborhood.CityUnemploymentRate;
            EthnicMixLargestSlicePercent = neighborhood.EthnicMixLargestSlicePercent;
            EthnicMixLargestSlicePercentIsGood = ethnicMixLargestSlicePercentIsGood;
            HighestAcceptableCityToNeighborhoodUnemploymentRateDifference = highestAcceptableCityToNeighborhoodUnemploymentRateDifference;
            HighestAcceptableEthnicMixLargestSlicePercent = highestAcceptableEthnicMixLargestSlicePercent;
            HighestAcceptablePovertyRate = highestAcceptablePovertyRate;
            HomesMedianDaysOnMarket = neighborhood.HomesMedianDaysOnMarket;
            MedianContractRent = neighborhood.MedianContractRent;
            MedianContractRentIsGood = medianContractRentIsGood;
            MedianHouseholdIncome = neighborhood.MedianHouseholdIncome;
            MedianHouseholdIncomeIsGood = medianHouseholdIncomeIsGood;
            NeighborhoodName = neighborhood.NeighborhoodName;
            NeighborhoodUnemploymentRate = neighborhood.NeighborhoodUnemploymentRate;
            PovertyRate = neighborhood.PovertyRate;
            PovertyRateIsGood = povertyRateIsGood;
            State = new ReadState(neighborhood.State);
        }

        public ReadDecimalRange AcceptableMedianContractRentRange { get; }

        public ReadDecimalRange AcceptableMedianHouseholdIncomeRange { get; }

        public string City { get; }

        public decimal CityToNeighborhoodUnemploymentRateDifference { get; }

        public bool CityToNeighborhoodUnemploymentRateDifferenceIsGood { get; }

        public decimal CityUnemploymentRate { get; }

        public decimal EthnicMixLargestSlicePercent { get; }

        public bool EthnicMixLargestSlicePercentIsGood { get; }

        public decimal HighestAcceptableCityToNeighborhoodUnemploymentRateDifference { get; }

        public decimal HighestAcceptableEthnicMixLargestSlicePercent { get; }

        public decimal HighestAcceptablePovertyRate { get; }

        public int HomesMedianDaysOnMarket { get; }

        public long Id { get; }

        public decimal MedianContractRent { get; }

        public bool MedianContractRentIsGood { get; }

        public decimal MedianHouseholdIncome { get; }

        public bool MedianHouseholdIncomeIsGood { get; }

        public string NeighborhoodName { get; }

        public decimal NeighborhoodUnemploymentRate { get; }

        public decimal PovertyRate { get; }

        public bool PovertyRateIsGood { get; }

        public ReadState State { get; }
    }
}