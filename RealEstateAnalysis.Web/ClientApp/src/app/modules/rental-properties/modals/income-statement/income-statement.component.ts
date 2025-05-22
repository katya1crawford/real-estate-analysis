import { Component } from '@angular/core';
import { ReadProperty } from '../../dtos/reads/readProperty';

@Component({
    styleUrls: ['./income-statement.component.css'],
    templateUrl: './income-statement.component.html'
})
export class IncomeStatementComponent {
    public property: ReadProperty;
    public combinedAnnualOperatingExpenses: { name: string, amount: number }[] = [];

    public initialize(property: ReadProperty): void {
        this.property = property;
        this.setCombinedAnnualOperatingExpenses(property);
    }

    public getPercentOfGoi(amount: number): number {
        const percentOfGoi = (amount / this.property.financialSummary.annualEffectiveGrossIncome) * 100;
        return +percentOfGoi.toFixed(2);
    }

    private setCombinedAnnualOperatingExpenses(property: ReadProperty): void {
        this.combinedAnnualOperatingExpenses = [];

        if (property.financialSummary.annualPropertyManagementFee > 0) {
            this.combinedAnnualOperatingExpenses.push({
                amount: property.financialSummary.annualPropertyManagementFee,
                name: 'Property Management Fee'
            });
        }

        if (property.annualOperatingExpenses.length > 0) {
            for (let i = 0; i < property.annualOperatingExpenses.length; i++) {
                this.combinedAnnualOperatingExpenses.push({
                    amount: property.annualOperatingExpenses[i].amount,
                    name: property.annualOperatingExpenses[i].operatingExpenseTypeName
                });
            }
        }

        this.combinedAnnualOperatingExpenses = this.combinedAnnualOperatingExpenses.sort(this.orderCombinedOperatingExpenseByAmountDesc);
    }

    private orderCombinedOperatingExpenseByAmountDesc(a, b): number {
        if (a.amount > b.amount) {
            return -1;
        }

        if (a.amount < b.amount) {
            return 1;
        }

        return 0;
    }
}
