using CompatriotsClub.Data;
using CompatriotsClub.Entities;
using Service.Base;
using Service.Catalogue;
using Service.Common;
using Service.Core;
using Service.System;

namespace Service.DI
{
    public class ApiDIConfig
    {
        public static void AddDependencies(IServiceCollection services)
        {

            #region
            services.AddScoped<IFileStorageService, FileStorageService>();
            #endregion

            services.AddScoped<IHangFireService, HangFireService>();
            services.AddScoped<IBaseService<Member>, BaseService<Member>>();
            services.AddScoped<IBaseService<Contact>, BaseService<Contact>>();
            services.AddScoped<IBaseService<Family>, BaseService<Family>>();
            services.AddScoped<IBaseService<Group>, BaseService<Group>>();
            services.AddScoped<IBaseService<Permission>, BaseService<Permission>>();

            services.AddScoped<IBaseService<AppUser>, BaseService<AppUser>>();


            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IFamilyService, FamilyService>();
            services.AddScoped<IGroupService, GroupService>();

            #region Authentication
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IUserService, UserService>();
            #endregion

            #region social network
            services.AddScoped<IBaseService<Post>, BaseService<Post>>();
            services.AddScoped<IBaseService<Comment>, BaseService<Comment>>();
            services.AddScoped<IBaseService<Image>, BaseService<Image>>();

            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IImageService, ImageService>();


            #endregion
        }
    }
}
