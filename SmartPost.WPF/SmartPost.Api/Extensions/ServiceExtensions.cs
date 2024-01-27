using SmartPost.DataAccess.Interfaces;
using SmartPost.DataAccess.Interfaces.CancelOrders;
using SmartPost.DataAccess.Interfaces.Cards;
using SmartPost.DataAccess.Interfaces.Categories;
using SmartPost.DataAccess.Interfaces.InventoryLists;
using SmartPost.DataAccess.Interfaces.Users;
using SmartPost.DataAccess.Repositories;
using SmartPost.DataAccess.Repositories.CancelOrders;
using SmartPost.DataAccess.Repositories.Cards;
using SmartPost.DataAccess.Repositories.Categories;
using SmartPost.DataAccess.Repositories.InventoryLists;
using SmartPost.DataAccess.Repositories.Users;
using SmartPost.Service.Interfaces.CancelOrders;
using SmartPost.Service.Interfaces.Cards;
using SmartPost.Service.Interfaces.Categories;
using SmartPost.Service.Interfaces.InventoryLists;
using SmartPost.Service.Interfaces.Users;
using SmartPost.Service.Services.CancelOrders;
using SmartPost.Service.Services.Cards;
using SmartPost.Service.Services.Categories;
using SmartPost.Service.Services.InventoryLists;
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

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<ICancelOrderService, CancelOrderService>();
            services.AddScoped<IInventoryListService, InventoryListService>();
            
        }
    }
}
