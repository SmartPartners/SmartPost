using SmartPost.Domain.Enums;

namespace SmartPost.Service.DTOs.Users;

public record UserForCreationDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public UserRole Role { get; set; }
}
