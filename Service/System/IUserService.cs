using AutoMapper;
using CompatriotsClub.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.common;
using Service.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ViewModel.System;

namespace Service.Catalogue
{
    public interface IUserService
    {
        Task<ResultModel> Login(UserLoginRequest model);
        Task<ResultModel> Register(RegisterRequest model);

        Task<PagingModel> GetPagedResult(UserFilter filter);
        Task<ResultModel> GetPermissionCodeOfUser(Guid userId);

        Task<ResultModel> GetPermissionDetailOfUser(Guid userId);
    }
    public class UserService : IUserService
    {
        private readonly CompatriotsClubContext _sqlDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public UserService(CompatriotsClubContext sqlDbContext,
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager,
            SignInManager<AppUser> signInManager,
            IMapper mapper,
            IConfiguration config)
        {
            _sqlDbContext = sqlDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _config = config;
            _mapper = mapper;
        }

        public async Task<PagingModel> GetPagedResult(UserFilter filter)
        {
            var result = new PagingModel()
            {
                TotalCounts = 0,
                Data = new List<UserResponseViewModel>()
            };

            var query = _sqlDbContext.Users
                            .Where(_ => string.IsNullOrEmpty(filter.Keyword) || _.UserName.ToLower().Contains(filter.Keyword.ToLower()));
            var users = await query.Skip(filter.PageIndex * filter.PageSize).Take(filter.PageSize).ToListAsync();

            var userModels = new List<UserResponseViewModel>();
            foreach (var user in users)
            {
                var userModel = _mapper.Map<AppUser, UserResponseViewModel>(user);

                userModels.Add(userModel);
            }

            result.TotalCounts = await query.CountAsync();
            result.Data = userModels;

            return result;
        }

        public async Task<ResultModel> GetPermissionCodeOfUser(Guid userId)
        {
            var result = new ResultModel();
            var permissions = await GetPermissionOfUser(userId);
            var permissionCodes = permissions.Select(_ => _.Code).ToList();

            result.Succeed = true;
            result.Data = permissionCodes;
            return result;
        }

        public async Task<ResultModel> GetPermissionDetailOfUser(Guid userId)
        {
            var result = new ResultModel();
            var permissions = await GetPermissionOfUser(userId);

            result.Succeed = true;
            result.Data = permissions;
            return result;
        }


        public async Task<List<PermissionModel>> GetPermissionOfUser(Guid userId)
        {
            var result = new List<PermissionModel>();
            var permissions = await _sqlDbContext.Permissions.AsQueryable().ToListAsync();
            var permissionOfUser = await _sqlDbContext.UserPermissions
                                            .Where(_ => _.UserId == userId)
                                            .Select(_ => _.PermissionId)
                                            .ToListAsync();

            foreach (var permissionId in permissionOfUser)
            {
                var permission = permissions.FirstOrDefault(_ => _.Id == permissionId);
                if (permission != null)
                {
                    var permissionModel = _mapper.Map<Permission, PermissionModel>(permission);
                    if (!result.Any(_ => _.Id == permissionModel.Id))
                        result.Add(permissionModel);
                }
            }

            var roleOfUser = await _sqlDbContext.UserRoles
                                        .Where(_ => _.UserId == userId)
                                        .Select(_ => _.RoleId)
                                        .ToListAsync();

            var permissionOfRole = await _sqlDbContext.RolePermissions
                                            .Where(_ => roleOfUser.Contains(_.RoleId))
                                            .Select(_ => _.PermissionId)
                                            .ToListAsync();

            foreach (var permissionId in permissionOfRole)
            {
                var permission = permissions.FirstOrDefault(_ => _.Id == permissionId);
                if (permission != null)
                {
                    var permissionModel = _mapper.Map<Permission, PermissionModel>(permission);
                    if (!result.Any(_ => _.Id == permissionModel.Id))
                        result.Add(permissionModel);
                }
            }

            return result;
        }


        public async Task<ResultModel> Login(UserLoginRequest request)
        {
            var result = new ResultModel();
            try
            {
                var user = await _userManager.FindByNameAsync(request.Username);
                if (user == null)
                {
                    throw new Exception($"Tài khoản {request.Username} không tồn tại");
                }
                var userl = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
                if (!userl.Succeeded)
                {
                    throw new Exception($"Mật khẩu {request.Username} {request.Password} sai");
                }
                var login = await GenerateToken(user);
                result.Succeed = true;
                result.Data = login;
                return result;
            }
            catch (Exception e)
            {
                result.ErrorMessages = e.Message + "\n" + (e.InnerException != null ? e.InnerException.Message : "") + "\n ***Trace*** \n" + e.StackTrace;
            }

            return result;
        }

        public async Task<ResultModel> Register(RegisterRequest request)
        {
            var result = new ResultModel();
            try
            {
                var user = await _userManager.FindByNameAsync(request.UserName);
                if (user != null)
                {
                    result.ErrorMessages = "tai khoản đã tồn tại";
                    return result;
                }

                user = new AppUser()
                {
                    UserName = request.UserName,
                    Email = "asdsadsa",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    PhoneNumber = "asdsa",
                    Avatar = "avatar1.png",
                };
                var cteateUser = await _userManager.CreateAsync(user, request.Password);
                result.Data = cteateUser;
                result.Succeed = true;
                return result;
            }
            catch (Exception e)
            {
                result.ErrorMessages = e.Message + "\n" + (e.InnerException != null ? e.InnerException.Message : "") + "\n ***Trace*** \n" + e.StackTrace;
            }
            return result;
        }

        private async Task<LoginViewModel> GenerateToken(AppUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                //new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.UserName),
                new Claim(ClaimTypes.Role, string.Join(";",roles)),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimConstants.USER_ID, user.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(3);
            var token = new JwtSecurityToken(
                _config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new LoginViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                Username = user.UserName,
                DisplayName = user.UserName,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                RoleName = roles != null && roles.Count > 0 ? roles[0] : null,
            };
        }
    }
}
