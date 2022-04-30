using AutoMapper;
using CompatriotsClub.Data;
using Microsoft.EntityFrameworkCore;
using Service.common;
using ViewModel;

namespace Service.Catalogue
{
    public interface IPositionService
    {
        Task<PagingModel> GetPagedResult(ViewModel.PositionFilter filter);
    }
    public class PositionService : IPositionService
    {
        private readonly CompatriotsClubContext _sqlDbContext;
        private readonly IMapper _mapper;
        public PositionService(CompatriotsClubContext sqlDbContext, IMapper mapper)
        {
            _sqlDbContext = sqlDbContext;
            _mapper = mapper;
        }
        public async Task<PagingModel> GetPagedResult(ViewModel.PositionFilter filter)
        {
            var result = new PagingModel()
            {
                TotalCounts = 0,
                Data = new List<PositionResponseViewModel>()
            };

            var query = _sqlDbContext.Position
                            .Where(_ => string.IsNullOrEmpty(filter.Keyword) || _.Name.ToLower().Contains(filter.Keyword.ToLower()));
            var users = await query.Skip(filter.PageIndex * filter.PageSize).Take(filter.PageSize).ToListAsync();

            var userModels = new List<PositionResponseViewModel>();
            foreach (var user in users)
            {
                var userModel = _mapper.Map<Position, PositionResponseViewModel>(user);

                userModels.Add(userModel);
            }

            result.TotalCounts = await query.CountAsync();
            result.Data = userModels;

            return result;
        }
    }
}
