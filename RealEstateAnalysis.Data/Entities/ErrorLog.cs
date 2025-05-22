using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RealEstateAnalysis.Data.Entities
{
    public class ErrorLog : BaseEntity
    {
        #region Constructors

        internal ErrorLog()
        {
        }

        public ErrorLog(string className, string methodName, string values, string error, string userEmail)
        {
            ClassName = className;
            MethodName = methodName;
            Values = values;
            Error = error;
            UserEmail = userEmail;
        }

        #endregion Constructors

        #region Properties

        public string ClassName { get; private set; }

        public string MethodName { get; private set; }

        public string Values { get; private set; }

        public string Error { get; private set; }

        public string UserEmail { get; private set; }

        #endregion Properties
    }

    internal class ErrorLogConfiguration : IEntityTypeConfiguration<ErrorLog>
    {
        public void Configure(EntityTypeBuilder<ErrorLog> entity)
        {
            //Ignore
            entity.Ignore(x => x.ModifiedDate);

            //Primary Key
            entity.HasKey(x => x.Id);

            //Table & Columns
            entity.ToTable("ErrorsLog");
        }
    }
}