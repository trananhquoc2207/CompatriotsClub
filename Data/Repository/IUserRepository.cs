using CompatriotsClub.Data;
using Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IUserRepository : IBaseRepository<AppUser>
    {
    }

    public class UserRepository : BaseRepository<AppUser>, IUserRepository
    {
        public UserRepository(CompatriotsClubContext dbContext) : base(dbContext)
        {
        }
    }

}
