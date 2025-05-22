using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstateAnalysis.Data.Entities
{
    public class MonetaryTransaction : BaseEntity
    {
        #region Constructors

        internal MonetaryTransaction()
        {
        }

        public MonetaryTransaction(decimal amount, decimal balance, string transactionNumber, string description, string userId)
        {
            Amount = Math.Round(amount, 2);
            Balance = Math.Round(balance, 2);
            TransactionNumber = transactionNumber;
            Description = description;
            UserId = userId;
        }

        #endregion Constructors

        #region Properties

        public decimal Amount { get; private set; }

        public decimal Balance { get; private set; }

        public string TransactionNumber { get; private set; }

        public string Description { get; private set; }

        public string UserId { get; private set; }

        internal User User { get; private set; }

        #endregion Properties
    }

    internal class MonetaryTransactionConfiguration : IEntityTypeConfiguration<MonetaryTransaction>
    {
        public void Configure(EntityTypeBuilder<MonetaryTransaction> entity)
        {
            //Ignore
            entity.Ignore(x => x.ModifiedDate);

            //Primary Key
            entity.HasKey(x => x.Id);

            //Properties
            entity.Property(x => x.TransactionNumber)
                .HasMaxLength(150);

            entity.Property(x => x.Description)
               .HasMaxLength(450);

            entity.Property(x => x.Description)
                .HasMaxLength(500);

            //Relationships
            entity.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            //Table & Columns
            entity.ToTable("MonetaryTransactions");
        }
    }
}