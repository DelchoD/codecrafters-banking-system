namespace BankingManagementSystem.Core.Models.Transaction
{
    public class TransactionAllDto
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal TotalAmount { get; set; }

        public string IbanFrom { get; set; } = null!;

        public string IbanTo { get; set; } = null!;
    }
}