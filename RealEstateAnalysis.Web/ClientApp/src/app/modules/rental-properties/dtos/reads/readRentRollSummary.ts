import { ReadAddress } from 'src/app/shared/dtos/reads/readAddress';
import { ReadAverageContractRentByMonth } from './readAverageContractRentByMonth';
import { ReadFloorPlanAverageContractRentByMonthSummary } from './readFloorPlanAverageContractRentByMonthSummary';
import { ReadFloorPlansSummary } from './readFloorPlansSummary';
import { ReadLeasesCountByMonth } from './readLeasesCountByMonth';
import { ReadOccupancy } from './readOccupancy';
import { ReadRentRollItem } from './readRentRollItem';
import { ReadRents } from './readRents';

export class ReadRentRollSummary {
    public readonly rentRollItems: ReadRentRollItem[];
    public readonly averageContractRentByMonth: ReadAverageContractRentByMonth[];
    public readonly floorPlanAverageContractRentByMonthSummary: ReadFloorPlanAverageContractRentByMonthSummary;
    public readonly newLeasesCountByMonth: ReadLeasesCountByMonth[];
    public readonly leasesExpireCountByMonth: ReadLeasesCountByMonth[];
    public readonly rentsAllOccupiedUnits: ReadRents;
    public readonly rentsNonRenovatedOccupiedUnits: ReadRents;
    public readonly rentsRenovatedOccupiedUnits: ReadRents;
    public readonly occupancyAllUnits: ReadOccupancy;
    public readonly occupancyNonRenovatedUnits: ReadOccupancy;
    public readonly occupancyRenovatedUnits: ReadOccupancy;
    public readonly floorPlansSummary: ReadFloorPlansSummary;
    public readonly totalSquareFootage: number;
    public readonly numberOfVacantUnits: number;
    public readonly numberOfRenovatedUnits: number;
    public readonly totalContractRent: number;
    public readonly totalOtherIncome: number;
    public readonly totalMarketRent: number;
    public readonly numberOfMonthToMonthUnits: number;
    public readonly totalNumberOfUnits: number;
    public readonly numberOfFloorPlans: number;
    public readonly vacancyRate: number;
    public readonly averageContractRent: number;
    public readonly averageMarketRent: number;
    public readonly totalActualMonthlyIncome: number;
    public readonly address: ReadAddress;
    public readonly averageSquareFootage: number;
    public readonly averageOtherIncome: number;
    public readonly averagePercentOfMarketRent: number;
}
