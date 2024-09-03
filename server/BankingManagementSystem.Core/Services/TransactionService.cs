namespace BankingManagementSystem.Core.Services
{
    using Contracts;
    using Infrastructure.Data;

    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext context;

        public TransactionService(ApplicationDbContext _context)
        {
           context = _context;   
        }


        public async Task GetAllTransactionsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
