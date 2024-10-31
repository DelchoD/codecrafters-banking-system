using BankingManagementSystem.Core.Services;
using BankingManagementSystem.Core.Services.Contracts;
using BankingManagementSystem.Infrastructure.Data;
using BankingManagementSystem.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace BankingManagementSystem.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IAccountService, AccountService>();

            return services;
        }
        
        public static IServiceCollection AddApplicationIdentity(this IServiceCollection services)
        {
            services
                .AddIdentityCore<Customer>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }
    }
}