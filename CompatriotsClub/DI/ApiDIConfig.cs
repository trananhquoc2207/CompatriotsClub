using CompatriotsClub.Data;
using Service.Base;
using Service.Catalogue;
using Service.Core;
using Service.System;

namespace Service.DI
{
    public class ApiDIConfig
    {
        public static void AddDependencies(IServiceCollection services)
        {
            services.AddScoped<IHangFireService, HangFireService>();
            services.AddScoped<IBaseService<Member>, BaseService<Member>>();
            services.AddScoped<IBaseService<Contact>, BaseService<Contact>>();
            services.AddScoped<IBaseService<Family>, BaseService<Family>>();
            services.AddScoped<IBaseService<Group>, BaseService<Group>>();
            services.AddScoped<IBaseService<Post>, BaseService<Post>>();
            services.AddScoped<IBaseService<Permission>, BaseService<Permission>>();

            services.AddScoped<IBaseService<AppUser>, BaseService<AppUser>>();


            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IFamilyService, FamilyService>();
            services.AddScoped<IFundService, FundService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IPostService, PostService>();

            #region Authentication
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IUserService, UserService>();
            #endregion
        }
    }
}
