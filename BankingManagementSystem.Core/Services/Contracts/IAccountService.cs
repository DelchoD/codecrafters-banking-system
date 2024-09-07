using BankingManagementSystem.Infrastructure.Data.Models;

namespace BankingManagementSystem.Core.Services.Contracts
{
    public interface IAccountService
    {
        List<Account> GetCustomerAccounts(Customer customer);

        //Add more methods

    }
}
