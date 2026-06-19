using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechShop.Inventory.Core.Interfaces;
using TechShop.Inventory.Core.Interfaces.Repositories;
using TechShop.Inventory.Infrastructure.Persistence;
using TechShop.Inventory.Infrastructure.Persistence.Context;
using TechShop.Inventory.Infrastructure.Persistence.Repositories;

namespace TechShop.Inventory.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<TechShopInventoryContext>(options 
				=> options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

			services.AddScoped<IStockItemRepository, StockItemRepository>();
			services.AddScoped<IWarehouseRepository, WarehouseRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			return services;
		}
	}
}
