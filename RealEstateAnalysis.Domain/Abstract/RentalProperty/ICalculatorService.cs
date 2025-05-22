using RealEstateAnalysis.Domain.DTOs.Reads.RentalProperty;
using RealEstateAnalysis.Domain.DTOs.Writes.RentalProperty;

namespace RealEstateAnalysis.Domain.Abstract.RentalProperty
{
    public interface ICalculatorService
    {
        ReadFinancialSummary GetFinancialSummary(WriteFinancialSummary model);

        List<ReadFinancialForecast> GetLongTermFinancialForecasts(ReadProperty property);
    }
}