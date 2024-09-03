
namespace BankingManagementSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Core.Services.Contracts;


    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService transactionService;

        public TransactionController(ITransactionService _transactionService)
        {
            transactionService = _transactionService;
        }
    }
}
