namespace BankingManagementSystem.Core.Services
{
    using Contracts;
    using Infrastructure.Data;
    public class CustomerService : ICustomerService 
    {
        private readonly ApplicationDbContext context;

        public CustomerService(ApplicationDbContext _context)
        {
           context = _context;
        }
    }
}
