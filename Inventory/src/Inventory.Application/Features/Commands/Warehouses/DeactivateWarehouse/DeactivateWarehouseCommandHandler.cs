using Inventory.Application.Exceptions;
using Inventory.Core.Interfaces;
using Inventory.Core.Interfaces.Repositories;

namespace Inventory.Application.Features.Commands.Warehouses.DeactiveWarehouse
{
	public class DeactivateWarehouseCommandHandler
	{
		private readonly IWarehouseRepository _repository;
		private readonly IUnitOfWork _unitOfWork;

		public DeactivateWarehouseCommandHandler(IWarehouseRepository repository, IUnitOfWork unitOfWork)
		{
			_repository = repository;
			_unitOfWork = unitOfWork;
		}

		public async Task Handle(DeactivateWarehouseCommand command, CancellationToken cancellationToken)
		{
			var warehouse = await _repository.GetByIdAsync(command.IdWarehouse, cancellationToken);

			if (warehouse == null) throw new WarehouseNotFoundException(command.IdWarehouse);

			warehouse.Deactivate();

			await _repository.UpdateAsync(warehouse, cancellationToken);

			await _unitOfWork.SaveAsync(cancellationToken);
		}
	}
}
