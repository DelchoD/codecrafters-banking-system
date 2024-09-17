using BankingManagementSystem.Infrastructure.Data;
using BankingManagementSystem.Infrastructure.Data.Models;

namespace BankingManagementSystem.Repositories
{
    public class AccountRepository : Repository<Account>
    {
        public AccountRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Account> FirstOrDefaultAsync(Func<object, bool> func)
        {
            throw new NotImplementedException();
        }
    }
}