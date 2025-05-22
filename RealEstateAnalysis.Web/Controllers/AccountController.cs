using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RealEstateAnalysis.Domain.Abstract;
using RealEstateAnalysis.Domain.DTOs.Writes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Web.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IErrorLogService errorLogService;
        private readonly IMembershipService membershipService;

        public AccountController(IMembershipService membershipService, IErrorLogService errorLogService)
        {
            this.membershipService = membershipService;
            this.errorLogService = errorLogService;
        }

        [HttpPost("ConfirmEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromBody] WriteConfirmEmail model)
        {
            try
            {
                await membershipService.ConfirmEmailAsync(model);
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
                    ClassName = nameof(AccountController),
                    MethodName = nameof(ConfirmEmail),
                    Values = JsonConvert.SerializeObject(model),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpDelete("DeleteAccount")]
        public async Task<IActionResult> DeleteAccount()
        {
            try
            {
                await membershipService.DeleteAccount();
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
                    ClassName = nameof(AccountController),
                    MethodName = nameof(DeleteAccount),
                    Values = string.Empty,
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpGet("GetLoggedInUser")]
        public async Task<IActionResult> GetLoggedInUser()
        {
            try
            {
                return Ok(membershipService.GetLoggedInUser());
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(AccountController),
                    MethodName = nameof(GetLoggedInUser),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost("RefreshToken")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromBody] WriteRefreshToken model)
        {
            try
            {
                return Ok(await membershipService.RefreshTokenAsync(model));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(AccountController),
                    MethodName = nameof(RefreshToken),
                    Values = JsonConvert.SerializeObject(model),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] WriteRegistration model)
        {
            try
            {
                await membershipService.RegisterAsync(model);
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
                    ClassName = nameof(AccountController),
                    MethodName = nameof(Register),
                    Values = JsonConvert.SerializeObject(model),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost("RequestPasswordReset")]
        [AllowAnonymous]
        public async Task<IActionResult> RequestPasswordReset([FromBody] WriteRequestPasswordReset model)
        {
            try
            {
                await membershipService.EmailPasswordResetLinkAsync(model);
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
                    ClassName = nameof(AccountController),
                    MethodName = nameof(RequestPasswordReset),
                    Values = JsonConvert.SerializeObject(model),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] WritePasswordReset model)
        {
            try
            {
                await membershipService.ResetPasswordAsync(model);
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
                    ClassName = nameof(AccountController),
                    MethodName = nameof(ResetPassword),
                    Values = JsonConvert.SerializeObject(model),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPost("SignIn")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn([FromBody] WriteSignIn model)
        {
            try
            {
                return Ok(await membershipService.SignInAsync(model));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(AccountController),
                    MethodName = nameof(SignIn),
                    Values = JsonConvert.SerializeObject(new Dictionary<string, object>
                    {
                        { nameof(model.Email), model.Email }
                    }),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] WriteUpdateUser model)
        {
            try
            {
                return Ok(await membershipService.UpdateUserAsync(model));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ValidationResult(ex.Errors));
            }
            catch (Exception ex)
            {
                await errorLogService.LogErrorAsync(new WriteErrorLog()
                {
                    ClassName = nameof(AccountController),
                    MethodName = nameof(UpdateUser),
                    Values = JsonConvert.SerializeObject(model),
                    Error = JsonConvert.SerializeObject(ex)
                });

                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
    }
}