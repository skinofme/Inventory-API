using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechShop.Inventory.Application.Features.Commands.CreateStockItem;
using TechShop.Inventory.Application.Features.Commands.Warehouses.CreateWarehouse;
using TechShop.Inventory.Application.Features.Queries.GetStockItemById;
using TechShop.Inventory.Application.Features.Queries.Warehouses.GetWarehouseById;
using TechShop.Inventory.Application.Features.Queries.Warehouses.GetWarehouses;

namespace TechShop.Inventory.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
		{
			// Warehouses.
			services.AddScoped<CreateWarehouseCommandHandler>();
			services.AddScoped<GetWarehouseByIdQueryHandler>();
			services.AddScoped<GetWarehousesQueryHandler>();

			// StockItems.
			services.AddScoped<GetStockItemByIdQueryHandler>();
			services.AddScoped<CreateStockItemCommandHandler>();

			return services;
		}
	}
}
