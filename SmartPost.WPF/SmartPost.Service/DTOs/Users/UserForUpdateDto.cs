namespace SmartPost.Service.DTOs.Users;

public record UserForUpdateDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public long RoleId { get; set; }
}
