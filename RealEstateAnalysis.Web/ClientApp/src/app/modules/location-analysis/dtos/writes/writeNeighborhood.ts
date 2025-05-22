export class WriteNeighborhood {
    constructor(public neighborhoodName: string,
        public city: string,
        public medianHouseholdIncome: number,
        public medianContractRent: number,
        public cityUnemploymentRate: number,
        public neighborhoodUnemploymentRate: number,
        public povertyRate: number,
        public ethnicMixLargestSlicePercent: number,
        public homesMedianDaysOnMarket: number,
        public stateId: number) { }
}
