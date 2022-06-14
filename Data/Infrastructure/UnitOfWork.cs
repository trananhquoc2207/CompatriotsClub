using CompatriotsClub.Data;
using Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Infrastructure
{
#nullable disable
    public  interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompatriotsClubContext _dbContext;

        public UnitOfWork(CompatriotsClubContext dbContext) => _dbContext = dbContext;

        private IUserRepository _userRepository;

        public IUserRepository Users
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_dbContext);

                return _userRepository;
            }
        }


        public CompatriotsClubContext DbContext => _dbContext;
        

        public int SaveChanges()
        {
            try
            {
                var result = DbContext.SaveChanges();
                return result;
            }
            catch (DbUpdateException dbEx)
            {
               throw HandleDbUpdateEx(dbEx);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Exception HandleDbUpdateEx(DbUpdateException dbEx)
        {
            if (dbEx.InnerException != null)
            {
                // dbEx.InnerException.InnerException is SqlException
                if (dbEx.InnerException.InnerException != null)
                    throw new Exception(dbEx.InnerException.InnerException.Message,
                                        dbEx.InnerException.InnerException);
                else throw new Exception(dbEx.InnerException.Message, dbEx.InnerException);
            }
            else throw new Exception(GetDbUpdateErrMsgs(dbEx), dbEx);
        }
        private string GetDbUpdateErrMsgs(DbUpdateException dbEx)
        {
            return dbEx.Message;
        }
        public async Task<int> SaveChangesAsync()
        {
            try
            {
                var result = await DbContext.SaveChangesAsync();
                return result;
            }
            catch (DbUpdateException dbEx)
            {
                throw HandleDbUpdateEx(dbEx);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
