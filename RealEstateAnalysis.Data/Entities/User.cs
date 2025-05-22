using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstateAnalysis.Data.Entities
{
    public class User : IdentityUser
    {
        #region Constructors

        internal User()
        {
        }

        public User(string firstName, string lastName, string email)
        {
            UserName = email;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }

        #endregion Constructors

        #region Properties

        public virtual string FirstName { get; private set; }

        public virtual string LastName { get; private set; }

        public virtual string RefreshToken { get; private set; }

        public virtual DateTime? RefreshTokenExpirationDate { get; private set; }

        #endregion Properties

        #region Commands

        public void Update(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public void UpdateRefreshToken(string refreshToken, double jwtTokenExpiresInHours)
        {
            RefreshToken = refreshToken;
            RefreshTokenExpirationDate = DateTime.Now.AddHours(jwtTokenExpiresInHours).AddMinutes(15);
        }

        #endregion Commands
    }

    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            //Properties
            entity.Property(x => x.FirstName)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(x => x.LastName)
                .HasMaxLength(50)
                .IsRequired();

            entity.Property(x => x.RefreshToken)
                .HasMaxLength(500);
        }
    }
}