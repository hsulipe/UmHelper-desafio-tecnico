using Domain.Models.Entities;
using Infrastructure.Databases.Postgres;
using Infrastructure.Repositories.Base.Impl;

namespace Infrastructure.Repositories.Users.Impl
{
    public class UserAccountRepository : RepositoryBase<UserAccount>, IUserAccountRepository
    {
        private readonly FinanceDbContext _context;

        public UserAccountRepository(FinanceDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
