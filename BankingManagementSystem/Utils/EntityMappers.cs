using BankingManagementSystem.Core.Models.Account;
using BankingManagementSystem.Core.Models.Customer;
using BankingManagementSystem.Core.Models.Transaction;
using BankingManagementSystem.Infrastructure.Data.Models;

namespace BankingManagementSystem.Utils;

public static class EntityMappers
{
    public static AccountDetailsDto MapAccountToDetailsDto(Account account)
    {
        return new AccountDetailsDto
        {
            AccountId = account.Id,
            Name = account.Name,
            Iban = account.Iban,
            Balance = account.Balance,
            CustomerId = account.CustomerId,
            TransactionsFrom = account.TransactionsFrom.Select(MapTransactionToAllDto).ToList(),
            TransactionsTo = account.TransactionsTo.Select(MapTransactionToAllDto).ToList()
        };
    }

    public static TransactionDetailsDto MapTransactionToAllDto(Transaction transaction)
    {
        return new TransactionDetailsDto
        {
            Id = transaction.Id,
            TotalAmount = transaction.TotalAmount,
            Date = transaction.Date,
            IbanFrom = transaction.IBANFrom.Iban,
            IbanTo = transaction.IBANTo.Iban,
            Reason = transaction.Reason
        };
    }

    public static TransactionDetailsDto ToTransactionDto(Transaction transaction)
    {
        return new TransactionDetailsDto
        {
            Id = transaction.Id,
            Date = transaction.Date,
            TotalAmount = transaction.TotalAmount,
            Reason = transaction.Reason,
            IbanFrom = transaction.IBANFrom.Iban,
            IbanTo = transaction.IBANTo.Iban,
        };
    }

    public static TransactionAllDto ToTransactionAllDto(Transaction transaction)
    {
        return new TransactionAllDto
        {
            Id = transaction.Id,
            Date = transaction.Date,
            TotalAmount = transaction.TotalAmount,
            IBANFrom = new AccountTransactionDto { Iban = transaction.IBANFromId },
            IBANTo = new AccountTransactionDto { Iban = transaction.IBANToId }
        };
    }

    public static TransactionDetailsDto MapTransactionsToDetailsDto(Transaction transaction)
    {
        return new TransactionDetailsDto
        {
            Id = transaction.Id,
            TotalAmount = transaction.TotalAmount,
            Date = transaction.Date,
            IbanFrom = transaction.IBANFrom.Iban,
            IbanTo = transaction.IBANTo.Iban,
            Reason = transaction.Reason
        };
    }

    public static AllDto ToCustomerAllDto(Customer customer)
    {
        return new AllDto
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            MiddleName = customer.MiddleName,
            LastName = customer.LastName,
            Email = customer.Email,
            PersonalIdNumber = customer.PersonalIdNumber,
            Accounts = customer.Accounts.Select(account => new AccountDetailsDto
            {
                AccountId = account.Id,
                Name = account.Name,
                Iban = account.Iban,
                Balance = account.Balance,
                CustomerId = account.CustomerId,
                TransactionsFrom = account.TransactionsFrom.Select(MapTransactionsToDetailsDto).ToList(),
                TransactionsTo = account.TransactionsTo.Select(MapTransactionsToDetailsDto).ToList()
            }).ToList(),
        };
    }
}