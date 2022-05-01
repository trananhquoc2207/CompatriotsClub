using AutoMapper;
using CompatriotsClub.Data;
using Microsoft.EntityFrameworkCore;
using Service.common;
using ViewModel.Catalogue;

namespace Service.Catalogue
{
#nullable disable
    public interface IContactService
    {
        Task<ResultModel> AddMember(int id, ContactMembersRequest request);
        Task<ResultModel> RemoveMember(int id, int memberId);
        Task<PagingModel> GetPagedResult(ContactFilter request);
    }
    public class ContactService : IContactService
    {
        private readonly IMapper _mapper;
        private readonly CompatriotsClubContext _sqlDbContext;



        public ContactService(IMapper mapper, CompatriotsClubContext sqlDbContext)
        {
            _mapper = mapper;
            _sqlDbContext = sqlDbContext;
        }
        public async Task<ResultModel> AddMember(int id, ContactMembersRequest request)
        {

            var result = new ResultModel();
            try
            {
                var permission = await _sqlDbContext.Contacts.Where(_ => _.Id == id).FirstOrDefaultAsync();
                if (permission == null)
                {
                    result.ErrorMessages = "Not found permission";
                    return result;
                }
                var members = await _sqlDbContext.Members.Where(_ => _.Id == request.MemberId).FirstOrDefaultAsync();
                if (members == null)
                {
                    result.ErrorMessages = "Not found member";
                    return result;
                }
                var contactMember = new ContactMembers()
                {
                    ContactId = id,
                    MemberId = request.MemberId,
                    RoleId = request.PositionId,
                };
                _sqlDbContext.ContactMembers.AddRange(contactMember);
                await _sqlDbContext.SaveChangesAsync();

                result.Succeed = true;
                result.Data = contactMember;
            }
            catch (Exception e)
            {
                result.ErrorMessages = e.Message + "\n" + (e.InnerException != null ? e.InnerException.Message : "") + "\n ***Trace*** \n" + e.StackTrace;
            }

            return result;
        }

        public async Task<PagingModel> GetPagedResult(ContactFilter filter)
        {
            var result = new PagingModel()
            {
                TotalCounts = 0,
                Data = new List<ContactResponseViewModel>()
            };

            var query = _sqlDbContext.Contacts
                            .Where(_ => string.IsNullOrEmpty(filter.Keyword) || _.Name.ToLower().Contains(filter.Keyword.ToLower()));
            var users = await query.Skip(filter.PageIndex * filter.PageSize).Take(filter.PageSize).ToListAsync();

            var contactModel = _mapper.Map<List<Contacts>, List<ContactResponseViewModel>>(users);

            result.TotalCounts = await query.CountAsync();
            result.Data = contactModel;

            return result;
        }

        public async Task<ResultModel> RemoveMember(int id, int memberId)
        {

            var result = new ResultModel();
            try
            {
                var permission = await _sqlDbContext.Contacts.Where(_ => _.Id == id).FirstOrDefaultAsync();
                if (permission == null)
                {
                    result.ErrorMessages = "Not found permission";
                    return result;
                }
                var contactMember = await _sqlDbContext.ContactMembers.Where(_ => _.ContactId == id && _.MemberId == memberId).FirstOrDefaultAsync();
                _sqlDbContext.ContactMembers.Remove(contactMember);
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
