using SmartPost.Domain.Commons;
using System.ComponentModel.DataAnnotations;

namespace SmartPost.Domain.Entities.Loans;

public class Loan : Auditable
{
    [Required]
    public decimal PaidCash { get; set; }
    [Required]
    public decimal PaidCard { get; set; }
    [Required]
    public decimal TotalPayment { get; set; }
    [Required]
    public decimal LeftAmount { get; set; }

    [Required]
    public int ReceiptId { get; set; }
    [Required]
    public int CustomerId { get; set; }
    public string SellerId { get; set; } = string.Empty;


    public IEnumerable<LoanPayment> LoanPayments = new List<LoanPayment>();
}
