using TechShop.Inventory.Application.Features.DTOs;
using TechShop.Inventory.Core.Entities;
using TechShop.Inventory.Core.Interfaces;
using TechShop.Inventory.Core.Interfaces.Repositories;

namespace TechShop.Inventory.Application.Features.Commands.Warehouses.CreateWarehouse
{
	public class CreateWarehouseCommandHandler
	{
		private readonly IWarehouseRepository _repository;
		private readonly IUnitOfWork _unitOfWork;
		public CreateWarehouseCommandHandler(IWarehouseRepository repository, IUnitOfWork unitOfWork)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
		}

		public async Task<WarehouseDto> Handle(CreateWarehouseCommand command, CancellationToken cancellationToken)
		{
			var warehouse = Warehouse.Create(command.Code, command.Name, command.Location);

			await _repository.AddAsync(warehouse, cancellationToken);

			await _unitOfWork.SaveAsync(cancellationToken);

			return new WarehouseDto(
				warehouse.IdWarehouse,
				warehouse.Code,
				warehouse.Name,
				warehouse.Location, 
				warehouse.IsActive
			);
		}

	}
}
