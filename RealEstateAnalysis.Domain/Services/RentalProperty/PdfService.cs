using iText.Html2pdf;
using Microsoft.Extensions.Options;
using RealEstateAnalysis.Domain.Abstract;
using RealEstateAnalysis.Domain.Abstract.RentalProperty;
using RealEstateAnalysis.Domain.DTOs.Reads;
using RealEstateAnalysis.Domain.Settings;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Domain.Services.RentalProperty
{
    public class PdfService : IPdfService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly AppSettings appSettings;
        private readonly IMembershipService membershipService;

        public PdfService(IHttpClientFactory httpClientFactory, IOptions<AppSettings> appOptions, IMembershipService membershipService)
        {
            this.httpClientFactory = httpClientFactory;
            appSettings = appOptions.Value;
            this.membershipService = membershipService;
        }

        public async Task<ReadFile> GetPropertySummaryPdfAsync(long propertyId)
        {
            var jwtToken = membershipService.GetLoggedInUserJwtToken();

            var httpClient = httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken);
            var uri = new Uri($"{appSettings.BaseUrl}/api/Pdf/GetPropertySummaryHtml/{propertyId}");
            var html = await httpClient.GetStringAsync(uri);

            using (var memoryStream = new MemoryStream())
            {
                HtmlConverter.ConvertToPdf(html, memoryStream);

                var pdfBytes = memoryStream.ToArray();

                return new ReadFile(id: 0,
                    name: "PropertySummary.pdf",
                    bytes: pdfBytes,
                    mimeType: "application/pdf",
                    createdDate: DateTime.Now);
            }
        }
    }
}