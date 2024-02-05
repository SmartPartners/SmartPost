namespace SmartPost.Service.DTOs.PartnerProduct;

public record PartnerProductForCreationDto
{
    public long PartnerId { get; set; }
    public long BrandId { get; set; }
    public long CategoryId { get; set; }
    public long UserId { get; set; }
    public string TransNo { get; set; }
    public string ProductName { get; set; }
    public string PCode { get; set; }
    public string BarCode { get; set; }
    public decimal Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
}
