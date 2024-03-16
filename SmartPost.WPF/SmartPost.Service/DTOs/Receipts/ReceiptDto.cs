using SmartPost.Domain.Entities.Loans;
using SmartPost.Domain.Entities.Receipts;
using SmartPost.Domain.Entities.Transactions;

namespace SmartPost.Service.DTOs.Receipts;

public class ReceiptDto
{
    public long Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal PaidCash { get; set; }
    public decimal PaidCard { get; set; }
    public bool HasLoan { get; set; }
    public string SellerId { get; set; } = string.Empty;
    public Loan Loan = new Loan();
    public List<Transaction> Transactions = new List<Transaction>();

    public static explicit operator ReceiptDto(Receipt v)
        => new ReceiptDto()
        {
            TotalPrice = v.TotalPrice,
            Discount = v.Discount,
            PaidCash = v.PaidCash,
            PaidCard = v.PaidCard,
            HasLoan = v.HasLoan,
            Id = v.Id,
            Loan = v.Loan,
            SellerId = v.SellerId,
            Transactions = v.Transactions.ToList()
        };

    public static explicit operator Receipt(ReceiptDto v)
        => new Receipt()
        {
            TotalPrice = v.TotalPrice,
            Discount = v.Discount,
            PaidCash = v.PaidCash,
            PaidCard = v.PaidCard,
            HasLoan = v.HasLoan,
            Id = v.Id,
            Loan = v.Loan,
            SellerId = v.SellerId,
            Transactions = v.Transactions.ToList()
        };
}
