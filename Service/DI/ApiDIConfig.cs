using Microsoft.Extensions.DependencyInjection;
using Service.Core;

namespace Service.DI
{
    public class ApiDIConfig
    {
        public static void AddDependencies(IServiceCollection services)
        {
            // Provide dependencies for api layer, here
            // services.AddScoped<INhanVienService, NhanVienService>();
            services.AddScoped<IHangFireService, HangFireService>();
        }
    }
}
