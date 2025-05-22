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
    public class RentalPropertyRentRollController : Controller
    {
        private readonly IRentRollService rentRollService;
        private readonly IErrorLogService errorLogService;

        public RentalPropertyRentRollController(IRentRollService rentRollService, IErrorLogService errorLogService)
        {
            this.rentRollService = rentRollService;
            this.errorLogService = errorLogService;
        }

        [HttpGet("Summary/{propertyId}")]
        public async Task<IActionResult> GetSummary(long propertyId)
        {
            try
            {
                return Ok(await rentRollService.GetRentRollSummary(propertyId));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(RentalPropertyRentRollController),
                    MethodName = nameof(GetSummary),
                    Values = JsonConvert.SerializeObject(new Dictionary<string, object>
                    {
                        { nameof(propertyId), propertyId },
                    }),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost("ImportRentRollCsv")]
        public async Task<IActionResult> ImportRentRollCsv([FromForm] WriteImportRentRollCsv model)
        {
            try
            {
                return Ok(await rentRollService.ImportRentRollCsv(model));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(RentalPropertyRentRollController),
                    MethodName = nameof(ImportRentRollCsv),
                    Values = JsonConvert.SerializeObject(model),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}