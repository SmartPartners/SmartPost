using SmartPost.DataAccess.Data;
using SmartPost.DataAccess.Interfaces.StockProducts;
using SmartPost.Domain.Entities.StokProducts;

namespace SmartPost.DataAccess.Repositories.StockProducts;

public class StockProductRepository : Repository<StokProduct>, IStockProductRepository
{
    public StockProductRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
