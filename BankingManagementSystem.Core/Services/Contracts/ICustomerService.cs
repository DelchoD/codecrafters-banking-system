using BankingManagementSystem.Core.Models.Customer;
using BankingManagementSystem.Infrastructure.Data.Models;

namespace BankingManagementSystem.Core.Services.Contracts
{
    public interface ICustomerService
    {
        Task<Customer> RegisterCustomer(FormDto customerDto);
        Task<Customer> GetCustomerById(string customerId);
        Task<Customer> UpdateCustomerProfile(string customerId, UpdateDto customerUpdates);
        Task<bool> DeleteCustomer(string customerId);
        Task<List<Customer>> GetAllCustomers();
    }
}
