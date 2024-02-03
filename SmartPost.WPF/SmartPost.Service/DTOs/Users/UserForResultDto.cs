using SmartPost.Domain.Enums;
using SmartPost.Service.DTOs.Cards;
using SmartPost.Service.DTOs.PartnerProduct;
using SmartPost.Service.DTOs.StockProducts;

namespace SmartPost.Service.DTOs.Users;

public record UserForResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    public UserRole Role { get; set; }

    public ICollection<StockProductsForResultDto> StokProducts { get; set; }
    public ICollection<CardForResultDto> Cards { get; set; }
    public ICollection<PartnerProductForResultDto> PartnerProducts { get; set; }
}
