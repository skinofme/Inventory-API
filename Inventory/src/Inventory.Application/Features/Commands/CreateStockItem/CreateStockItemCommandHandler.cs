using Inventory.Application.Features.DTOs;
using Inventory.Core.Entities;
using Inventory.Core.Interfaces;
using Inventory.Core.Interfaces.Repositories;

namespace Inventory.Application.Features.Commands.CreateStockItem
{
	public class CreateStockItemCommandHandler
	{

		private readonly IStockItemRepository _repository;
		private readonly IUnitOfWork _unitOfWork;

		public CreateStockItemCommandHandler(IStockItemRepository repository, IUnitOfWork unitOfWork)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
		}

		public async Task<StockItemDto> Handle(CreateStockItemCommand command, CancellationToken cancellationToken)
		{
			// TODO: Add warehouse id validation before creating the stock item.
			// NOTE: Currently the method does not validate warehouse id existence.

			//var exists = await _warehouseRepository.ExistsAsync(command.IdWarehouse);
			//if (!exists) throw new Exception(command.IdWarehouse);

			var stockItem = StockItem.Create(command.IdWarehouse, command.Sku);
			await _repository.AddAsync(stockItem);

			await _unitOfWork.SaveAsync(cancellationToken);

			return new StockItemDto(
				stockItem.IdStockItem,
				stockItem.IdWarehouse,
				stockItem.Sku,
				stockItem.IsActive,
				stockItem.QuantityAvailable,
				stockItem.QuantityReserved,
				stockItem.QuantityTotal
			);
		}

	}
}
