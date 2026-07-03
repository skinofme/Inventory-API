using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Inventory.Application.Features.Commands.CreateStockItem;
using Inventory.Application.Features.Commands.Warehouses.ActivateWarehouse;
using Inventory.Application.Features.Commands.Warehouses.CreateWarehouse;
using Inventory.Application.Features.Commands.Warehouses.DeactiveWarehouse;
using Inventory.Application.Features.Queries.GetStockItemById;
using Inventory.Application.Features.Queries.Warehouses.GetWarehouseById;
using Inventory.Application.Features.Queries.Warehouses.GetWarehouses;

namespace Inventory.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
		{
			// Warehouses.
			services.AddScoped<CreateWarehouseCommandHandler>();
			services.AddScoped<GetWarehouseByIdQueryHandler>();
			services.AddScoped<GetWarehousesQueryHandler>();
			services.AddScoped<DeactivateWarehouseCommandHandler>();
			services.AddScoped<ActivateWarehouseCommandHandler>();

			// StockItems.
			services.AddScoped<GetStockItemByIdQueryHandler>();
			services.AddScoped<CreateStockItemCommandHandler>();

			return services;
		}
	}
}
