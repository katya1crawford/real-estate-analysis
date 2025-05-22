import { ReadFloorPlan } from './readFloorPlan';

export class ReadFloorPlansSummary {
    public readonly floorPlans: ReadFloorPlan[];
    public readonly units: number;
    public readonly averageContractRent: number;
    public readonly averageMarketRent: number;
    public readonly percentOfMarketRent: number;
    public readonly averageSquareFootage: number;
}
