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

            var transaction = new Transaction
            {
                Date = transactionCreateDTO.Date,
                TotalAmount = transactionCreateDTO.TotalAmount,
                Reason = transactionCreateDTO.Reason,
                IBANFromId = transactionCreateDTO.IBANFrom.IBAN,
                IBANToId = transactionCreateDTO.IBANTo.IBAN,
                CreatorId = transactionCreateDTO.CreatorId
            };

            var createdTransaction = await _transactionService.ProcessTransaction(transaction);
            return CreatedAtAction(nameof(GetTransaction), new { id = createdTransaction.Id }, createdTransaction);
        }

        // GET: api/transaction/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDetailsDTO>> GetTransaction(int id)
        {
            var transaction = await _transactionService.GetTransactionById(id);
            if (transaction == null)
            {
                return NotFound();
            }

            var transactionDTO = new TransactionDetailsDTO
            {
                Id = transaction.Id,
                Date = transaction.Date,
                TotalAmount = transaction.TotalAmount,
                Reason = transaction.Reason,
                IBANFrom = new AccountTransactionDTO { IBAN = transaction.IBANFromId },
                IBANTo = new AccountTransactionDTO { IBAN = transaction.IBANToId },
                CreatorId = transaction.CreatorId
            };

            return Ok(transactionDTO);
        }

        // GET: api/transaction
        [HttpGet]
        public async Task<ActionResult<List<TransactionAllDTO>>> GetAllTransactions()
        {
            var transactions = await _transactionService.GetAllTransactionsAsync();
            var transactionDTOs = transactions.Select(t => new TransactionAllDTO
            {
                Id = t.Id,
                Date = t.Date,
                TotalAmount = t.TotalAmount,
                IBANFrom = new AccountTransactionDTO { IBAN = t.IBANFromId },
                IBANTo = new AccountTransactionDTO { IBAN = t.IBANToId }
            }).ToList();

            return Ok(transactionDTOs);
        }

        // DELETE: api/transaction/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _transactionService.GetTransactionById(id);

            if (transaction == null)
            {
                return NotFound();
            }

            await _transactionService.CancelTransaction(id);

            return NoContent();
        }
    }
}
