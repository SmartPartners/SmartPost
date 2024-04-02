using System.Windows;
using SmartPost.Desktop.Windows;
using SmartPost.DataAccess.Data;
using SmartPost.Service.Mappers;
using Microsoft.EntityFrameworkCore;
using SmartPost.Service.Services.Cards;
using SmartPost.Service.Services.Users;
using SmartPost.Service.Services.Brands;
using SmartPost.Service.Interfaces.Cards;
using SmartPost.Service.Interfaces.Users;
using SmartPost.Service.Interfaces.Brands;
using SmartPost.Service.Services.Partners;
using SmartPost.Service.Services.Products;
using SmartPost.Service.Services.Categories;
using SmartPost.Service.Interfaces.Products;
using SmartPost.DataAccess.Interfaces.Users;
using SmartPost.Service.Interfaces.Partners;
using SmartPost.DataAccess.Interfaces.Cards;
using SmartPost.DataAccess.Interfaces.Barnds;
using SmartPost.DataAccess.Repositories.Cards;
using SmartPost.Service.Interfaces.Categories;
using SmartPost.DataAccess.Repositories.Users;
using SmartPost.Service.Services.CancelOrders;
using SmartPost.DataAccess.Interfaces.Products;
using Microsoft.Extensions.DependencyInjection;
using SmartPost.DataAccess.Interfaces.Partners;
using SmartPost.DataAccess.Repositories.Brands;
using SmartPost.Service.Services.StockProducts;
using SmartPost.Service.Interfaces.CancelOrders;
using SmartPost.Service.Services.InventoryLists;
using SmartPost.DataAccess.Interfaces.Categories;
using SmartPost.DataAccess.Repositories.Partners;
using SmartPost.Service.Interfaces.StockProducts;
using SmartPost.DataAccess.Repositories.Products;
using SmartPost.Service.Interfaces.InventoryLists;
using SmartPost.DataAccess.Repositories.Categories;
using SmartPost.DataAccess.Interfaces.CancelOrders;
using SmartPost.DataAccess.Interfaces.StockProducts;
using SmartPost.DataAccess.Interfaces.InventoryLists;
using SmartPost.DataAccess.Repositories.CancelOrders;
using SmartPost.DataAccess.Repositories.StockProducts;
using SmartPost.DataAccess.Repositories.InventoryLists;

namespace SmartPost.Desktop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        IServiceCollection services = new ServiceCollection();

        // database 
        services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql("Host = localhost; Port=5432; Database = DbSmartPost; UserId=postgres; Password=root;"));

        // mapper
        services.AddAutoMapper(typeof(MapperProfile));

        // Serivices
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICardService, CardService>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<IPartnerService, PartnerService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICancelOrderService, CancelOrderService>();
        services.AddScoped<IStockProductService, StockProductService>();
        services.AddScoped<IInventoryListService, InventoryListService>();
        services.AddScoped<ICardManagementService, CardManagementService>();
        services.AddScoped<IPartnerProductService, PartnerProductService>();
        services.AddScoped<ICanceledProductsService, CanceledProductesService>();
        services.AddScoped<IProductStockManagementService, ProductStockManagementService>();
        
        // Repositories
        services.AddScoped<ICardRepository, CardRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<IPartnerRepository, PartnerRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICancelOrderRepository, CancelOrderRepository>();
        services.AddScoped<IStockProductRepository, StockProductRepository>();
        services.AddScoped<IInventoryListRepository, InventoryListRepository>();
        services.AddScoped<IPartnerProductRepository, PartnerProductRepository>();

        var serviceProvider = services.BuildServiceProvider();

        new SaleWindow(serviceProvider).Show();
    }

}

