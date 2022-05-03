using AutoMapper;
using CompatriotsClub.Data;
using CompatriotsClub.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ViewModel.Catalogue;
using ViewModel.System;
using ViewModels.Catalog.Posts;

namespace ViewModel.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile(IConfiguration config)
        {
            CreateMap<decimal, decimal>().ConvertUsing(x => Math.Round(x, 3));
            CreateMap<Contacts, ContactViewModel>().ReverseMap();
            CreateMap<Contacts, ContactResponseViewModel>().ReverseMap();

            CreateMap<Contacts, ContactViewModel>().ReverseMap();
            CreateMap<Contacts, ContactResponseViewModel>().ReverseMap();

            CreateMap<Family, FamilyViewModel>().ReverseMap();
            CreateMap<Family, FamilyResponseViewModel>().ReverseMap();

            CreateMap<Group, GroupViewModel>().ReverseMap();
            CreateMap<Group, GroupResponseViewModel>().ReverseMap();

            CreateMap<Position, PositionViewModel>().ReverseMap();
            CreateMap<Position, PositionResponseViewModel>().ReverseMap();

            CreateMap<Member, MemberViewModel>().ReverseMap();
            CreateMap<Member, MemberResponseViewModel>().ReverseMap();

            CreateMap<Permission, PermissionViewModel>().ReverseMap();
            CreateMap<Permission, PermissionResponseViewModel>().ReverseMap();
            CreateMap<Permission, PermissionModel>().ReverseMap();

            CreateMap<AppUser, UserViewModel>().ReverseMap();
            CreateMap<AppUser, UserResponseViewModel>().ReverseMap();
            CreateMap<AppUser, LoginViewModel>().ReverseMap();

            CreateMap<Post, PostViewModel>().ReverseMap();
            CreateMap<Post, PostResponseViewModel>().ReverseMap();
            CreateMap<Post, PostAddViewModel>().ReverseMap();

            CreateMap<Comment, CommentViewModel>().ReverseMap();
            CreateMap<Comment, CommentAddViewModel>().ReverseMap();

            CreateMap<Image, ImageViewModel>().ReverseMap();

            CreateMap<RoleAddModel, AppRole>();
            CreateMap<RoleUpdateModel, AppRole>();
            CreateMap<AppRole, RoleModel>();
            CreateMap<AppRole, RoleDetailModel>();
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
