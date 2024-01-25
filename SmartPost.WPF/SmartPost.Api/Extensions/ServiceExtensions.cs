using SmartPost.DataAccess.Interfaces;
using SmartPost.DataAccess.Interfaces.Users;
using SmartPost.DataAccess.Repositories;
using SmartPost.DataAccess.Repositories.Users;
using SmartPost.Service.Interfaces.Users;
using SmartPost.Service.Services.Users;

namespace SmartPost.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IUserRepository, UserRepository>();


            services.AddScoped<IUserService, UserService>();
        }
    }
}
