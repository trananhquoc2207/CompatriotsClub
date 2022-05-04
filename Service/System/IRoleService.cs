using AutoMapper;
using CompatriotsClub.Data;
using Microsoft.EntityFrameworkCore;
using Service.common;
using ViewModel.System;

namespace Service.System
{
    public interface IRoleService
    {
        Task<ResultModel> Create(RoleAddModel model);

        Task<ResultModel> Update(RoleUpdateModel model, Guid roleId);

        Task<ResultModel> Delete(Guid roleId);

        Task<ResultModel> Get(Guid roleId);

        Task<PagingModel> GetPermission(Guid roleId);

        Task<PagingModel> GetPagedResult(int pageIndex, int pageSize);

        Task<ResultModel> AddUser(AddUserToRoleModel model, Guid roleId);

        Task<ResultModel> RemoveUser(RemoveUserOfRoleModel model, Guid roleId);

        Task<ResultModel> AddPermission(AddPermissionToRoleModel model, Guid roleId);

        Task<ResultModel> RemovePermission(RemovePermissionOfRoleModel model, Guid roleId);
    }

    public class RoleService : IRoleService
    {
        private readonly IMapper _mapper;
        private readonly CompatriotsClubContext _sqlDbContext;

        public RoleService(IMapper mapper, CompatriotsClubContext sqlDbContext)
        {
            _mapper = mapper;
            _sqlDbContext = sqlDbContext;
        }

        public async Task<ResultModel> Create(RoleAddModel model)
        {
            var result = new ResultModel();
            try
            {
                var keyword = model.Name.ToLower();
                var found = await _sqlDbContext.Roles.Where(_ => _.Name.ToLower() == keyword).FirstOrDefaultAsync();
                if (found != null)
                {
                    result.ErrorMessages = "Dupliated code";
                    return result;
                }

                var role = _mapper.Map<RoleAddModel, AppRole>(model);
                _sqlDbContext.Roles.Add(role);
                await _sqlDbContext.SaveChangesAsync();

                result.Succeed = true;
                result.Data = role.Id;
            }
            catch (Exception e)
            {
                result.ErrorMessages = e.Message + "\n" + (e.InnerException != null ? e.InnerException.Message : "") + "\n ***Trace*** \n" + e.StackTrace;
            }

            return result;
        }

        public async Task<ResultModel> Update(RoleUpdateModel model, Guid roleId)
        {
            var result = new ResultModel();
            try
            {
                var role = await _sqlDbContext.Roles.Where(_ => _.Id == roleId).FirstOrDefaultAsync();
                if (role == null)
                {
                    result.ErrorMessages = "Not found";
                    return result;
                }

                _mapper.Map<RoleUpdateModel, AppRole>(model, role);
                _sqlDbContext.Roles.Update(role);
                await _sqlDbContext.SaveChangesAsync();

                result.Succeed = true;
                result.Data = role.Id;
            }
            catch (Exception e)
            {
                result.ErrorMessages = e.Message + "\n" + (e.InnerException != null ? e.InnerException.Message : "") + "\n ***Trace*** \n" + e.StackTrace;
            }

            return result;
        }

        public async Task<ResultModel> Delete(Guid roleId)
        {
            var result = new ResultModel();
            try
            {
                var role = await _sqlDbContext.Roles.Where(_ => _.Id == roleId).FirstOrDefaultAsync();
                if (role == null)
                {
                    result.ErrorMessages = "Not found";
                    return result;
                }

                if (_sqlDbContext.Entry(role).State == EntityState.Detached)
                {
                    _sqlDbContext.Roles.Attach(role);
                }

                _sqlDbContext.Roles.Remove(role);
                await _sqlDbContext.SaveChangesAsync();

                result.Succeed = true;
                result.Data = roleId;
            }
            catch (Exception e)
            {
                result.ErrorMessages = e.Message + "\n" + (e.InnerException != null ? e.InnerException.Message : "") + "\n ***Trace*** \n" + e.StackTrace;
            }

            return result;
        }
#nullable disable
        public async Task<ResultModel> Get(Guid roleId)
        {
            var result = new ResultModel()
            {
                Succeed = true
            };
            try
            {
                var role = await _sqlDbContext.Roles.FirstOrDefaultAsync(_ => _.Id == roleId);
                if (role == null)
                {
                    return result;
                }
                var userRoles = await _sqlDbContext.UserRoles.Where(x => x.RoleId == role.Id).ToListAsync();
                var roleModel = _mapper.Map<AppRole, RoleDetailModel>(role);
                roleModel.Users = new List<MinifyUserModel>();

                foreach (var userRole in userRoles)
                {
                    var user = await _sqlDbContext.Users.FirstOrDefaultAsync(_ => _.Id == userRole.UserId);
                    var userModel = new MinifyUserModel()
                    {
                        Id = user.Id,
                        Username = user.UserName,
                    };

                    roleModel.Users.Add(userModel);
                }

                result.Data = roleModel;
            }
            catch (Exception e)
            {
                result.Succeed = false;
                result.ErrorMessages = e.Message + "\n" + (e.InnerException != null ? e.InnerException.Message : "") + "\n ***Trace*** \n" + e.StackTrace;
            }

            return result;
        }

        public async Task<PagingModel> GetPermission(Guid roleId)
        {
            var result = new PagingModel()
            {
                TotalCounts = 0,
                Data = new List<Permission>()
            };

            var role = await _sqlDbContext.Roles.Where(_ => _.Id == roleId).FirstOrDefaultAsync();
            if (role == null)
            {
                return result;
            }

            var permissions = await _sqlDbContext.Permissions.ToListAsync();
            var permissionOfRole = await _sqlDbContext.RolePermissions
                                            .Where(_ => _.RoleId == roleId)
                                            .Select(_ => _.PermissionId)
                                            .ToListAsync();

            foreach (var permissionId in permissionOfRole)
            {
                var permission = permissions.FirstOrDefault(_ => _.Id == permissionId);
                if (permission != null && !result.Data.Contains(permission))
                {
                    result.Data.Add(permission);
                }
            }

            return result;
        }

        public async Task<PagingModel> GetPagedResult(int pageIndex, int pageSize)
        {
            var result = new PagingModel()
            {
                TotalCounts = 0,
                Data = new List<RoleModel>(),
            };

            var query = _sqlDbContext.Roles;
            var roles = await query.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            var roleModel = _mapper.Map<List<AppRole>, List<RoleModel>>(roles);

            result.TotalCounts = await query.CountAsync();
            result.Data = roleModel;

            return result;
        }

        public async Task<ResultModel> AddUser(AddUserToRoleModel model, Guid roleId)
        {
            var result = new ResultModel();
            try
            {
                var role = await _sqlDbContext.Roles.Where(_ => _.Id == roleId).FirstOrDefaultAsync();
                if (role == null)
                {
                    result.ErrorMessages = "Not found Role";
                    return result;
                }

                var userRoles = new List<AppUserRoles>();
                foreach (var userId in model.Ids)
                {
                    var userRole = await _sqlDbContext.AppUserRoles.Where(_ => _.UserId == userId).FirstOrDefaultAsync();
                    if (userRole == null)
                    {
                        userRole = new AppUserRoles()
                        {
                            UserId = userId,
                            RoleId = role.Id,
                        };

                        _sqlDbContext.UserRoles.Add(userRole);
                    }
                    else
                    {
                        //var resultOfCompare = AuthUtilities.CompareLevelOfRole(role, userRole.Role);
                        //if (resultOfCompare != null && resultOfCompare.Id != userRole.Role.Id)
                        //{
                        //    userRole.RoleId = resultOfCompare.Id;
                        //    _sqlDbContext.UserRoles.Update(userRole);
                        //}
                    }

                    await _sqlDbContext.SaveChangesAsync();

                    userRoles.Add(userRole);
                }

                await _sqlDbContext.SaveChangesAsync();

                result.Succeed = true;
                result.Data = role.Id;
            }
            catch (Exception e)
            {
                result.ErrorMessages = e.Message + "\n" + (e.InnerException != null ? e.InnerException.Message : "") + "\n ***Trace*** \n" + e.StackTrace;
            }

            return result;
        }

        public async Task<ResultModel> RemoveUser(RemoveUserOfRoleModel model, Guid roleId)
        {
            var result = new ResultModel();
            try
            {
                var role = await _sqlDbContext.Roles.Where(_ => _.Id == roleId).FirstOrDefaultAsync();
                if (role == null)
                {
                    result.ErrorMessages = "Not found";
                    return result;
                }

                foreach (var userId in model.Ids)
                {
                    var roleOfUser = await _sqlDbContext.UserRoles.Where(_ => _.RoleId == roleId && _.UserId == userId).ToListAsync();
                    _sqlDbContext.UserRoles.RemoveRange(roleOfUser);
                }

                await _sqlDbContext.SaveChangesAsync();

                result.Succeed = true;
                result.Data = role.Id;
            }
            catch (Exception e)
            {
                result.ErrorMessages = e.Message + "\n" + (e.InnerException != null ? e.InnerException.Message : "") + "\n ***Trace*** \n" + e.StackTrace;
            }

            return result;
        }

        public async Task<ResultModel> AddPermission(AddPermissionToRoleModel model, Guid roleId)
        {
            var result = new ResultModel();
            try
            {
                var Role = await _sqlDbContext.Roles.Where(_ => _.Id == roleId).FirstOrDefaultAsync();
                if (Role == null)
                {
                    result.ErrorMessages = "Not found Role";
                    return result;
                }

                var rolePermissions = new List<RolePermission>();
                foreach (var permissionId in model.Ids)
                {
                    var rolePermission = new RolePermission()
                    {
                        RoleId = roleId,
                        PermissionId = permissionId,
                    };

                    rolePermissions.Add(rolePermission);
                }

                _sqlDbContext.RolePermissions.AddRange(rolePermissions);
                await _sqlDbContext.SaveChangesAsync();

                result.Succeed = true;
                result.Data = Role.Id;
            }
            catch (Exception e)
            {
                result.ErrorMessages = e.Message + "\n" + (e.InnerException != null ? e.InnerException.Message : "") + "\n ***Trace*** \n" + e.StackTrace;
            }

            return result;
        }

        public async Task<ResultModel> RemovePermission(RemovePermissionOfRoleModel model, Guid roleId)
        {
            var result = new ResultModel();
            try
            {
                var role = await _sqlDbContext.Roles.Where(_ => _.Id == roleId).FirstOrDefaultAsync();
                if (role == null)
                {
                    result.ErrorMessages = "Not found Role";
                    return result;
                }

                foreach (var permissionId in model.Ids)
                {
                    var permissionOfRole = await _sqlDbContext.RolePermissions.Where(_ => _.PermissionId == permissionId && _.RoleId == roleId).ToListAsync();
                    _sqlDbContext.RolePermissions.RemoveRange(permissionOfRole);
                }

                await _sqlDbContext.SaveChangesAsync();

                result.Succeed = true;
                result.Data = role.Id;
            }
            catch (Exception e)
            {
                result.ErrorMessages = e.Message + "\n" + (e.InnerException != null ? e.InnerException.Message : "") + "\n ***Trace*** \n" + e.StackTrace;
            }

            return result;
        }
    }
}
