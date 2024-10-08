﻿using BankingManagementSystem.Utils;
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
        public async Task<ActionResult<TransactionDetailsDto>> CreateTransaction([FromBody] TransactionCreateDto transactionCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdTransaction = await _transactionService.ProcessTransaction(transactionCreateDto);

            var transactionDto = EntityMappers.ToTransactionDto(createdTransaction);

            return Ok(transactionDto);
        }

        // GET: api/transaction/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<TransactionDetailsDto>> GetTransactionById(int id)
        {
            var transaction = await _transactionService.GetTransactionById(id);

            var transactionDto = EntityMappers.ToTransactionDto(transaction);

            return Ok(transactionDto);
        }

        // GET: api/transaction/by-account/{accountId}
        [HttpGet("by-account/{accountId}")]
        public ActionResult<List<TransactionDetailsDto>> GetTransactionsByAccountId(string accountId)
        {
            var transactions = _transactionService.GetTransactionsByAccountId(accountId);
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
        public ActionResult<List<TransactionAllDto>> GetTransactionsByDate(string accountId, DateTime startDate, DateTime endDate)
        {
            var transactions = _transactionService.GetTransactionsByDate(accountId, startDate, endDate);

            var transactionDtos = transactions.Select(EntityMappers.ToTransactionAllDto).ToList();

            return Ok(transactionDtos);
        }

        // GET: api/transaction/by-amount
        [HttpGet("by-amount")]
        public ActionResult<List<TransactionAllDto>> GetTransactionsByAmount(string accountId, decimal minAmount, decimal maxAmount)
        {
            var transactions = _transactionService.GetTransactionsByAmount(accountId, minAmount, maxAmount);

            var transactionDtos = transactions.Select(EntityMappers.ToTransactionAllDto).ToList();

            return Ok(transactionDtos);
        }

        // GET: api/transaction/outgoing/{accountId}
        [HttpGet("outgoing/{accountId}")]
        public ActionResult<List<TransactionAllDto>> GetOutgoingTransactions(string accountId)
        {
            var transactions = _transactionService.GetOutgoingTransactions(accountId);

            var transactionDtos = transactions.Select(EntityMappers.ToTransactionAllDto).ToList();

            return Ok(transactionDtos);
        }

        // GET: api/transaction/incoming/{accountId}
        [HttpGet("incoming/{accountId}")]
        public ActionResult<List<TransactionAllDto>> GetIncomingTransactions(string accountId)
        {
            var transactions = _transactionService.GetIncomingTransactions(accountId);

            var transactionDtos = transactions.Select(EntityMappers.ToTransactionAllDto).ToList();

            return Ok(transactionDtos);
        }

        // DELETE: api/transaction/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var isCancelled = await _transactionService.CancelTransaction(id);

            if (!isCancelled)
                return NotFound();

            return NoContent();
        }
    }
}