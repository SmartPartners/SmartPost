using SmartPost.Domain.Configurations;
using SmartPost.Service.DTOs.Partners;

namespace SmartPost.Service.Interfaces.Partners;

public interface IPartnerService
{
    Task<bool> RemoveAsync(long id);
    Task<PartnerForResultDto> RetrieveByIdAsync(long id);
    Task<PartnerForResultDto> CreateAsync(PartnerForCreationDto dto);
    Task<PartnerForResultDto> ModifyAsync(long id, PartnerForUpdateDto dto);
    Task<IEnumerable<PartnerForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
