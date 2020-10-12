using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NNice.Business.Services;
using NNice.DAL;
using NNice.DAL.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DIExtensions
    {
        public static void RegisterDbRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository), typeof(Repository<NNiceContext>));
        }

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IComboService, ComboService>();
            services.AddScoped<IInventoryService, InventoryService>();
            services.AddScoped<IWorkShiftService, WorkShiftService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();

        }
    }
}
