using SmartPost.Domain.Enums;

namespace SmartPost.Service.DTOs.Users;

public record UserForUpdateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public UserRole Role { get; set; }
}
