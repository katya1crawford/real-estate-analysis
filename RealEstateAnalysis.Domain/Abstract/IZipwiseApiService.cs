using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Domain.Abstract
{
    public interface IZipwiseApiService
    {
        Task<List<string>> GetZipCodesInRadiusAsync(string zipCode, int radius);
    }
}