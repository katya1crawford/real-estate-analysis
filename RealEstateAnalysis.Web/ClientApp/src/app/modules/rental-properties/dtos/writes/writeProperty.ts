import { WriteAnnualOperatingExpense } from './writeAnnualOperatingExpense';
import { WriteExteriorRepairExpense } from '../../../../shared/dtos/writes/writeExteriorRepairExpense';
import { WriteGeneralRepairExpense } from '../../../../shared/dtos/writes/writeGeneralRepairExpense';
import { WriteAddress } from '../../../../shared/dtos/writes/writeAddress';
import { WriteInteriorRepairExpense } from '../../../../shared/dtos/writes/writeInteriorRepairExpense';
import { WriteClosingCost } from '../../../../shared/dtos/writes/writeClosingCost';
import { WriteUnitGroup } from '../../../../shared/dtos/writes/writeUnitGroup';

export class WriteProperty {
    constructor(public thumbnailImage: File | null,
        public yearBuiltIn: number,
        public buildingSquareFootage: number,
        public lotSquareFootage: number,
        public propertyTypeId: number,
        public propertyStatusId: number,
        public notes: string,
        public address: WriteAddress,
        public unitGroups: WriteUnitGroup[],
        public annualOperatingExpenses: WriteAnnualOperatingExpense[],
        public annualOperatingExpensesGrowthRate: number,
        public marketCapitalizationRate: number,
        public annualGrossScheduledRentalIncomeGrowthRate: number,
        public annualGrossScheduledRentalIncome: number,
        public annualVacancyRate: number,
        public annualPropertyManagementFeeRate: number,
        public closingCosts: WriteClosingCost[],
        public downPayment: number,
        public exteriorRepairExpenses: WriteExteriorRepairExpense[],
        public generalRepairExpenses: WriteGeneralRepairExpense[],
        public interiorRepairExpenses: WriteInteriorRepairExpense[],
        public loanApr: number,
        public loanYears: number,
        public otherAnnualIncome: number,
        public purchasePrice: number,
        public groupName: string) { }
}
