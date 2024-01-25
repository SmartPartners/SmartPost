namespace SmartPost.Service.DTOs.Cards;

/// <summary>
/// Hamma DTO tugaganidan keyin mapperga bog'lash
/// </summary>
public record CardForUpdateDto
{
    public long UserId { get; set; }
    public string TransNo { get; set; }
    public string ProductName { get; set; }
    public string BarCode { get; set; }
    public long PCode { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal DiscPercent { get; set; }
    public decimal Quantity { get; set; }
    public string Status { get; set; }
}
