using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstateAnalysis.Domain.Abstract;
using RealEstateAnalysis.Domain.Abstract.RentalProperty;
using RealEstateAnalysis.Domain.DTOs.Writes;
using RealEstateAnalysis.Domain.DTOs.Writes.RentalProperty;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Web.Controllers.RentalProperty
{
    [Route("api/[controller]")]
    public class RentalPropertyCalculatorController : Controller
    {
        private readonly ICalculatorService calculatorService;
        private readonly IErrorLogService errorLogService;
        private readonly IPropertyService propertyService;

        public RentalPropertyCalculatorController(ICalculatorService calculatorService, IPropertyService propertyService, IErrorLogService errorLogService)
        {
            this.calculatorService = calculatorService;
            this.propertyService = propertyService;
            this.errorLogService = errorLogService;
        }

        [HttpPost("FinancialSummary")]
        public async Task<IActionResult> PostFinancialSummary([FromBody] WriteFinancialSummary model)
        {
            try
            {
                return Ok(calculatorService.GetFinancialSummary(model));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(RentalPropertyCalculatorController),
                    MethodName = nameof(PostFinancialSummary),
                    Values = JsonConvert.SerializeObject(model),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("LongTermFinancialForecasts/{propertyId}")]
        public async Task<IActionResult> GetLongTermFinancialForecast(long propertyId)
        {
            try
            {
                var property = await propertyService.GetByIdAsync(propertyId, includeNearbyPlaces: false);
                return Ok(calculatorService.GetLongTermFinancialForecasts(property));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(RentalPropertyCalculatorController),
                    MethodName = nameof(GetLongTermFinancialForecast),
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