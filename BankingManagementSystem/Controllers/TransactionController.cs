namespace BankingManagementSystem.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Core.Services.Contracts;
    using BankingManagementSystem.Core.Models.Transaction;
    using BankingManagementSystem.Core.Models.Account;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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
        public async Task<ActionResult<TransactionDetailsDTO>> CreateTransaction([FromBody] TransactionCreateDTO transactionCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdTransaction = await _transactionService.ProcessTransaction(transactionCreateDTO);

            var transactionDTO = new TransactionDetailsDTO
            {
                Id = createdTransaction.Id,
                Date = createdTransaction.Date,
                TotalAmount = createdTransaction.TotalAmount,
                Reason = createdTransaction.Reason,
                IBANFrom = new AccountTransactionDto { Iban = createdTransaction.IBANFromId },
                IBANTo = new AccountTransactionDto { Iban = createdTransaction.IBANToId }
            };

            return CreatedAtAction(nameof(GetTransaction), new { id = transactionDTO.Id }, transactionDTO);
        }

        // GET: api/transaction/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDetailsDTO>> GetTransaction(int id)
        {
            var transaction = await _transactionService.GetTransactionById(id);

            var transactionDTO = new TransactionDetailsDTO
            {
                Id = transaction.Id,
                Date = transaction.Date,
                TotalAmount = transaction.TotalAmount,
                Reason = transaction.Reason,
                IBANFrom = new AccountTransactionDto { Iban = transaction.IBANFromId },
                IBANTo = new AccountTransactionDto { Iban = transaction.IBANToId }
            };

            return Ok(transactionDTO);
        }

        // GET: api/transaction/by-account/{accountId}
        [HttpGet("by-account/{accountId}")]
        public async Task<ActionResult<List<TransactionDetailsDTO>>> GetTransactionsByAccountId(string accountId)
        {
            var transactions = await _transactionService.GetTransactionsByAccountId(accountId);
            var transactionDTOs = transactions.Select(transaction => new TransactionDetailsDTO
            {
                Id = transaction.Id,
                Date = transaction.Date,
                TotalAmount = transaction.TotalAmount,
                Reason = transaction.Reason,
                IBANFrom = new AccountTransactionDto { Iban = transaction.IBANFromId },
                IBANTo = new AccountTransactionDto { Iban = transaction.IBANToId }
            }).ToList();

            return Ok(transactionDTOs);
        }

        // GET: api/transaction
        [HttpGet]
        public async Task<ActionResult<List<TransactionAllDTO>>> GetAllTransactions()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();

            var transactionDTOs = transactions.Select(transaction => new TransactionAllDTO
            {
                Id = transaction.Id,
                Date = transaction.Date,
                TotalAmount = transaction.TotalAmount,
                IBANFrom = new AccountTransactionDto { Iban = transaction.IBANFromId },
                IBANTo = new AccountTransactionDto { Iban = transaction.IBANToId }
            }).ToList();

            return Ok(transactionDTOs);
        }

        // GET: api/transaction/by-date
        [HttpGet("by-date")]
        public async Task<ActionResult<List<TransactionAllDTO>>> GetTransactionsByDate(string accountId, DateTime startDate, DateTime endDate)
        {
            var transactions = await _transactionService.GetTransactionsByDate(accountId, startDate, endDate);

            var transactionDTOs = transactions.Select(transaction => new TransactionAllDTO
            {
                Id = transaction.Id,
                Date = transaction.Date,
                TotalAmount = transaction.TotalAmount,
                IBANFrom = new AccountTransactionDto { Iban = transaction.IBANFromId },
                IBANTo = new AccountTransactionDto { Iban = transaction.IBANToId }
            }).ToList();

            return Ok(transactionDTOs);
        }
        // GET: api/transaction/by-amount
        [HttpGet("by-amount")]
        public async Task<ActionResult<List<TransactionAllDTO>>> GetTransactionsByAmount(string accountId, decimal minAmount, decimal maxAmount)
        {
            var transactions = await _transactionService.GetTransactionsByAmount(accountId, minAmount, maxAmount);

            var transactionDTOs = transactions.Select(transaction => new TransactionAllDTO
            {
                Id = transaction.Id,
                Date = transaction.Date,
                TotalAmount = transaction.TotalAmount,
                IBANFrom = new AccountTransactionDto { Iban = transaction.IBANFromId },
                IBANTo = new AccountTransactionDto { Iban = transaction.IBANToId }
            }).ToList();

            return Ok(transactionDTOs);
        }

        // GET: api/transaction/outgoing/{accountId}
        [HttpGet("outgoing/{accountId}")]
        public async Task<ActionResult<List<TransactionAllDTO>>> GetOutgoingTransactions(string accountId)
        {
            var transactions = await _transactionService.GetOutgoingTransactions(accountId);

            var transactionDTOs = transactions.Select(transaction => new TransactionAllDTO
            {
                Id = transaction.Id,
                Date = transaction.Date,
                TotalAmount = transaction.TotalAmount,
                IBANFrom = new AccountTransactionDto { Iban = transaction.IBANFromId },
                IBANTo = new AccountTransactionDto { Iban = transaction.IBANToId }
            }).ToList();

            return Ok(transactionDTOs);
        }

        // GET: api/transaction/incoming/{accountId}
        [HttpGet("incoming/{accountId}")]
        public async Task<ActionResult<List<TransactionAllDTO>>> GetIncomingTransactions(string accountId)
        {
            var transactions = await _transactionService.GetIncomingTransactions(accountId);

            var transactionDTOs = transactions.Select(transaction => new TransactionAllDTO
            {
                Id = transaction.Id,
                Date = transaction.Date,
                TotalAmount = transaction.TotalAmount,
                IBANFrom = new AccountTransactionDto { Iban = transaction.IBANFromId },
                IBANTo = new AccountTransactionDto { Iban = transaction.IBANToId }
            }).ToList();

            return Ok(transactionDTOs);
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
