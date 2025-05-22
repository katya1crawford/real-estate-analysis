using RealEstateAnalysis.Domain.DTOs.Reads;
using RealEstateAnalysis.Domain.DTOs.Writes;
using System.Threading.Tasks;

namespace RealEstateAnalysis.Domain.Abstract
{
    public interface IMembershipService
    {
        Task ConfirmEmailAsync(WriteConfirmEmail model);

        Task DeleteAccount();

        Task EmailPasswordResetLinkAsync(WriteRequestPasswordReset model);

        ReadUser GetLoggedInUser();

        string GetLoggedInUserJwtToken();

        Task<ReadAuthResponse> RefreshTokenAsync(WriteRefreshToken model);

        Task RegisterAsync(WriteRegistration model);

        Task ResetPasswordAsync(WritePasswordReset model);

        Task<ReadAuthResponse> SignInAsync(WriteSignIn model);

        Task<ReadUser> UpdateUserAsync(WriteUpdateUser model);
    }
}