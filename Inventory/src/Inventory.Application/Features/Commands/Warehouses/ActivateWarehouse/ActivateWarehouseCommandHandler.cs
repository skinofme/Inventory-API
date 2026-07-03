using Inventory.Application.Exceptions;
using Inventory.Core.Interfaces;
using Inventory.Core.Interfaces.Repositories;

namespace Inventory.Application.Features.Commands.Warehouses.ActivateWarehouse
{
	public class ActivateWarehouseCommandHandler
	{
		private readonly IWarehouseRepository _repository;
		private readonly IUnitOfWork _unitOfWork;
		public ActivateWarehouseCommandHandler(IWarehouseRepository repository, IUnitOfWork unitOfWork)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
		}

		public async Task Handle(ActivateWarehouseCommand command, CancellationToken cancellationToken)
		{
			var warehouse = await _repository.GetByIdAsync(command.IdWarehouse, cancellationToken);

			if (warehouse == null) throw new WarehouseNotFoundException(command.IdWarehouse);

			warehouse.Activate();

			await _repository.UpdateAsync(warehouse, cancellationToken);

			await _unitOfWork.SaveAsync(cancellationToken);
		}

	}
}
