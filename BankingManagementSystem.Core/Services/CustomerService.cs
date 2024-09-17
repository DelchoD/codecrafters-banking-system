using BankingManagementSystem.Core.Models.Customer;
using BankingManagementSystem.Core.Services.Contracts;
using BankingManagementSystem.Infrastructure.Data;
using BankingManagementSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingManagementSystem.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> RegisterCustomer(FormDto customerDto)
        {
            if (customerDto == null)
            {
                throw new ArgumentNullException(nameof(customerDto));
            }

            try
            {
                var customer = new Customer
                {
                    FirstName = customerDto.FirstName,
                    MiddleName = customerDto.MiddleName,
                    LastName = customerDto.LastName,
                    Email = customerDto.Email,
                    Password = customerDto.Password,
                    PersonalIdNumber = customerDto.PersonalIDNumber,
                    DateOfBirth = customerDto.DateOfBirth,
                    Address = customerDto.Address,
                };
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();

                return customer;
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

        public async Task<Customer> UpdateCustomerProfile(string customerId, UpdateDto customerUpdates)
        {
            try
            {
                var customer = await GetCustomerById(customerId);

                if (!string.IsNullOrWhiteSpace(customerUpdates.Email) && customer.Email != customerUpdates.Email)
                {
                    customer.Email = customerUpdates.Email;
                }
                else if (!string.IsNullOrWhiteSpace(customerUpdates.PhoneNumber) &&
                         customer.PhoneNumber != customerUpdates.PhoneNumber)
                {
                    customer.PhoneNumber = customerUpdates.PhoneNumber;
                }
                else if (!string.IsNullOrWhiteSpace(customerUpdates.Password) &&
                         customer.Password != customerUpdates.Password)
                {
                    customer.Password = customerUpdates.Password;
                }
                else if (!string.IsNullOrEmpty(customerUpdates.Address) && customer.Address != customerUpdates.Address)
                {
                    customer.Address = customerUpdates.Address;
                }

                _context.Customers.Update(customer);
                await _context.SaveChangesAsync();

                return customer;
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

        public async Task<List<Customer>> GetAllCustomers()
        {
            try
            {
                var customers = await _context.Customers.ToListAsync();
                return customers;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error retrieving customers list.", ex);
            }
        }
    }
}