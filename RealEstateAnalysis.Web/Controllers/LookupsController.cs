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
    public class LookupsController : Controller
    {
        private readonly IErrorLogService errorLogService;
        private readonly ILookupService lookupService;

        public LookupsController(ILookupService lookupService, IErrorLogService errorLogService)
        {
            this.lookupService = lookupService;
            this.errorLogService = errorLogService;
        }

        [HttpGet("ClosingCostTypes")]
        public async Task<IActionResult> GetClosingCostTypes()
        {
            try
            {
                return Ok(await lookupService.GetAllClosingCostTypesAsync());
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(LookupsController),
                    MethodName = nameof(GetClosingCostTypes),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("ExteriorRepairExpenseTypes")]
        public async Task<IActionResult> GetExteriorRepairExpenseTypes()
        {
            try
            {
                return Ok(await lookupService.GetAllExteriorRepairExpenseTypesAsync());
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(LookupsController),
                    MethodName = nameof(GetExteriorRepairExpenseTypes),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("GeneralRepairExpenseTypes")]
        public async Task<IActionResult> GetGeneralRepairExpenseTypes()
        {
            try
            {
                return Ok(await lookupService.GetAllGeneralRepairExpenseTypesAsync());
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(LookupsController),
                    MethodName = nameof(GetGeneralRepairExpenseTypes),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("InteriorRepairExpenseTypes")]
        public async Task<IActionResult> GetInteriorRepairExpenseTypes()
        {
            try
            {
                return Ok(await lookupService.GetAllInteriorRepairExpenseTypesAsync());
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(LookupsController),
                    MethodName = nameof(GetInteriorRepairExpenseTypes),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("OperatingExpenseTypes")]
        public async Task<IActionResult> GetOperatingExpenseTypes()
        {
            try
            {
                return Ok(await lookupService.GetAllOperatingExpenseTypesAsync());
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(LookupsController),
                    MethodName = nameof(GetOperatingExpenseTypes),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("PropertyTypes")]
        public async Task<IActionResult> GetPropertyTypes()
        {
            try
            {
                return Ok(await lookupService.GetAllPropertyTypesAsync());
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(LookupsController),
                    MethodName = nameof(GetPropertyTypes),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("PropertyStatuses")]
        public async Task<IActionResult> GetPropertyStatuses()
        {
            try
            {
                return Ok(await lookupService.GetAllPropertyStatusesAsync());
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(LookupsController),
                    MethodName = nameof(GetPropertyStatuses),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("States")]
        public async Task<IActionResult> GetStates()
        {
            try
            {
                return Ok(await lookupService.GetAllStatesAsync());
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(LookupsController),
                    MethodName = nameof(GetStates),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}