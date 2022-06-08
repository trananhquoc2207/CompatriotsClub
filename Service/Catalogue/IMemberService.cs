using AutoMapper;
using CompatriotsClub.Data;
using Microsoft.EntityFrameworkCore;
using Service.common;
using ViewModel;

namespace Service.Catalogue
{
    public interface IMemberService
    {
        Task<PagingModel> GetPagedResult(ViewModel.MemberFilter filter);
        List<MemberResponseViewModel> GetAll();
        Task<ResultModel> Delete(int id);
    }
    public class MemberService : IMemberService
    {
        private readonly CompatriotsClubContext _sqlDbContext;
        private readonly IMapper _mapper;
        public MemberService(CompatriotsClubContext sqlDbContext, IMapper mapper)
        {
            _sqlDbContext = sqlDbContext;
            _mapper = mapper;
        }
        public MemberService()
        {

        }

        public async Task<ResultModel> Delete(int id)
        {
            var result = new ResultModel();
            var member = await _sqlDbContext.Members.FindAsync(id);
            if (member == null)
            {
                result.ErrorMessages = "Không tìm thấy hội viên";
                return result;
            }
            member.IsDelete = true;
            _sqlDbContext.Members.Update(member);
            var ss = await _sqlDbContext.SaveChangesAsync();
            if (ss > 0)
            {
                result.Data = member;
                result.Succeed = true;
                return result;
            }
            return result;
        }

        public List<MemberResponseViewModel> GetAll()
        {
            var a = _sqlDbContext.Members.Where(x => x.IsDelete == false).ToList();
            var res = _mapper.Map<List<MemberResponseViewModel>>(a);
            return res;
        }

        public async Task<PagingModel> GetPagedResult(ViewModel.MemberFilter filter)
        {
            var result = new PagingModel()
            {
                TotalCounts = 0,
                Data = new List<MemberResponseViewModel>()
            };

            var query = _sqlDbContext.Members.Where(_ => _.IsDelete == filter.IsLeft)
                            .Where(_ => string.IsNullOrEmpty(filter.Keyword) || _.Name.ToLower().Contains(filter.Keyword.ToLower()));

            var users = await query.Skip(filter.PageIndex * filter.PageSize).Take(filter.PageSize).ToListAsync();

            var userModels = new List<MemberResponseViewModel>();
            foreach (var user in users)
            {
                var userModel = _mapper.Map<Member, MemberResponseViewModel>(user);

                userModels.Add(userModel);
            }

            result.TotalCounts = await query.CountAsync();
            result.Data = userModels;

            return result;
        }


    }
}
