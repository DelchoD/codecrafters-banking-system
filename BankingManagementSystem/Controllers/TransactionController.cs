namespace BankingManagementSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Core.Services.Contracts;
    using BankingManagementSystem.Core.Models.Transaction;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // POST: api/transaction
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionCreateDTO transactionCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdTransactionDTO = await _transactionService.ProcessTransaction(transactionCreateDTO);
            return CreatedAtAction(nameof(GetTransaction), new { id = createdTransactionDTO.Id }, createdTransactionDTO);
        }

        // GET: api/transaction/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDetailsDTO>> GetTransaction(int id)
        {
            var transactionDTO = await _transactionService.GetTransactionById(id);
            if (transactionDTO == null)
            {
                return NotFound();
            }
            return Ok(transactionDTO);
        }

        // GET: api/transaction/by-account/{accountId}
        [HttpGet("by-account/{accountId}")]
        public async Task<ActionResult<List<TransactionDetailsDTO>>> GetTransactionsByAccountId(int accountId)
        {
            var transactions = await _transactionService.GetTransactionsByAccountId(accountId);

            if (transactions == null || !transactions.Any())
            {
                return NoContent();
            }

            return Ok(transactions);
        }

        // GET: api/transaction
        [HttpGet]
        public async Task<ActionResult<List<TransactionAllDTO>>> GetAllTransactions()
        {
            var transactionDTOs = await _transactionService.GetAllTransactionsAsync();

            if (transactionDTOs == null || !transactionDTOs.Any())
            {
                return NoContent(); 
            }

            return Ok(transactionDTOs);
        }

        // GET: api/transaction/by-date
        [HttpGet("by-date")]
        public async Task<ActionResult<List<TransactionDetailsDTO>>> GetTransactionsByDate(DateTime startDate, DateTime endDate)
        {
            var transactions = await _transactionService.GetTransactionsByDate(startDate, endDate);
            if (transactions == null || !transactions.Any())
            {
                return NoContent();
            }

            return Ok(transactions);
        }

        // GET: api/transaction/by-amount
        [HttpGet("by-amount")]
        public async Task<ActionResult<List<TransactionDetailsDTO>>> GetTransactionsByAmount(decimal minAmount, decimal maxAmount)
        {
            var transactions = await _transactionService.GetTransactionsByAmount(minAmount, maxAmount);
            if (transactions == null || !transactions.Any())
            {
                return NoContent();
            }

            return Ok(transactions);
        }

        // GET: api/transaction/outgoing/{accountId}
        [HttpGet("outgoing/{accountId}")]
        public async Task<ActionResult<List<TransactionDetailsDTO>>> GetOutgoingTransactions(int accountId)
        {
            var transactions = await _transactionService.GetOutgoingTransactions(accountId);
            if (transactions == null || !transactions.Any())
            {
                return NoContent();
            }

            return Ok(transactions);
        }

        // GET: api/transaction/incoming/{accountId}
        [HttpGet("incoming/{accountId}")]
        public async Task<ActionResult<List<TransactionDetailsDTO>>> GetIncomingTransactions(int accountId)
        {
            var transactions = await _transactionService.GetIncomingTransactions(accountId);
            if (transactions == null || !transactions.Any())
            {
                return NoContent();
            }

            return Ok(transactions);
        }

        // DELETE: api/transaction/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            bool isCancelled = await _transactionService.CancelTransaction(id);

            if (!isCancelled)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
