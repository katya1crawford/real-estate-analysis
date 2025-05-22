using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstateAnalysis.Domain.Abstract;
using RealEstateAnalysis.Domain.Abstract.RentalProperty;
using RealEstateAnalysis.Domain.DTOs.Writes;
using RealEstateAnalysis.Domain.DTOs.Writes.RentalProperty;
using RealEstateAnalysis.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Web.Controllers.RentalProperty
{
    [Route("api/[controller]")]
    public class RentalPropertiesController : Controller
    {
        private readonly IErrorLogService errorLogService;
        private readonly IPropertyService propertyService;

        public RentalPropertiesController(IPropertyService propertyService, IErrorLogService errorLogService)
        {
            this.propertyService = propertyService;
            this.errorLogService = errorLogService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await propertyService.DeleteAsync(id);
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
                    ClassName = nameof(RentalPropertiesController),
                    MethodName = nameof(Delete),
                    Values = JsonConvert.SerializeObject(new Dictionary<string, object>
                    {
                        { nameof(id), id }
                    }),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete("{propertyId}/DeleteThumbnailImage")]
        public async Task<IActionResult> DeleteThumbnailImage(long propertyId)
        {
            try
            {
                await propertyService.DeleteThumbnailImageAsync(propertyId);
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
                    ClassName = nameof(RentalPropertiesController),
                    MethodName = nameof(DeleteThumbnailImage),
                    Values = JsonConvert.SerializeObject(new Dictionary<string, object>
                    {
                        { nameof(propertyId), propertyId }
                    }),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("propertyStatus/{propertyStatus}")]
        public async Task<IActionResult> GetByPropertyStatus(PropertyStatusEnum propertyStatus)
        {
            try
            {
                return Ok(await propertyService.GetByStatus(propertyStatus));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(RentalPropertiesController),
                    MethodName = nameof(GetByPropertyStatus),
                    Values = JsonConvert.SerializeObject(new Dictionary<string, object>
                    {
                        { nameof(propertyStatus), propertyStatus }
                    }),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("groupName/{groupName}")]
        public async Task<IActionResult> GetByGroupName(string groupName)
        {
            try
            {
                return Ok(await propertyService.GetByGroupNameAsync(groupName));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(RentalPropertiesController),
                    MethodName = nameof(GetByGroupName),
                    Values = JsonConvert.SerializeObject(new Dictionary<string, object>
                    {
                        { nameof(groupName), groupName }
                    }),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("GetAllGroupNames")]
        public async Task<IActionResult> GetAllGroupNames()
        {
            try
            {
                return Ok(await propertyService.GetAllGroupNamesAsync());
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(RentalPropertiesController),
                    MethodName = nameof(GetAllGroupNames),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("GetAllSubjectPropertyLookups")]
        public async Task<IActionResult> GetAllSubjectPropertyLookups()
        {
            try
            {
                return Ok(await propertyService.GetAllSubjectPropertyLookupsAsync());
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(RentalPropertiesController),
                    MethodName = nameof(GetAllSubjectPropertyLookups),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{id}/{includeNearbyPlaces?}")]
        public async Task<IActionResult> Get(long id, bool includeNearbyPlaces = false)
        {
            try
            {
                return Ok(await propertyService.GetByIdAsync(id, includeNearbyPlaces));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(RentalPropertiesController),
                    MethodName = nameof(Get),
                    Values = JsonConvert.SerializeObject(new Dictionary<string, object>
                    {
                        { nameof(id), id }
                    }),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] WriteProperty model)
        {
            try
            {
                return Ok(await propertyService.AddAsync(model));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(RentalPropertiesController),
                    MethodName = nameof(Post),
                    Values = JsonConvert.SerializeObject(model),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromForm] WriteProperty model)
        {
            try
            {
                return Ok(await propertyService.UpdateAsync(id, model));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(RentalPropertiesController),
                    MethodName = nameof(Put),
                    Values = JsonConvert.SerializeObject(new Dictionary<string, object>
                    {
                        { nameof(id), id },
                        { nameof(WriteProperty), model }
                    }),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}