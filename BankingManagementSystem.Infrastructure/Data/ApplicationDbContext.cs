using BankingManagementSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingManagementSystem.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "BankingManagementSystem.db");
        }


        public string DbPath { get; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<LoanApplication> LoanApplications { get; set; }

        public DbSet<RiskAssessment> RiskAssessments { get; set; }

        public DbSet<CreditScore> CreditScores { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.CreditScore)
                .WithOne(c => c.Customer)
                .HasForeignKey<CreditScore>(c => c.CustomerId);


            modelBuilder.Entity<LoanApplication>()
                .HasOne(r => r.RiskAssessment)
                .WithOne(r => r.LoanApplication)
                .HasForeignKey<RiskAssessment>(r => r.LoanApplicationId);


            base.OnModelCreating(modelBuilder);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");

            base.OnConfiguring(optionsBuilder);
        }
    }
}