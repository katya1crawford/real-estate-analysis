import { ReadOccupancyFloorPlan } from './readOccupancyFloorPlan';

export class ReadOccupancy {
    public readonly occupancyFloorPlans: ReadOccupancyFloorPlan[];
    public readonly totalUnits: number;
    public readonly totalOccupiedUnits: number;
    public readonly totalVacantUnits: number;
    public readonly totalPercentOfVacantUnits: number;
}
