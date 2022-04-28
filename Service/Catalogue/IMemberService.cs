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
        public async Task<PagingModel> GetPagedResult(ViewModel.MemberFilter filter)
        {
            var result = new PagingModel()
            {
                TotalCounts = 0,
                Data = new List<MemberResponseViewModel>()
            };

            var query = _sqlDbContext.Members
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
