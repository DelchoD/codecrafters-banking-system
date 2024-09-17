using BankingManagementSystem.Infrastructure.Data;
using BankingManagementSystem.Infrastructure.Data.Models;
using BankingManagementSystem.Repositories;

namespace BankingManagementSystem.Core.Repositories
{
    public class AccountRepository : Repository<Account>
    {
        public AccountRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}