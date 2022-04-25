using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ViewModel.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile(IConfiguration config)
        {
            CreateMap<decimal, decimal>().ConvertUsing(x => Math.Round(x, 3));
        }
    }
    public class MapperConfig
    {
        public static void Config(IServiceCollection services, IConfiguration config)
        {
            var mappingConfig = new MapperConfiguration(
                mc =>
                {
                    mc.AddProfile(new MapperProfile(config));
                });

            IMapper mapper = mappingConfig.CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}
