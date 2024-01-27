using SmartPost.Domain.Configurations;
using SmartPost.Service.DTOs.Products;

namespace SmartPost.Service.Interfaces.Products;

public  interface IProductService
{
    Task<bool> DeleteAsync(long id);
    Task<ProductForResultDto>GetByIdAsync(long id);
    Task<IEnumerable<ProductForResultDto>> GetAllAsync(PaginationParams @params);
    public Task<ProductForResultDto> GetByName(string name);
    Task<ProductForResultDto> UpdateAsync(long id,ProductForUpdateDto productForUpdateDto);
    Task<ProductForResultDto> CreateAsync(ProductForCreationDto productForCreationDto);

}
