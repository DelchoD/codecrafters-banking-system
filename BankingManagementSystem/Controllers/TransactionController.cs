using BankingManagementSystem.Utils;
using BankingManagementSystem.Core.Models.Transaction;
using Microsoft.AspNetCore.Mvc;
using BankingManagementSystem.Core.Services.Contracts;

namespace BankingManagementSystem.Controllers
{
    [Route("api/transactions")]
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
        public async Task<ActionResult<TransactionDetailsDto>> CreateTransaction(
            [FromBody] TransactionCreateDto transactionCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdTransaction = await _transactionService.ProcessTransaction(transactionCreateDto);

            var transactionDto = EntityMappers.ToTransactionDto(createdTransaction);

            return CreatedAtAction(nameof(GetTransaction), new { id = transactionDto.Id }, transactionDto);
        }

        // GET: api/transaction/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDetailsDto>> GetTransaction(int id)
        {
            var transaction = await _transactionService.GetTransactionById(id);

            var transactionDto = EntityMappers.ToTransactionDto(transaction);

            return Ok(transactionDto);
        }

        // GET: api/transaction/by-account/{accountId}
        [HttpGet("by-account/{accountId}")]
        public async Task<ActionResult<List<TransactionDetailsDto>>> GetTransactionsByAccountId(string accountId)
        {
            var transactions = await _transactionService.GetTransactionsByAccountId(accountId);
            var transactionDtos = transactions.Select(EntityMappers.ToTransactionDto).ToList();

            return Ok(transactionDtos);
        }

        // GET: api/transaction
        [HttpGet]
        public async Task<ActionResult<List<TransactionAllDto>>> GetAllTransactions()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();

            var transactionDtos = transactions.Select(EntityMappers.ToTransactionAllDto).ToList();

            return Ok(transactionDtos);
        }

        // GET: api/transaction/by-date
        [HttpGet("by-date")]
        public async Task<ActionResult<List<TransactionAllDto>>> GetTransactionsByDate(string accountId,
            DateTime startDate, DateTime endDate)
        {
            var transactions = await _transactionService.GetTransactionsByDate(accountId, startDate, endDate);

            var transactionDtos = transactions.Select(EntityMappers.ToTransactionAllDto).ToList();

            return Ok(transactionDtos);
        }

        // GET: api/transaction/by-amount
        [HttpGet("by-amount")]
        public async Task<ActionResult<List<TransactionAllDto>>> GetTransactionsByAmount(string accountId,
            decimal minAmount, decimal maxAmount)
        {
            var transactions = await _transactionService.GetTransactionsByAmount(accountId, minAmount, maxAmount);

            var transactionDtos = transactions.Select(EntityMappers.ToTransactionAllDto).ToList();

            return Ok(transactionDtos);
        }

        // GET: api/transaction/outgoing/{accountId}
        [HttpGet("outgoing/{accountId}")]
        public async Task<ActionResult<List<TransactionAllDto>>> GetOutgoingTransactions(string accountId)
        {
            var transactions = await _transactionService.GetOutgoingTransactions(accountId);

            var transactionDtos = transactions.Select(EntityMappers.ToTransactionAllDto).ToList();

            return Ok(transactionDtos);
        }

        // GET: api/transaction/incoming/{accountId}
        [HttpGet("incoming/{accountId}")]
        public async Task<ActionResult<List<TransactionAllDto>>> GetIncomingTransactions(string accountId)
        {
            var transactions = await _transactionService.GetIncomingTransactions(accountId);

            var transactionDtos = transactions.Select(EntityMappers.ToTransactionAllDto).ToList();

            return Ok(transactionDtos);
        }

        // DELETE: api/transaction/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var isCancelled = await _transactionService.CancelTransaction(id);

            if (!isCancelled)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}