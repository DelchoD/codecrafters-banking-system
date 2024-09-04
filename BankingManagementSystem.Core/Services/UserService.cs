namespace BankingManagementSystem.Core.Services
{
    using Contracts;
    using Infrastructure.Data;
    public class UserService : IUserService 
    {
        private readonly ApplicationDbContext context;

        public UserService(ApplicationDbContext _context)
        {
           context = _context;
        }
    }
}
