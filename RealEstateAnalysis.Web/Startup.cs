using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RealEstateAnalysis.Data.Abstract;
using RealEstateAnalysis.Data.Entities;
using RealEstateAnalysis.Data.Repositories;
using RealEstateAnalysis.Domain;
using RealEstateAnalysis.Domain.Abstract;
using RealEstateAnalysis.Domain.Constants;
using RealEstateAnalysis.Domain.Services;
using RealEstateAnalysis.Domain.Settings;
using System.Text;
using LocationAnalysisDataRepositories = RealEstateAnalysis.Data.Repositories.LocationAnalysis;
using LocationAnalysisDateAbstract = RealEstateAnalysis.Data.Abstract.LocationAnalysis;
using LocationAnalysisDomainAbstract = RealEstateAnalysis.Domain.Abstract.LocationAnalysis;
using LocationAnalysisDomainServices = RealEstateAnalysis.Domain.Services.LocationAnalysis;
using RentalPropertyDataAbstract = RealEstateAnalysis.Data.Abstract.RentalProperty;
using RentalPropertyDataRepositories = RealEstateAnalysis.Data.Repositories.RentalProperty;
using RentalPropertyDomainAbstract = RealEstateAnalysis.Domain.Abstract.RentalProperty;
using RentalPropertyDomainServices = RealEstateAnalysis.Domain.Services.RentalProperty;
using ReonomyDataAbstract = RealEstateAnalysis.Data.Abstract.Reonomy;
using ReonomyDataRepositories = RealEstateAnalysis.Data.Repositories.Reonomy;

namespace RealEstateAnalysis.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddDbContext<EFDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:RealEstateAnalysis"], x => x.EnableRetryOnFailure()));
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.Lockout = new LockoutOptions()
                {
                    AllowedForNewUsers = true,
                    DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15),
                    MaxFailedAccessAttempts = 5
                };
            })
                .AddEntityFrameworkStores<EFDbContext>()
                .AddDefaultTokenProviders();

            var jwtSettings = Configuration.GetSection("JwtSettings").Get<JwtSettings>();

            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                sharedOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                sharedOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = true;
                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.ValidIssuer,

                    ValidateAudience = true,
                    ValidAudience = jwtSettings.ValidAudience,

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.PrivateKey))
                };
                config.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .RequireRole(UserRoles.Member)
                    .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddHttpClient();

            services.AddOptions();
            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));
            services.Configure<ApiSettings>(Configuration.GetSection("ApiSettings"));
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<LocationAnalysisSettings>(Configuration.GetSection("LocationAnalysisSettings"));

            //DI Configuration
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Rental Property
            services.AddTransient<RentalPropertyDataAbstract.IPropertyRepository, RentalPropertyDataRepositories.PropertyRepository>();
            services.AddTransient<RentalPropertyDataAbstract.IFileContentRepository, RentalPropertyDataRepositories.FileContentRepository>();
            services.AddTransient<RentalPropertyDataAbstract.IGalleryImageRepository, RentalPropertyDataRepositories.GalleryImageRepository>();
            services.AddTransient<RentalPropertyDomainAbstract.IPropertyService, RentalPropertyDomainServices.PropertyService>();
            services.AddTransient<RentalPropertyDomainAbstract.ICalculatorService, RentalPropertyDomainServices.CalculatorService>();
            services.AddTransient<RentalPropertyDomainAbstract.IFileContentService, RentalPropertyDomainServices.FileContentService>();
            services.AddTransient<RentalPropertyDomainAbstract.IGalleryImageService, RentalPropertyDomainServices.GalleryImageService>();
            services.AddTransient<RentalPropertyDomainAbstract.IPdfService, RentalPropertyDomainServices.PdfService>();
            services.AddTransient<RentalPropertyDomainAbstract.IRentRollService, RentalPropertyDomainServices.RentRollService>();

            //Location Analysis
            services.AddTransient<LocationAnalysisDateAbstract.ICityRepository, LocationAnalysisDataRepositories.CityRepository>();
            services.AddTransient<LocationAnalysisDateAbstract.INeighborhoodRepository, LocationAnalysisDataRepositories.NeighborhoodRepository>();
            services.AddTransient<LocationAnalysisDomainAbstract.ICityService, LocationAnalysisDomainServices.CityService>();
            services.AddTransient<LocationAnalysisDomainAbstract.INeighborhoodService, LocationAnalysisDomainServices.NeighborhoodService>();
            services.AddTransient<LocationAnalysisDateAbstract.ICityDataRepository, LocationAnalysisDataRepositories.CityDataRepository>();

            //Shared
            services.AddTransient<ISoldPropertyService, SoldPropertyService>();
            services.AddTransient<ReonomyDataAbstract.ISoldPropertyRepository, ReonomyDataRepositories.SoldPropertyRepository>();
            services.AddTransient<IContactUsService, ContactUsService>();
            services.AddTransient<ICryptographyService, CryptographyService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IErrorLogRepository, ErrorLogRepository>();
            services.AddTransient<IErrorLogService, ErrorLogService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<ILoanService, LoanService>();
            services.AddTransient<ILookupRepository, LookupRepository>();
            services.AddTransient<ILookupService, LookupService>();
            services.AddTransient<IMembershipService, MembershipService>();
            services.AddTransient<IMonetaryTransactionRepository, MonetaryTransactionRepository>();
            services.AddTransient<IMonetaryTransactionService, MonetaryTransactionService>();
            services.AddTransient<IGoogleGeocodeApiService, GoogleGeocodeApiService>();
            services.AddTransient<IGooglePlaceApiService, GooglePlaceApiService>();
            services.AddTransient<IZipwiseApiService, ZipwiseApiService>();
            services.AddTransient<IDataScraperService, DataScraperService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}