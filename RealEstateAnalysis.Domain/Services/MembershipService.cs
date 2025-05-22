using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RealEstateAnalysis.Data.Entities;
using RealEstateAnalysis.Domain.Abstract;
using RealEstateAnalysis.Domain.Constants;
using RealEstateAnalysis.Domain.DTOs.Reads;
using RealEstateAnalysis.Domain.DTOs.Writes;
using RealEstateAnalysis.Domain.Settings;
using RealEstateAnalysis.Domain.Validators;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ValidationFailure = FluentValidation.Results.ValidationFailure;

namespace RealEstateAnalysis.Domain.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly AppSettings appSettings;
        private readonly IEmailService emailService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly JwtSettings jwtSettings;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public MembershipService(IOptions<JwtSettings> jwtOptions,
            IOptions<AppSettings> appOptions,
            IHttpContextAccessor httpContextAccessor,
            IEmailService emailService,
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            jwtSettings = jwtOptions.Value;
            appSettings = appOptions.Value;
            this.httpContextAccessor = httpContextAccessor;
            this.emailService = emailService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task ConfirmEmailAsync(WriteConfirmEmail model)
        {
            var validator = new ConfirmEmailValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var user = await userManager.FindByIdAsync(model.UserId);

            if (user == null)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid user Id.") });

            var result = await userManager.ConfirmEmailAsync(user, model.Token);

            if (result.Succeeded == false)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Unable to confirm email.") });
        }

        public async Task DeleteAccount()
        {
            var loggedInUser = GetLoggedInUser();
            var user = await userManager.FindByIdAsync(loggedInUser.Id);
            await userManager.DeleteAsync(user);
        }

        public async Task EmailPasswordResetLinkAsync(WriteRequestPasswordReset model)
        {
            var validator = new RequestPasswordResetValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var user = await userManager.FindByNameAsync(model.Email);

            if (user == null)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid email address.") });

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var urlFriendlyToken = WebUtility.UrlEncode(token);

            var message = $@"Hi {user.FirstName}, <br /><br />
                We've received a request to reset you password. If you didn't make the request,
                just ignore this email. Otherwise, you can reset your password using this link:
                <a href=""{appSettings.BaseUrl}/reset-password?userId={user.Id}&token={urlFriendlyToken}"" target=""_blank"">Click here to reset your password.</a><br /><br />
                Thanks, <br />
                ${appSettings.BusinessName}";

            var subject = "Password Reset";
            await emailService.SendAsync(new WriteEmail { ToEmail = user.Email, FromEmail = appSettings.SupportEmail, Subject = subject, Message = message });
        }

        public ReadUser GetLoggedInUser()
        {
            if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated == false)
                return null;

            var id = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var email = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
            var firstName = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.GivenName).Value;
            var lastName = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Surname).Value;
            var roles = httpContextAccessor.HttpContext.User.FindAll(ClaimTypes.Role).Select(x => x.Value).ToList();

            return new ReadUser(id: id,
                firstName: firstName,
                lastName: lastName,
                email: email,
                roles: roles);
        }

        public string GetLoggedInUserJwtToken() =>
            httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Substring("Bearer ".Length).Trim();

        public async Task<ReadAuthResponse> RefreshTokenAsync(WriteRefreshToken model)
        {
            var validator = new RefreshTokenValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var principal = GetPrincipalFromExpiredToken(model.Token);
            var userId = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userId))
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid refresh token.") });

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid refresh token.") });

            var savedRefreshToken = user.RefreshToken;

            if (savedRefreshToken != model.RefreshToken || user.RefreshTokenExpirationDate < DateTime.Now)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid refresh token.") });

            var jwtToken = await GetJwtTokenAsync(user);
            var refreshToken = GetRefreshToken();
            user.UpdateRefreshToken(refreshToken, jwtSettings.JwtTokenExpiresInHours);
            await userManager.UpdateAsync(user);

            return new ReadAuthResponse(token: jwtToken, refreshToken: refreshToken);
        }

        public async Task RegisterAsync(WriteRegistration model)
        {
            var validator = new RegistrationValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var newUser = new User(firstName: model.FirstName,
                lastName: model.LastName,
                email: model.Email);

            var result = await userManager.CreateAsync(newUser, model.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(newUser, UserRoles.Member);

                if (appSettings.AdminEmail.Equals(model.Email.Trim(), StringComparison.InvariantCultureIgnoreCase))
                    await userManager.AddToRoleAsync(newUser, UserRoles.Admin);

                var token = await userManager.GenerateEmailConfirmationTokenAsync(newUser);
                var urlFriendlyToken = WebUtility.UrlEncode(token);
                var subject = $"Confirm your email";
                var message = $@"<p>Please confirm your account by <a href=""{appSettings.BaseUrl}/confirm-email?userId={newUser.Id}&token={urlFriendlyToken}"" target=""_blank"">clicking here</a>.</p>
                    <br />
                    Thanks, <br />
                    {appSettings.BusinessName}";

                await emailService.SendAsync(new WriteEmail { ToEmail = newUser.Email, FromEmail = appSettings.SupportEmail, Subject = subject, Message = message });
            }
            else
            {
                var validationErrors = new List<ValidationFailure>();

                foreach (var error in result.Errors)
                    validationErrors.Add(new ValidationFailure("", error.Description));

                throw new ValidationException(validationErrors);
            }
        }

        public async Task ResetPasswordAsync(WritePasswordReset model)
        {
            var validator = new PasswordResetValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var user = await userManager.FindByIdAsync(model.UserId);

            if (user == null)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "User not found.") });

            var result = await userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);

            if (result.Succeeded == false)
            {
                var validationErrors = new List<ValidationFailure>();

                foreach (var error in result.Errors)
                    validationErrors.Add(new ValidationFailure("", error.Description));

                throw new ValidationException(validationErrors);
            }
        }

        public async Task<ReadAuthResponse> SignInAsync(WriteSignIn model)
        {
            var validator = new SignInValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                var user = await userManager.FindByNameAsync(model.Email);
                var jwtToken = await GetJwtTokenAsync(user);
                var refreshToken = GetRefreshToken();
                user.UpdateRefreshToken(refreshToken, jwtSettings.JwtTokenExpiresInHours);
                await userManager.UpdateAsync(user);

                return new ReadAuthResponse(token: jwtToken, refreshToken: refreshToken);
            }
            else if (result.IsNotAllowed)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", $"You must confirm your email before being able to access {appSettings.BusinessName}. Click on the confirmation link in your email.") });
            else if (result.IsLockedOut)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", $"Your account has been locked due to excessive failed login attempts. Please contact wait 15 minutes and try again.") });
            else
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid email or password.") });
        }

        public async Task<ReadUser> UpdateUserAsync(WriteUpdateUser model)
        {
            var validator = new UpdateUserValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var loggedInUser = GetLoggedInUser();
            var user = await userManager.FindByIdAsync(loggedInUser.Id);
            var signInResult = await signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);

            if (signInResult.Succeeded == false)
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid password.") });

            user.Update(firstName: model.FirstName, lastName: model.LastName);
            await userManager.UpdateAsync(user);

            var roles = await userManager.GetRolesAsync(user);

            return new ReadUser(id: user.Id,
                firstName: user.FirstName,
                lastName: user.LastName,
                email: user.Email,
                roles: roles.ToList());
        }

        private async Task<string> GetJwtTokenAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName)
            };

            var roles = await userManager.GetRolesAsync(user);

            foreach (var role in roles)
                claims.Add(new Claim("role", role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.PrivateKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: jwtSettings.ValidIssuer,
                audience: jwtSettings.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddHours(jwtSettings.JwtTokenExpiresInHours),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string expiredToken)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidAudience = jwtSettings.ValidAudience,

                ValidateIssuer = true,
                ValidIssuer = jwtSettings.ValidIssuer,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.PrivateKey)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(expiredToken, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new ValidationException(new List<ValidationFailure> { new ValidationFailure("", "Invalid refresh token.") });

            return principal;
        }

        private string GetRefreshToken()
        {
            var randomNumber = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}