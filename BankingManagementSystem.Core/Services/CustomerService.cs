namespace BankingManagementSystem.Core.Services
{
    using BankingManagementSystem.Infrastructure.Data.Models;
    using Contracts;
    using Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;
    using BankingManagementSystem.Core.Models.Customer;
    using BankingManagementSystem.Core.Models.User;
    using BankingManagementSystem.Core.Models.Account;
    using BankingManagementSystem.Core.Models.Transaction;

    public class CustomerService : ICustomerService 
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }

        //this should be imported
        private AccountDetailsDto mapAccountToDetailsDto(Account account)
        {
            return new AccountDetailsDto
            {
                AccountId = account.Id,
                Name = account.Name,
                Iban = account.Iban,
                Balance = account.Balance,
                CustomerId = account.CustomerId,
                TransactionsFrom = account.TransactionsFrom.Select(mapAccountToDetailsDto).ToList(),
                TransactionsTo = account.TransactionsTo.Select(mapAccountToDetailsDto).ToList()
            };
        }
        //this should be imported
        private TransactionDetailsDTO mapAccountToDetailsDto(Transaction transaction)
        {
            return new TransactionDetailsDTO
            {
                Id = transaction.Id,
                TotalAmount = transaction.TotalAmount,
                Date = transaction.Date,
                IbanFrom = transaction.IBANFrom.Iban,
                IbanTo = transaction.IBANTo.Iban,
                Reason = transaction.Reason
            };
        }

        private Customer toCustomer(CustomerFormDTO customerDTO) 
        {
            return new Customer
            {
                FirstName = customerDTO.FirstName,
                MiddleName = customerDTO.MiddleName,
                LastName = customerDTO.LastName,
                Email = customerDTO.Email,
                Password = customerDTO.Password,
                PersonalIdNumber = customerDTO.PersonalIDNumber,
                DateOfBirth = customerDTO.DateOfBirth,
                Address = customerDTO.Address,
            };
        }

        public CustomerAllDTO toCustomerAllDTO(Customer customer)
        {
             return new CustomerAllDTO
               {
                   Id = customer.Id,
                   FirstName = customer.FirstName,
                   MiddleName = customer.MiddleName,
                   LastName = customer.LastName,
                   Email = customer.Email,
                   PersonalIDNumber = customer.PersonalIdNumber,
                   Accounts = customer.Accounts.Select(mapAccountToDetailsDto).ToList(),
               };
        }

        public async Task<CustomerAllDTO> RegisterCustomer(CustomerFormDTO customerDTO)
        {
            if (customerDTO == null) 
            {
                throw new ArgumentNullException(nameof(customerDTO));
            }

            try
            {
                Customer customer = toCustomer(customerDTO);
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                return toCustomerAllDTO(customer);
            }
            catch (Exception ex) 
            {
                throw new InvalidOperationException("Error while registering customer", ex);
            }
        }

        public async Task<Customer> GetCustomerById(string customerId) 
        {
            try
            {
                var customer = await _context.Customers.FindAsync(customerId);
                if (customer == null)
                {
                    throw new KeyNotFoundException("Customer not found");
                }
                return customer;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving customer with ID {customerId}.", ex);
            }
        }

        public async Task<CustomerAllDTO> GetCustomerDTOById(string customerId)
        {
            var customer = await GetCustomerById(customerId);

            return toCustomerAllDTO(customer);
        }
        
         public async Task<CustomerAllDTO> UpdateCustomerProfile(string customerId, CustomerUpdateDTO customerUpdates)
   {
       try
       {
           var customer = await GetCustomerById(customerId);

           if (!string.IsNullOrWhiteSpace(customerUpdates.Email) && customer.Email != customerUpdates.Email)
           {
               customer.Email = customerUpdates.Email;
           }
           else if (!string.IsNullOrWhiteSpace(customerUpdates.PhoneNumber) && customer.PhoneNumber != customerUpdates.PhoneNumber)
           {
               customer.PhoneNumber = customerUpdates.PhoneNumber;
           }
           else if (!string.IsNullOrWhiteSpace(customerUpdates.Password) && customer.Password != customerUpdates.Password)
           {
               customer.Password = customerUpdates.Password;
           }
           else if (!string.IsNullOrEmpty(customerUpdates.Address) && customer.Address != customerUpdates.Address)
           {
               customer.Address = customerUpdates.Address;
           }
           _context.Customers.Update(customer);
           await _context.SaveChangesAsync();
           return toCustomerAllDTO(customer);
       }
       catch (Exception ex) 
       {
           throw new InvalidOperationException($"Error updating profile for customer with ID {customerId}.", ex);
       }
   }

        public async Task<bool> DeleteCustomer(string customerId)
        {
            try
            {
                var customer = await GetCustomerById(customerId);
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error deleting customer with ID {customerId}.", ex);
            }
        }

        public async Task<List<CustomerAllDTO>> GetAllCustomers()
        {
            try
            {
                var customers = await _context.Customers.ToListAsync();
                var customerDTOs = customers.Select(customer => toCustomerAllDTO(customer)).ToList();
                return customerDTOs;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error retrieving customers list.", ex);
            }
        }   
    }
}
