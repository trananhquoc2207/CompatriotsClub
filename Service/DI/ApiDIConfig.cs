using Microsoft.Extensions.DependencyInjection;
using Service.Core;

namespace Service.DI
{
    public class ApiDIConfig
    {
        public static void AddDependencies(IServiceCollection services)
        {

            services.AddScoped<IHangFireService, HangFireService>();
        }
    }
}
