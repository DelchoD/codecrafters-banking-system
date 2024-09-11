namespace BankingManagementSystem.Core.Services
{
    using BankingManagementSystem.Infrastructure.Data.Models;
    using Contracts;
    using Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;
    using BankingManagementSystem.Core.Models.Customer;
    using BankingManagementSystem.Core.Models.User;

    public class CustomerService : ICustomerService 
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
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
                PersonalIDNumber = customerDTO.PersonalIDNumber,
                DateOfBirth = customerDTO.DateOfBirth,
                Address = customerDTO.Address,
            };
        }

        private CustomerFormDTO toCustomerFormDTO(Customer customer) 
        {
            return new CustomerFormDTO
            {
                FirstName = customer.FirstName,
                MiddleName = customer.MiddleName,
                LastName = customer.LastName,
                Email = customer.Email,
                Password = customer.Password,
                PersonalIDNumber = customer.PersonalIDNumber,
                DateOfBirth = customer.DateOfBirth,
                Address = customer.Address,
            };
        }

        private CustomerAllDTO toCustomerAllDTO(Customer customer)
        {
            return new CustomerAllDTO
            {
                Id = customer.Id,
                FirstName = customer.FirstName,
                MiddleName = customer.MiddleName,
                LastName = customer.LastName,
                Email = customer.Email,
                PersonalIDNumber = customer.PersonalIDNumber,
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

        // for internal use only
        private async Task<Customer> GetCustomerById(int customerId) 
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

        public async Task<CustomerAllDTO> GetCustomerDTOById(int customerId)
        {
            var customer = await GetCustomerById(customerId);

            return toCustomerAllDTO(customer);
        }

        public async Task<bool> UpdateCustomerProfile(int customerId, CustomerUpdateDTO customerUpdates)
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

                _context.Customers.Update(customer);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) 
            {
                throw new InvalidOperationException($"Error updating profile for customer with ID {customerId}.", ex);
            }
        }

        public async Task<bool> DeleteCustomer(int customerId)
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

        public async Task<IEnumerable<CustomerAllDTO>> GetAllCustomers()
        {
            try
            {
                var customers = await _context.Customers.ToListAsync();
                var customerDTOs = customers.Select(customer => toCustomerAllDTO(customer));
                return customerDTOs;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error retrieving customers list.", ex);
            }
        }   
    }
}
