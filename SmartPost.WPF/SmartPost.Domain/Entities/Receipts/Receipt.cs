using SmartPost.Domain.Commons;
using SmartPost.Domain.Entities.Loans;
using SmartPost.Domain.Entities.Transactions;
using System.ComponentModel.DataAnnotations;

namespace SmartPost.Domain.Entities.Receipts;

public class Receipt : Auditable
{
    [Required]
    public decimal TotalPrice { get; set; }
    [Required]
    public decimal Discount { get; set; }
    [Required]
    public decimal PaidCash { get; set; }
    [Required]
    public decimal PaidCard { get; set; }
    [Required]
    public bool HasLoan { get; set; }

    public bool IsDeleted { get; set; }

    [Required]
    public string SellerId { get; set; } = string.Empty;
    public int WarehouseId { get; set; }


    public Loan Loan = new Loan();
    public IEnumerable<Transaction> Transactions = new List<Transaction>();
}
