namespace TechShop.Inventory.Core.Interfaces.Repositories
{
	public interface IRepository<T> where T : class
	{
		Task<List<T>> GetAllAsync();
		Task<T> GetByIdAsync(int id);
		Task AddAsync(T entity);
		Task EditAsync(T entity, DateTime now);
		Task<bool> DeleteAsync(int id);
	}
}
