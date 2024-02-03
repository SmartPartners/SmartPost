namespace SmartPost.Service.DTOs.PartnerProduct;

public record PartnerProductForResultDto
{
    public long Id { get; set; }
    public long PartnerId { get; set; }
    public long BrandId { get; set; }
    public long CategoryId { get; set; }
    public long UserId { get; set; }
    public string ProductName { get; set; }
    public string PCode { get; set; }
    public string BarCode { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; }
    public decimal Paid { get; set; }
    public decimal Debt { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
