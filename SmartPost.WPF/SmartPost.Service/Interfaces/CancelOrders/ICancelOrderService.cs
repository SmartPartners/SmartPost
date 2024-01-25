using SmartPost.Domain.Configurations;
using SmartPost.Service.DTOs.CancelOrders;

namespace SmartPost.Service.Interfaces.CancelOrders;
/// <summary>
/// keyingi bosqich Service ni yaratish
/// </summary>
public interface ICancelOrderService
{
    Task<bool> ReamoveAsync(long id);
    Task<CancelOrderForResultDto> RetrieveByIdAsync(long id);
    Task<CancelOrderForResultDto> CreateAsync(CancelOrderForCreationDto dto);
    Task<CancelOrderForResultDto> ModifyAsync(long id, CancelOrderForUpdateDto dto);
    Task<IEnumerable<CancelOrderForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
