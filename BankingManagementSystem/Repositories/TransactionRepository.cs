using BankingManagementSystem.Infrastructure.Data;
using BankingManagementSystem.Infrastructure.Data.Models;

namespace BankingManagementSystem.Repositories
{
    public class TransactionRepository : Repository<Transaction>
    {
        public TransactionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}