import { ReadRentsFloorPlan } from './readRentsFloorPlan';

export class ReadRents {
    public readonly rentsFloorPlans: ReadRentsFloorPlan[];
    public readonly units: number;
    public readonly averageContractRent: number;
    public readonly averageMarketRent: number;
    public readonly percentOfMarketRent: number;
}
