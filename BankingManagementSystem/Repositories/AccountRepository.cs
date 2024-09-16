public class AccountRepository : Repository<Account>
{
    public AccountRepository(ApplicationDbContext context) : base(context) { }

}