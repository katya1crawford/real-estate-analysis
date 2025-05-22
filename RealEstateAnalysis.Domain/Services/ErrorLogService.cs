using Microsoft.AspNetCore.Hosting;
using RealEstateAnalysis.Data.Abstract;
using RealEstateAnalysis.Data.Entities;
using RealEstateAnalysis.Domain.Abstract;
using RealEstateAnalysis.Domain.DTOs.Writes;

namespace RealEstateAnalysis.Domain.Services
{
    public class ErrorLogService : IErrorLogService
    {
        private readonly IErrorLogRepository errorLogRepository;
        private readonly IMembershipService membershipService;
        private readonly IHostingEnvironment environment;

        public ErrorLogService(IErrorLogRepository errorLogRepository, IMembershipService membershipService, IHostingEnvironment environment)
        {
            this.errorLogRepository = errorLogRepository;
            this.membershipService = membershipService;
            this.environment = environment;
        }

        public async Task LogErrorAsync(WriteErrorLog model)
        {
            if (environment.IsDevelopment())
                return;

            var loggedInUser = membershipService.GetLoggedInUser();

            var errorLog = new ErrorLog(className: model.ClassName,
                methodName: model.MethodName,
                values: model.Values,
                error: model.Error,
                userEmail: loggedInUser?.Email);

            await errorLogRepository.SaveOrUpdateAsync(errorLog);
        }
    }
}