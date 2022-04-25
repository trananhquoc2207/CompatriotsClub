using CompatriotsClub.Application.Extensions;
using CompatriotsClub.Data;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.EntityFrameworkCore;
using Service.Core;
using Service.DI;
using ViewModel.Mapper;

namespace HRSystem.Application
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                             .SetBasePath(env.ContentRootPath)
                             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                             .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                             .AddEnvironmentVariables();

            Environment = env;
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            #region old
            ApiDIConfig.AddDependencies(services);
            MapperConfig.Config(services, Configuration);

            // upload post size


            services.AddMvc()
            .AddSessionStateTempDataProvider();
            services.AddSession();
            #endregion

            #region Hangfire
            services.AddHangfire(configuration => configuration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(
                        Configuration.GetConnectionString("Hangfire"),
                        new SqlServerStorageOptions
                        {
                            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                            QueuePollInterval = TimeSpan.Zero,
                            UseRecommendedIsolationLevel = true,
                            DisableGlobalLocks = true
                        }
                    ));
            services.AddHangfireServer();
            #endregion

            #region App Settings
            services.AddOptions();
            #endregion

            services.ConfigCors();
            services.ConfigJwt(Configuration["Jwt:Key"], Configuration["Jwt:Issuer"], Configuration["Jwt:Issuer"]);
            //services.ConfigSwagger(Environment);
            services.AddBusinessServices();
            services.AddHttpContextAccessor();
            //services.AddAutoMapper(typeof(ShiftMappingProfile));
            services.AddDbContext<CompatriotsClubContext>(option =>
                option.UseSqlServer(Configuration.GetConnectionString("SqlDb"), _ => _.MigrationsAssembly("Data"))
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHangFireService hangFireService)
        {
            app.UseCors("AllowAll");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                Authorization = new List<IDashboardAuthorizationFilter>()
            });
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


            hangFireService.StartBackgroundService();
        }
    }
}
