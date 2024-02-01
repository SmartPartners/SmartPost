using SmartPost.Domain.Configurations;
using SmartPost.Service.DTOs.StockProducts;

namespace SmartPost.Service.Interfaces.StockProducts;

public interface IStockProductService
{
    Task<bool> DeleteAsymc(long id);
    Task<StockProductsForResultDto> GetByIdAsync(long id);
    Task<IEnumerable<StockProductsForResultDto>> GetAllAsync(PaginationParams @params);
    Task<StockProductsForResultDto> UpdateAsync(long id, StockProductForUpdateDto updateDto);
    //Task<StockProductsForResultDto> CreateAsync(StockProductForCreationDto createDto);
    Task<StockProductsForResultDto> AddQuentityToStockProduct(long id, decimal quantity);
}
