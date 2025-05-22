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
    public class RentalPropertyFilesController : Controller
    {
        private readonly IPropertyService propertyService;
        private readonly IFileContentService fileContentService;
        private readonly IErrorLogService errorLogService;

        public RentalPropertyFilesController(IPropertyService propertyService, IFileContentService fileContentService, IErrorLogService errorLogService)
        {
            this.propertyService = propertyService;
            this.errorLogService = errorLogService;
            this.fileContentService = fileContentService;
        }

        [HttpGet("{rentalPropertyId}/{fileId}")]
        public async Task<IActionResult> Get(long rentalPropertyId, long fileId)
        {
            try
            {
                return Ok(await fileContentService.GetAsync(rentalPropertyId, fileId));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(RentalPropertyFilesController),
                    MethodName = nameof(Get),
                    Values = JsonConvert.SerializeObject(new Dictionary<string, object>
                    {
                        { nameof(rentalPropertyId), rentalPropertyId },
                        { nameof(fileId), fileId }
                    }),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] WriteFiles model)
        {
            try
            {
                return Ok(await propertyService.AddFilesAsync(model));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(RentalPropertyFilesController),
                    MethodName = nameof(Post),
                    Values = JsonConvert.SerializeObject(model),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete("{propertyId}/{fileId}")]
        public async Task<IActionResult> Delete(long propertyId, long fileId)
        {
            try
            {
                await propertyService.DeleteFileAsync(propertyId, fileId);
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
                    ClassName = nameof(RentalPropertyFilesController),
                    MethodName = nameof(Delete),
                    Values = JsonConvert.SerializeObject(new Dictionary<string, object>
                    {
                        { nameof(propertyId), propertyId },
                        { nameof(fileId), fileId }
                    }),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}