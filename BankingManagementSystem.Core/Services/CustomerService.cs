using BankingManagementSystem.Core.Models.Customer;
using BankingManagementSystem.Core.Services.Contracts;
using BankingManagementSystem.Infrastructure.Data;
using BankingManagementSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using BankingManagementSystem.Core.Repositories;

namespace BankingManagementSystem.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerService(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
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
                _customerRepository.AddAsync(customer);
                await _customerRepository.SaveChangesAsync();

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
                var customer = await _customerRepository.FindAsync(customerId);
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

                _customerRepository.Update(customer);
                await _customerRepository.SaveChangesAsync();

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
                _customerRepository.Remove(customer);
                await _customerRepository.SaveChangesAsync();
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
                var customers = await _customerRepository.ToListAsync();
                return customers;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error retrieving customers list.", ex);
            }
        }
    }
}