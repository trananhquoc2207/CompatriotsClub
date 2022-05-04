using CompatriotsClub.Data;
using CompatriotsClub.Entities;
using Service.Base;
using Service.Catalogue;
using Service.Common;
using Service.Core;
using Service.Export;
using Service.System;

namespace Service.DI
{
    public class ApiDIConfig
    {
        public static void AddDependencies(IServiceCollection services)
        {

            #region Common
            services.AddScoped<IHangFireService, HangFireService>();

            services.AddScoped<IFileStorageService, FileStorageService>();
            #endregion

            services.AddScoped<IBaseService<Member>, BaseService<Member>>();
            services.AddScoped<IBaseService<Contacts>, BaseService<Contacts>>();
            services.AddScoped<IBaseService<Family>, BaseService<Family>>();
            services.AddScoped<IBaseService<Group>, BaseService<Group>>();
            services.AddScoped<IBaseService<Position>, BaseService<Position>>();


            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IFamilyService, FamilyService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IPositionService, PositionService>();


            services.AddScoped<IMemberExportService, MemberExportService>();
            #region Authentication
            services.AddScoped<IBaseService<Permission>, BaseService<Permission>>();
            services.AddScoped<IBaseService<AppUser>, BaseService<AppUser>>();

            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
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
