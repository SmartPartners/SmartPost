using SmartPost.Domain.Entities.StokProducts;
using SmartPost.Service.DTOs.StockProducts;

namespace SmartPost.Service.Interfaces.StockProducts;

public interface IProductStockManagementService
{
    Task<bool> MoveProductToStockAsync(long id, long userId, decimal quantityToMove);
    Task<IEnumerable<StockProductsForResultDto>> RetrieveAllWithDateTimeAsync(long userId, DateTime startDate, DateTime endDate);

}
