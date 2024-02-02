using SmartPost.Service.DTOs.PartnerProduct;

namespace SmartPost.Service.DTOs.Partners;

public record PartnerForResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }

    public ICollection<PartnerProductForResultDto> PartnersProducts { get; set; }
}
