using FluentValidation;
using FluentValidation.Results;
using RealEstateAnalysis.Data.Abstract;
using RealEstateAnalysis.Data.Entities;
using RealEstateAnalysis.Domain.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Domain.Services
{
    public class MonetaryTransactionService : IMonetaryTransactionService
    {
        private readonly IMembershipService membershipService;
        private readonly IMonetaryTransactionRepository monetaryTransactionRepository;

        public MonetaryTransactionService(IMonetaryTransactionRepository monetaryTransactionRepository, IMembershipService membershipService)
        {
            this.membershipService = membershipService;
            this.monetaryTransactionRepository = monetaryTransactionRepository;
        }

        public async Task AddMoneyAsync(decimal amount, string transactionNumber, string description)
        {
            if (amount == 0)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Transaction amount must be more equal 0.") });

            var loggedInUser = membershipService.GetLoggedInUser();
            var recentTransaction = await monetaryTransactionRepository.GetRecent(loggedInUser.Id);
            var newBalance = recentTransaction != null ? recentTransaction.Balance : 0;

            var newTransaction = new MonetaryTransaction(amount: amount,
                balance: newBalance,
                transactionNumber: transactionNumber,
                description: description,
                userId: loggedInUser.Id);

            await monetaryTransactionRepository.SaveAsync(newTransaction);
        }
    }
}