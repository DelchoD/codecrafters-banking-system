using BankingManagementSystem.Infrastructure.Data;
using BankingManagementSystem.Infrastructure.Data.Models;

namespace BankingManagementSystem.Core.Repositories
{
    public class CustomerRepository : Repository<Customer>
    {
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}