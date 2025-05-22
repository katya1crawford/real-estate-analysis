using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstateAnalysis.Domain.Abstract;
using RealEstateAnalysis.Domain.Abstract.RentalProperty;
using RealEstateAnalysis.Domain.DTOs.Writes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Web.Controllers.RentalProperty
{
    [Route("api/[controller]")]
    public class PdfController : Controller
    {
        private readonly IErrorLogService errorLogService;
        private readonly IPdfService pdfService;
        private readonly IPropertyService propertyService;

        public PdfController(IPropertyService propertyService, IPdfService pdfService, IErrorLogService errorLogService)
        {
            this.propertyService = propertyService;
            this.pdfService = pdfService;
            this.errorLogService = errorLogService;
        }

        [HttpGet("GetPropertySummaryHtml/{propertyId}")]
        public async Task<IActionResult> GetPropertySummaryHtml(long propertyId)
        {
            var property = await propertyService.GetByIdAsync(propertyId, includeNearbyPlaces: false);
            return View("PropertySummary", property);
        }

        [HttpGet("GetPropertySummaryPdf/{propertyId}")]
        public async Task<IActionResult> GetPropertySummaryPdf(long propertyId)
        {
            try
            {
                return Ok(await pdfService.GetPropertySummaryPdfAsync(propertyId));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(PdfController),
                    MethodName = nameof(GetPropertySummaryPdf),
                    Values = JsonConvert.SerializeObject(new Dictionary<string, object>
                    {
                        { nameof(propertyId), propertyId }
                    }),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}