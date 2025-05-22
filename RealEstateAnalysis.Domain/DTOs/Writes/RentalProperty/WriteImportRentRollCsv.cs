using Microsoft.AspNetCore.Http;

namespace RealEstateAnalysis.Domain.DTOs.Writes.RentalProperty
{
    public class WriteImportRentRollCsv
    {
        public long PropertyId { get; set; }

        public IFormFile RentRollCsv { get; set; }
    }
}