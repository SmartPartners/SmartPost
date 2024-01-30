using SmartPost.Domain.Entities.Cards;
using SmartPost.Domain.Entities.StokProducts;
using SmartPost.Domain.Enums;

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

    public ICollection<StokProduct> StokProducts { get; set; }
    public ICollection<Card> Cards { get; set; }
}
