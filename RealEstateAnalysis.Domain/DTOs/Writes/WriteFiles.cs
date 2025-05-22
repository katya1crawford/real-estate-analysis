using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace RealEstateAnalysis.Domain.DTOs.Writes
{
    public class WriteFiles
    {
        public long PropertyId { get; set; }

        public List<IFormFile> Files { get; set; }
    }
}