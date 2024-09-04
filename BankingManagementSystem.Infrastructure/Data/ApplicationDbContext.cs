using Microsoft.EntityFrameworkCore;

namespace BankingManagementSystem.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {

        }


        //Create DbSets
   


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
