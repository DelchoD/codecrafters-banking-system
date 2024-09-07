namespace BankingManagementSystem.Core.Services
{
    using BankingManagementSystem.Infrastructure.Data.Models;
    using Contracts;
    using Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;

    public class CustomerService : ICustomerService 
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> RegisterCustomer(Customer customer)
        {
            if (customer == null) 
            {
                throw new ArgumentNullException(nameof(customer));
            }

            try
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return customer;
            }
            catch (Exception ex) 
            {
                throw new InvalidOperationException("Error while registering customer", ex);
            }
        }

        public async Task<Customer> GetCustomerById(int customerId)
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

        public async Task<bool> UpdateCustomerProfile(int customerId, Customer updatedCustomer)
        {
            try
            {
                var customer = await GetCustomerById(customerId);

                customer.FirstName = updatedCustomer.FirstName;
                customer.MiddleName = updatedCustomer.MiddleName;
                customer.LastName = updatedCustomer.LastName;
                customer.Email = updatedCustomer.Email;
                customer.PersonalIDNumber = updatedCustomer.PersonalIDNumber;
                customer.DateOfBirth = updatedCustomer.DateOfBirth;
                customer.Address = updatedCustomer.Address;

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

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            try
            {
                return await _context.Customers.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error retrieving customers list.", ex);
            }
        }   
    }
}
