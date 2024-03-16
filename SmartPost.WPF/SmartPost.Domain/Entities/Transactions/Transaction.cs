using SmartPost.Domain.Commons;
using System.ComponentModel.DataAnnotations;

namespace SmartPost.Domain.Entities.Transactions;

public class Transaction : Auditable
{
    [Required]
    public int Quantity { get; set; }
    [Required]
    public decimal TotalPrice { get; set; }
    [Required]
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal ProductPrice { get; set; }
    [Required]
    public int ReceiptId { get; set; }
    public bool IsDeleted { get; set; }
}
