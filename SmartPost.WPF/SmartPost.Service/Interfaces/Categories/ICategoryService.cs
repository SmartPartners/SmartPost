using SmartPost.Domain.Configurations;
using SmartPost.Service.DTOs.Categories;

namespace SmartPost.Service.Interfaces.Categories;

public interface ICategoryService
{
    Task<bool> ReamoveAsync(long id);
    Task<CategoryForResultDto> RetrieveByIdAsync(long id);
    Task<CategoryForResultDto> CreateAsync(CategoryForCreationDto dto);
    Task<CategoryForResultDto> ModifyAsync(long id, CategoryForUpdateDto dto);
    Task<IEnumerable<CategoryForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
