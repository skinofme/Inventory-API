using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechShop.Inventory.Application.Features.Commands.CreateStockItem;
using TechShop.Inventory.Application.Features.Commands.Warehouses.CreateWarehouse;
using TechShop.Inventory.Application.Features.Queries.GetStockItemById;
using TechShop.Inventory.Application.Features.Queries.Warehouses.GetWarehouseById;

namespace TechShop.Inventory.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<GetStockItemByIdQueryHandler>();
			services.AddScoped<CreateStockItemCommandHandler>();
			services.AddScoped<GetWarehouseByIdQueryHandler>();
			services.AddScoped<CreateWarehouseCommandHandler>();
			return services;
		}
	}
}
