using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstateAnalysis.Domain.Abstract;
using RealEstateAnalysis.Domain.Abstract.LocationAnalysis;
using RealEstateAnalysis.Domain.DTOs.Writes;
using RealEstateAnalysis.Domain.DTOs.Writes.LocationAnalysis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Web.Controllers.LocationAnalysis
{
    [Route("api/LocationAnalysis/[controller]")]
    public class CitiesController : Controller
    {
        private readonly IErrorLogService errorLogService;
        private readonly ICityService cityService;
        private readonly IDataScraperService dataScraperService;

        public CitiesController(IErrorLogService errorLogService, ICityService cityService, IDataScraperService dataScraperService)
        {
            this.errorLogService = errorLogService;
            this.cityService = cityService;
            this.dataScraperService = dataScraperService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await cityService.DeleteAsync(id);
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
                    ClassName = nameof(CitiesController),
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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await cityService.GetAllAsync());
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(CitiesController),
                    MethodName = nameof(Get),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                return Ok(await cityService.GetByIdAsync(id));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(CitiesController),
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
        public async Task<IActionResult> Post([FromBody] WriteCity model)
        {
            try
            {
                return Ok(await cityService.AddAsync(model));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(CitiesController),
                    MethodName = nameof(Post),
                    Values = JsonConvert.SerializeObject(model),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] WriteCity model)
        {
            try
            {
                return Ok(await cityService.UpdateAsync(id, model));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(CitiesController),
                    MethodName = nameof(Put),
                    Values = JsonConvert.SerializeObject(new Dictionary<string, object>
                    {
                        { nameof(id), id },
                        { nameof(WriteCity), model }
                    }),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut("ToggleIsFavorite/{id}")]
        public async Task<IActionResult> ToggleIsFavorite(long id)
        {
            try
            {
                await cityService.ToggleIsFavorite(id);
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
                    ClassName = nameof(CitiesController),
                    MethodName = nameof(ToggleIsFavorite),
                    Values = JsonConvert.SerializeObject(new Dictionary<string, object>
                    {
                        { nameof(id), id },
                    }),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost("HarvestCityData/{minimumPopulationCount}")]
        public async Task<IActionResult> HarvestCityData([FromRoute] int minimumPopulationCount)
        {
            try
            {
                await cityService.HarvestCityData(minimumPopulationCount);
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
                    ClassName = nameof(CitiesController),
                    MethodName = nameof(HarvestCityData),
                    Values = JsonConvert.SerializeObject(new Dictionary<string, object>
                    {
                        { nameof(minimumPopulationCount), minimumPopulationCount },
                    }),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}