using SmartPost.Domain.Configurations;
using SmartPost.Service.DTOs.InventoryLists;

namespace SmartPost.Service.Interfaces.InventoryLists;

public interface IInventoryListService
{
    Task<bool> ReamoveAsync(long id);
    Task<InventoryListForResultDto> RetrieveByIdAsync(long id);
    Task<InventoryListForResultDto> CreateAsync(InventoryListForCreationDto dto);
    Task<InventoryListForResultDto> ModifyAsync(long id, InventoryListForUpdateDto dto);
    Task<IEnumerable<InventoryListForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
