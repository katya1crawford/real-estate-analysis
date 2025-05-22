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
    public class NeighborhoodsController : Controller
    {
        private readonly IErrorLogService errorLogService;
        private readonly INeighborhoodService neighborhoodService;

        public NeighborhoodsController(IErrorLogService errorLogService, INeighborhoodService neighborhoodService)
        {
            this.errorLogService = errorLogService;
            this.neighborhoodService = neighborhoodService;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                await neighborhoodService.DeleteAsync(id);
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
                    ClassName = nameof(NeighborhoodsController),
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
                return Ok(await neighborhoodService.GetAllAsync());
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(NeighborhoodsController),
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
                return Ok(await neighborhoodService.GetByIdAsync(id));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(NeighborhoodsController),
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
        public async Task<IActionResult> Post([FromBody] WriteNeighborhood model)
        {
            try
            {
                return Ok(await neighborhoodService.AddAsync(model));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(NeighborhoodsController),
                    MethodName = nameof(Post),
                    Values = JsonConvert.SerializeObject(model),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody] WriteNeighborhood model)
        {
            try
            {
                return Ok(await neighborhoodService.UpdateAsync(id, model));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(NeighborhoodsController),
                    MethodName = nameof(Put),
                    Values = JsonConvert.SerializeObject(new Dictionary<string, object>
                    {
                        { nameof(id), id },
                        { nameof(WriteNeighborhood), model }
                    }),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}
