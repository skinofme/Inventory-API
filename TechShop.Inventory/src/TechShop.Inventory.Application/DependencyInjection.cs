using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechShop.Inventory.Application.Features.Queries.GetStockItemById;

namespace TechShop.Inventory.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<GetStockItemByIdQueryHandler>();
			return services;
		}
	}
}
