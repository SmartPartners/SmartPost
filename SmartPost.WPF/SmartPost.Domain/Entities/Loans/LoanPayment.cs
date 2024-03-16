using SmartPost.Domain.Commons;
using System.ComponentModel.DataAnnotations;

namespace SmartPost.Domain.Entities.Loans;

public class LoanPayment : Auditable
{
    public decimal PaidCash { get; set; }
    public decimal PaidCard { get; set; }
    [Required]
    public decimal TotalPayment { get; set; }

    public int LoanId { get; set; }
    public string SellerId { get; set; } = string.Empty;
}
