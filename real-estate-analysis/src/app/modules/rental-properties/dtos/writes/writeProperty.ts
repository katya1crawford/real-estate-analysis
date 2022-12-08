import { WriteAddress } from './writeAddress';
import { WriteAnnualOperatingExpense } from './WriteAnnualOperatingExpense';
import { WriteClosingCost } from './WriteClosingCosts';
import { WriteExteriorRepairExpense } from './WriteExteriorRepairExpense';
import { WriteGeneralRepairExpense } from './WriteGeneralRepairExpense';
import { WriteInteriorRepairExpense } from './WriteInteriorRepairExpense';

import { WriteUnitGroup } from './writeUnitGroups';

export class WriteProperty {
  constructor(
    public thumbnailImage: File | null,
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
    public groupName: null) { }
}
