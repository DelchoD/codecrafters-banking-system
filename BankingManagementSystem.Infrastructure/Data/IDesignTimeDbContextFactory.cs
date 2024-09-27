using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure; 
using BankingManagementSystem.Infrastructure.Data;

public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseMySql("Server=localhost;Database=BankingManagementSystem;Username=codecrafters;Password=1234",
            new MySqlServerVersion(new Version(8, 0, 21))); 

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}
