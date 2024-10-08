﻿namespace BankingManagementSystem.Core.Models.Account
{
    public class AccountTransactionDto
    {
        public string TransactionId { get; set; } = string.Empty;

        public string Iban { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
    }
}