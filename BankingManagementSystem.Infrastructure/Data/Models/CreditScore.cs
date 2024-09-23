using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingManagementSystem.Infrastructure.Data.Models
{
    public class CreditScore
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public short Score { get; set; }

        public DateTime Date { get; set; }

        [Required]
        public string CustomerId { get; set; } = string.Empty;

        public Customer Customer { get; set; } = null!;
    }
}