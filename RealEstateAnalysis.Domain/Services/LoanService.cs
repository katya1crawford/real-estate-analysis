using FluentValidation;
using RealEstateAnalysis.Domain.Abstract;
using RealEstateAnalysis.Domain.DTOs.Reads;
using RealEstateAnalysis.Domain.DTOs.Writes;
using RealEstateAnalysis.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RealEstateAnalysis.Domain.Services
{
    public class LoanService : ILoanService
    {
        public double GetMonthlyLoanPaymentAmount(WriteLoan model)
        {
            if (model.Amount == 0)
                return 0;

            var validator = new LoanValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            if (model.Apr == 0)
                return (double)(model.Amount / (model.Years * 12));


            var termMonths = model.Years * 12;
            var monthlyInterestRate = ((double)model.Apr / 100) * (1.0 / 12.0);
            var monthlyLoanPayment = ((double)model.Amount * monthlyInterestRate) / (1 - 1 / Math.Pow((1 + monthlyInterestRate), termMonths));

            return monthlyLoanPayment;
        }

        public List<ReadMonthlyLoanAmortizationDetail> CalculateMonthlyLoanAmortizationDetails(WriteLoan model)
        {
            var monthlyPayment = GetMonthlyLoanPaymentAmount(model);

            if (monthlyPayment == 0)
                return new List<ReadMonthlyLoanAmortizationDetail>();

            var balanceAmount = (double)model.Amount;
            var termMonths = model.Years * 12;

            var monthlyDetails = new List<ReadMonthlyLoanAmortizationDetail>();

            for (var count = 0; count < termMonths; count++)
            {
                var monthInterestAmount = GetInterestAmount(balanceAmount, (double)model.Apr);
                var monthPrincipal = monthlyPayment - monthInterestAmount;
                balanceAmount -= monthPrincipal;

                var monthlyDetail = new ReadMonthlyLoanAmortizationDetail(month: count + 1,
                    payment: monthlyPayment,
                    interestPaid: monthInterestAmount,
                    principalPaid: monthPrincipal,
                    balance: balanceAmount);

                monthlyDetails.Add(monthlyDetail);
            }

            return monthlyDetails;
        }

        public double GetTotalPrincipalPaid(int years, WriteLoan model)
        {
            var monthlyLoanAmortizationDetails = CalculateMonthlyLoanAmortizationDetails(model);
            var filteredMonthlyLoanAmortizationDetails = monthlyLoanAmortizationDetails.OrderBy(x => x.Month).Take(years * 12).ToList();

            return filteredMonthlyLoanAmortizationDetails.Sum(x => x.PrincipalPaid);
        }

        private double GetInterestAmount(double principal, double apr) =>
            principal * ((apr / 100) * (1.0 / 12.0));
    }
}