import { WriteAnnualOperatingExpense } from './WriteAnnualOperatingExpense';
import { WriteClosingCost } from './WriteClosingCosts';
import { WriteExteriorRepairExpense } from './WriteExteriorRepairExpense';
import { WriteGeneralRepairExpense } from './WriteGeneralRepairExpense';
import { WriteInteriorRepairExpense } from './WriteInteriorRepairExpense';

export class WriteFinancialSummary {
  constructor(public purchasePrice: number,
    public downPayment: number,
    public annualGrossScheduledRentalIncome: number,
    public otherAnnualIncome: number,
    public annualVacancyRate: number,
    public annualPropertyManagementFeeRate: number,
    public loanApr: number,
    public loanYears: number,
    public annualGrossScheduledRentalIncomeGrowthRate: number,
    public annualOperatingExpensesGrowthRate: number,
    public marketCapitalizationRate: number,
    public annualOperatingExpenses: WriteAnnualOperatingExpense[],
    public closingCosts: WriteClosingCost[],
    public exteriorRepairExpenses: WriteExteriorRepairExpense[],
    public generalRepairExpenses: WriteGeneralRepairExpense[],
    public interiorRepairExpenses: WriteInteriorRepairExpense[]) { }
}
