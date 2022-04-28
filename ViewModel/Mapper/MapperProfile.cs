using AutoMapper;
using CompatriotsClub.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ViewModel.Catalogue;

namespace ViewModel.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile(IConfiguration config)
        {
            CreateMap<decimal, decimal>().ConvertUsing(x => Math.Round(x, 3));
            CreateMap<Contact, ContactViewModel>().ReverseMap();
            CreateMap<Contact, ContactResponseViewModel>().ReverseMap();

            CreateMap<Contact, ContactViewModel>().ReverseMap();
            CreateMap<Contact, ContactResponseViewModel>().ReverseMap();

            CreateMap<Family, FamilyViewModel>().ReverseMap();
            CreateMap<Family, FamilyResponseViewModel>().ReverseMap();

            CreateMap<Group, GroupViewModel>().ReverseMap();
            CreateMap<Group, GroupResponseViewModel>().ReverseMap();

            CreateMap<Member, MemberViewModel>().ReverseMap();
            CreateMap<Member, MemberResponseViewModel>().ReverseMap();

            CreateMap<Post, PostViewModel>().ReverseMap();
            CreateMap<Post, PostResponseViewModel>().ReverseMap();
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
