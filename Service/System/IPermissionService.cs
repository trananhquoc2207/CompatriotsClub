using AutoMapper;
using CompatriotsClub.Data;
using Microsoft.EntityFrameworkCore;
using Service.common;
using ViewModel.System;

namespace Service.System
{
    public interface IPermissionService
    {
        Task<PagingModel> GetPagedResult(int pageIndex, int pageSize);

        Task<ResultModel> AddUser(AddPermissionToUserModel model, Guid permissionId);

        Task<ResultModel> RemoveUser(RemovePermissionOfUserModel model, Guid permissionId);
    }
    public class PermissionService : IPermissionService
    {
        private readonly IMapper _mapper;
        private readonly CompatriotsClubContext _sqlDbContext;

        public PermissionService(IMapper mapper, CompatriotsClubContext sqlDbContext)
        {
            _mapper = mapper;
            _sqlDbContext = sqlDbContext;
        }
        public async Task<PagingModel> GetPagedResult(int pageIndex, int pageSize)
        {
            var result = new PagingModel()
            {
                TotalCounts = 0,
                Data = new List<PermissionModel>(),
            };

            var query = _sqlDbContext.Permissions;
            var permissions = await query.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync();
            var permissionModel = _mapper.Map<List<Permission>, List<PermissionModel>>(permissions);

            result.TotalCounts = await query.CountAsync();
            result.Data = permissionModel;

            return result;
        }

        public async Task<ResultModel> AddUser(AddPermissionToUserModel model, Guid permissionId)
        {
            var result = new ResultModel();
            try
            {
                var permission = await _sqlDbContext.Permissions.Where(_ => _.Id == permissionId).FirstOrDefaultAsync();
                if (permission == null)
                {
                    result.ErrorMessages = "Not found permission";
                    return result;
                }

                var userPermissions = new List<UserPermission>();
                foreach (var userId in model.Ids)
                {
                    var userPermission = new UserPermission()
                    {
                        PermissionId = permissionId,
                        UserId = userId,
                    };

                    userPermissions.Add(userPermission);
                }

                _sqlDbContext.UserPermissions.AddRange(userPermissions);
                await _sqlDbContext.SaveChangesAsync();

                result.Succeed = true;
                result.Data = permission.Id;
            }
            catch (Exception e)
            {
                result.ErrorMessages = e.Message + "\n" + (e.InnerException != null ? e.InnerException.Message : "") + "\n ***Trace*** \n" + e.StackTrace;
            }

            return result;
        }

        public async Task<ResultModel> RemoveUser(RemovePermissionOfUserModel model, Guid permissionId)
        {
            var result = new ResultModel();
            try
            {
                var permission = await _sqlDbContext.Permissions.Where(_ => _.Id == permissionId).FirstOrDefaultAsync();
                if (permission == null)
                {
                    result.ErrorMessages = "Not found permission";
                    return result;
                }

                foreach (var userId in model.Ids)
                {
                    var permissionOfUser = await _sqlDbContext.UserPermissions.Where(_ => _.PermissionId == permissionId && _.UserId == userId).ToListAsync();
                    _sqlDbContext.UserPermissions.RemoveRange(permissionOfUser);
                }

                await _sqlDbContext.SaveChangesAsync();

                result.Succeed = true;
                result.Data = permission.Id;
            }
            catch (Exception e)
            {
                result.ErrorMessages = e.Message + "\n" + (e.InnerException != null ? e.InnerException.Message : "") + "\n ***Trace*** \n" + e.StackTrace;
            }

            return result;
        }
    }
}
