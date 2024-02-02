using SmartPost.DataAccess.Interfaces;
using SmartPost.DataAccess.Interfaces.Barnds;
using SmartPost.DataAccess.Interfaces.CancelOrders;
using SmartPost.DataAccess.Interfaces.Cards;
using SmartPost.DataAccess.Interfaces.Categories;
using SmartPost.DataAccess.Interfaces.InventoryLists;
using SmartPost.DataAccess.Interfaces.Partners;
using SmartPost.DataAccess.Interfaces.Products;
using SmartPost.DataAccess.Interfaces.StockProducts;
using SmartPost.DataAccess.Interfaces.Users;
using SmartPost.DataAccess.Repositories;
using SmartPost.DataAccess.Repositories.Brands;
using SmartPost.DataAccess.Repositories.CancelOrders;
using SmartPost.DataAccess.Repositories.Cards;
using SmartPost.DataAccess.Repositories.Categories;
using SmartPost.DataAccess.Repositories.InventoryLists;
using SmartPost.DataAccess.Repositories.Partners;
using SmartPost.DataAccess.Repositories.Products;
using SmartPost.DataAccess.Repositories.StockProducts;
using SmartPost.DataAccess.Repositories.Users;
using SmartPost.Service.Interfaces.Brands;
using SmartPost.Service.Interfaces.CancelOrders;
using SmartPost.Service.Interfaces.Cards;
using SmartPost.Service.Interfaces.Categories;
using SmartPost.Service.Interfaces.InventoryLists;
using SmartPost.Service.Interfaces.Partners;
using SmartPost.Service.Interfaces.Products;
using SmartPost.Service.Interfaces.StockProducts;
using SmartPost.Service.Interfaces.Users;
using SmartPost.Service.Services.Brands;
using SmartPost.Service.Services.CancelOrders;
using SmartPost.Service.Services.Cards;
using SmartPost.Service.Services.Categories;
using SmartPost.Service.Services.InventoryLists;
using SmartPost.Service.Services.Partners;
using SmartPost.Service.Services.Products;
using SmartPost.Service.Services.StockProducts;
using SmartPost.Service.Services.Users;

namespace SmartPost.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<ICancelOrderRepository, CancelOrderRepository>();
            services.AddScoped<IInventoryListRepository, InventoryListRepository>();
            services.AddScoped<IPartnerRepository, PartnerRepository>();
            services.AddScoped<IPartnerProductRepository, PartnerProductRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<ICancelOrderService, CancelOrderService>();
            services.AddScoped<IInventoryListService, InventoryListService>();
            services.AddScoped<IProductStockManagementService, ProductStockManagementService>();
            services.AddScoped<ICardManagementService, CardManagementService>();
            services.AddScoped<ICanceledProductsService, CanceledProductesService>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IBrandRepository, BrandRepository>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IPartnerService, PartnerService>();

            services.AddScoped<IStockProductRepository, StockProductRepository>();
            services.AddScoped<IStockProductService, StockProductService>();
        }
    }
}
