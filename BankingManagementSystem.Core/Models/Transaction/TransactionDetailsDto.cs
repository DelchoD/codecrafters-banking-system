namespace BankingManagementSystem.Core.Models.Transaction
{
    public class TransactionDetailsDto
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal TotalAmount { get; set; }
        
        public string Reason { get; set; } = string.Empty;

        public string? IbanFrom { get; set; }

        public string? IbanTo { get; set; }
    }
}