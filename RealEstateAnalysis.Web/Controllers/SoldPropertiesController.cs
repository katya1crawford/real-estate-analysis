using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstateAnalysis.Domain.Abstract;
using RealEstateAnalysis.Domain.DTOs.Writes;
using System;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Web.Controllers
{
    [Route("api/[controller]")]
    public class SoldPropertiesController : Controller
    {
        private readonly ISoldPropertyService soldPropertyService;
        private readonly IErrorLogService errorLogService;

        public SoldPropertiesController(ISoldPropertyService soldPropertyService, IErrorLogService errorLogService)
        {
            this.soldPropertyService = soldPropertyService;
            this.errorLogService = errorLogService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("LoadDataIntoDatabase")]
        public async Task<IActionResult> LoadDataIntoDatabase()
        {
            try
            {
                await soldPropertyService.LoadDataIntoDatabase();
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(SoldPropertiesController),
                    MethodName = nameof(LoadDataIntoDatabase),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateSoldPropertiesWithGeocodeData")]
        public async Task<IActionResult> UpdateSoldPropertiesWithGeocodeData()
        {
            try
            {
                await soldPropertyService.UpdateSoldPropertiesWithGeocodeData();
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(SoldPropertiesController),
                    MethodName = nameof(UpdateSoldPropertiesWithGeocodeData),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost("Search")]
        public async Task<IActionResult> Search([FromBody] WriteSoldPropertiesSearch model)
        {
            try
            {
                return Ok(await soldPropertyService.Search(model));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(SoldPropertiesController),
                    MethodName = nameof(Search),
                    Values = JsonConvert.SerializeObject(model),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}