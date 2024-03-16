using SmartPost.Domain.Entities.Loans;
using SmartPost.Domain.Entities.Receipts;
using SmartPost.Service.DTOs.Transactions;

namespace SmartPost.Service.DTOs.Receipts;

public class AddReceiptDto
{
    public decimal TotalPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal PaidCash { get; set; }
    public decimal PaidCard { get; set; }
    public bool HasLoan { get; set; }
    public string SellerId { get; set; } = string.Empty;
    public Loan Loan = new Loan();
    public List<TransactionDto> Transactions = new List<TransactionDto>();

    public static explicit operator Receipt(AddReceiptDto v)
        => new Receipt()
        {
            TotalPrice = v.TotalPrice,
            Discount = v.Discount,
            PaidCash = v.PaidCash,
            PaidCard = v.PaidCard,
            HasLoan = v.HasLoan,
            SellerId = v.SellerId,
            IsDeleted = false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
}
