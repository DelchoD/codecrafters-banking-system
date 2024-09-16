public class CustomerRepository : Repository<Customer>
{
    public CustomerRepository(ApplicationDbContext context) : base(context) { }

}