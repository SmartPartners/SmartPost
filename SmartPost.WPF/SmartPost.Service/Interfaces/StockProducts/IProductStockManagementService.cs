using SmartPost.Service.DTOs.StockProducts;

namespace SmartPost.Service.Interfaces.StockProducts;

public interface IProductStockManagementService
{
    public Task<bool> MoveProductToStockAsync(long id, long userId, decimal quantityToMove);
    public Task<IEnumerable<StockProductsForResultDto>> RetrieveAllWithDateTimeAsync(long userId, DateTime startDate, DateTime endDate);

}
