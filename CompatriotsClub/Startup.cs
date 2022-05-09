using CompatriotsClub.Application.Extensions;
using CompatriotsClub.Data;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Service.Common;
using Service.Core;
using Service.DI;
using System.Text.Json;
using System.Text.Json.Serialization;
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
            services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<CompatriotsClubContext>()
                .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            });

            services.AddMvc()
            .AddSessionStateTempDataProvider();
            services.AddSession();

            services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);
            #endregion

            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true
            };

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

            services.AddCors(
               options =>
               {
                   options.AddPolicy(
                       "PolicyAll",
                       builder => builder
                       //.WithOrigins("http://172.19.192.1:9992")
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .SetIsOriginAllowed(host => true) // SignalR need it
                                                         //.WithOrigins("*")
                       .AllowCredentials()
                       .Build());
               });

            //  services.ConfigCors();
            //services.ConfigJwt(Configuration["Jwt:Key"], Configuration["Jwt:Issuer"], Configuration["Jwt:Issuer"]);
            //services.ConfigSwagger(Environment);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CompatriotsClub",
                    Version = "2",
                    Description = $"{DateTime.Now.ToUniversalTime().AddHours(7)}"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,
                        },
                        new List<string>()
                      }
                    });
                // Set the comments path for the Swagger JSON and UI.
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //c.IncludeXmlComments(xmlPath);
            });

            string issuer = Configuration.GetValue<string>("Tokens:Issuer");
            string signingKey = Configuration.GetValue<string>("Tokens:Key");
            byte[] signingKeyBytes = System.Text.Encoding.UTF8.GetBytes(signingKey);

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = issuer,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = System.TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
                };
            });

            services.AddBusinessServices();
            services.AddHttpContextAccessor();
            //services.AddAutoMapper(typeof(ShiftMappingProfile));
            services.AddDbContext<CompatriotsClubContext>(option =>
                option.UseSqlServer(Configuration.GetConnectionString("SqlDb"), _ => _.MigrationsAssembly("Data"))
            );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHangFireService hangFireService)
        {
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
            app.UseCors("PolicyAll");
            app.UseHttpContext();

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
