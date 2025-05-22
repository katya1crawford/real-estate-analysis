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
    public class RentalPropertyGalleryImageController : Controller
    {
        private readonly IErrorLogService errorLogService;
        private readonly IGalleryImageService galleryImageService;

        public RentalPropertyGalleryImageController(IErrorLogService errorLogService, IGalleryImageService galleryImageService)
        {
            this.errorLogService = errorLogService;
            this.galleryImageService = galleryImageService;
        }

        [HttpDelete("{propertyId}/{galleryImageId}")]
        public async Task<IActionResult> Delete(long propertyId, long galleryImageId)
        {
            try
            {
                await galleryImageService.DeleteAsync(galleryImageId, propertyId);
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
                    ClassName = nameof(RentalPropertyGalleryImageController),
                    MethodName = nameof(Delete),
                    Values = JsonConvert.SerializeObject(new Dictionary<string, object>
                    {
                        { nameof(galleryImageId), galleryImageId },
                        { nameof(propertyId), propertyId }
                    }),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{propertyId}/GetAllLarge")]
        public async Task<IActionResult> GetAllLarge(long propertyId)
        {
            try
            {
                return Ok(await galleryImageService.GetAllLargeAsync(propertyId));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(RentalPropertyGalleryImageController),
                    MethodName = nameof(GetAllLarge),
                    Values = JsonConvert.SerializeObject(new Dictionary<string, object>
                    {
                        { nameof(propertyId), propertyId }
                    }),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{propertyId}/GetAllSmall")]
        public async Task<IActionResult> GetAllSmall(long propertyId)
        {
            try
            {
                return Ok(await galleryImageService.GetAllSmallAsync(propertyId));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(RentalPropertyGalleryImageController),
                    MethodName = nameof(GetAllSmall),
                    Values = JsonConvert.SerializeObject(new Dictionary<string, object>
                    {
                        { nameof(propertyId), propertyId }
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
                return Ok(await galleryImageService.AddAsync(model));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(RentalPropertyGalleryImageController),
                    MethodName = nameof(Post),
                    Values = JsonConvert.SerializeObject(model),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}