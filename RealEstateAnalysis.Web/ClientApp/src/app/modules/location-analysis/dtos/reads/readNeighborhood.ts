import { ReadState } from '../../../../shared/dtos/reads/readState';
import { ReadDecimalRange } from './readDecimalRange';

export class ReadNeighborhood {
    constructor(public id: number,
        public neighborhoodName: string,
        public city: string,
        public medianHouseholdIncome: number,
        public medianContractRent: number,
        public cityUnemploymentRate: number,
        public neighborhoodUnemploymentRate: number,
        public povertyRate: number,
        public ethnicMixLargestSlicePercent: number,
        public state: ReadState,
        public cityToNeighborhoodUnemploymentRateDifference: number,
        public cityToNeighborhoodUnemploymentRateDifferenceIsGood: boolean,
        public povertyRateIsGood: boolean,
        public ethnicMixLargestSlicePercentIsGood: boolean,
        public highestAcceptableCityToNeighborhoodUnemploymentRateDifference: number,
        public highestAcceptablePovertyRate: number,
        public highestAcceptableEthnicMixLargestSlicePercent: number,
        public acceptableMedianHouseholdIncomeRange: ReadDecimalRange,
        public acceptableMedianContractRentRange: ReadDecimalRange,
        public medianHouseholdIncomeIsGood: boolean,
        public medianContractRentIsGood: boolean,
        public homesMedianDaysOnMarket: number) { }
}
