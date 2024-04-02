using SmartPost.Domain.Configurations;
using SmartPost.Service.DTOs.Products;

namespace SmartPost.Service.Interfaces.Products;

public interface IProductService
{
    Task<ProductForResultDto> CreateAsync(ProductForCreationDto productForCreationDto);
    Task<ProductForResultDto> UpdateAsync(long id, ProductForUpdateDto productForUpdateDto);
    Task<bool> DeleteAsync(long id);
    Task<ProductForResultDto> GetByIdAsync(long id);
    Task<ProductForResultDto> GetByName(string name);
    Task<IEnumerable<ProductForResultDto>> GetAllAsync(PaginationParams @params);

}
