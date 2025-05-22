import { WriteAnnualOperatingExpense } from './writeAnnualOperatingExpense';
import { WriteExteriorRepairExpense } from '../../../../shared/dtos/writes/writeExteriorRepairExpense';
import { WriteClosingCost } from '../../../../shared/dtos/writes/writeClosingCost';
import { WriteGeneralRepairExpense } from '../../../../shared/dtos/writes/writeGeneralRepairExpense';
import { WriteInteriorRepairExpense } from '../../../../shared/dtos/writes/writeInteriorRepairExpense';

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
