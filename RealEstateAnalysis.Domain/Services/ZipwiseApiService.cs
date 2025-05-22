using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RealEstateAnalysis.Domain.Abstract;
using RealEstateAnalysis.Domain.DTOs.ThirdParty.Zipwise;
using RealEstateAnalysis.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Domain.Services
{
    public class ZipwiseApiService : IZipwiseApiService
    {
        private readonly ApiSettings apiSettings;

        public ZipwiseApiService(IOptions<ApiSettings> apiOptions)
        {
            apiSettings = apiOptions.Value;
        }

        public async Task<List<string>> GetZipCodesInRadiusAsync(string zipCode, int radius)
        {
            using var client = new HttpClient();
            var uri = Uri.EscapeUriString(string.Format(apiSettings.ZipwiseUrl, zipCode.Substring(0, 5), radius));
            var response = await client.GetAsync(uri);
            var zipwiseJson = await response.Content.ReadAsStringAsync();
            var zipwise = JsonConvert.DeserializeObject<Zipwise>(zipwiseJson);

            return zipwise.Results.Select(x => x.ZipCode).ToList();
        }
    }
}