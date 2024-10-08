﻿using BankingManagementSystem.Infrastructure.Data;
using BankingManagementSystem.Infrastructure.Data.Models;

namespace BankingManagementSystem.Core.Repositories
{
    public class AccountRepository : Repository<Account>
    {
        public AccountRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}