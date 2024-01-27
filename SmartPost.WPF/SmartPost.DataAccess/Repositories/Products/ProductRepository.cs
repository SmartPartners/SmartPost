using SmartPost.DataAccess.Data;
using SmartPost.DataAccess.Interfaces.Products;
using SmartPost.Domain.Entities.StorageProducts;

namespace SmartPost.DataAccess.Repositories.Products;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}
