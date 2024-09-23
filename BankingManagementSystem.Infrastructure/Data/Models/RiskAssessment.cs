using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankingManagementSystem.Infrastructure.Data.Models
{
    using static Constants.ValidationConstants;

    public class RiskAssessment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public short RiskScore { get; set; }

        public DateTime Date { get; set; }

        [MaxLength(RiskAssessmentDetailsMaxLength)]
        public string? Details { get; set; }

        public int LoanApplicationId { get; set; }

        [Required]
        public LoanApplication LoanApplication { get; set; } = null!;
    }
}