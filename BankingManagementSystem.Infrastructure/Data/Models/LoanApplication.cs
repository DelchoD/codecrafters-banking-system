using BankingManagementSystem.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingManagementSystem.Infrastructure.Data.Models
{
    using static BankingManagementSystem.Infrastructure.Data.Constants.ValidationConstants;
    

    public class LoanApplication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public DateTime DateCreated { get; set; }

        public DateTime DateApproved { get; set; }

        public decimal AmountRequested { get; set; }

        public byte RepaymentPeriod { get; set; }


        [Required]
        [MaxLength(LoanApplicationReasonMaxLength)]
        public string Reason { get; set; } = string.Empty;


        public LoanType Type { get; set; }

        public LoanStatus Status { get; set; }


        [Required]
        public string CustomerId { get; set; } = string.Empty;

        [Required]
        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; } = null!;

        public RiskAssessment? RiskAssessment { get; set; }

    }
}
