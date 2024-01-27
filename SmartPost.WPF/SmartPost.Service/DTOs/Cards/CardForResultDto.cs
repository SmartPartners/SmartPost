namespace SmartPost.Service.DTOs.Cards;
/// <summary>
/// Hamma DTO tugaganidan keyin mapperga bog'lash
/// </summary>
public record CardForResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public string TransNo { get; set; }
    public string ProductName { get; set; }
    public string Size { get; set; }
    public string BarCode { get; set; }
    public string PCode { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal DiscPercent { get; set; }
    public decimal Quantity { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
