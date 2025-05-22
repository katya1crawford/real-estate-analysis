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
    [AllowAnonymous]
    public class ContactUsController : Controller
    {
        private readonly IContactUsService contactUsService;
        private readonly IErrorLogService errorLogService;

        public ContactUsController(IContactUsService contactUsService, IErrorLogService errorLogService)
        {
            this.contactUsService = contactUsService;
            this.errorLogService = errorLogService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WriteContactUs model)
        {
            try
            {
                await contactUsService.SendEmailAsync(model);
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
                    ClassName = nameof(ContactUsController),
                    MethodName = nameof(Post),
                    Values = JsonConvert.SerializeObject(model),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}