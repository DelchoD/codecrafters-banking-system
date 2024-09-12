public class TransactionRepository : Repository<Transaction>
{
    public TransactionRepository(ApplicationDbContext context) : base(context) { }

}