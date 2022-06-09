namespace StockTrader.Domain.Services
{
    public interface IDataService<T>
    {
        /// <summary>
        /// Gets all items of type T.
        /// </summary>
        /// <returns>All items of type T.</returns>
        Task<IEnumerable<T>> GetAll();
        /// <summary>
        /// Gets an item of type T by its id.
        /// </summary>
        /// <param name="id">Id of the item.</param>
        /// <returns>The item of type T with the given id.</returns>
        Task<T> Get(int id);
        /// <summary>
        /// Adds an item of type T.
        /// </summary>
        /// <param name="entity">Entity of type T.</param>
        /// <returns>The added item of type T.</returns>
        Task<T> Create(T entity);
        /// <summary>
        /// Updates an item of type T.
        /// </summary>
        /// <param name="id">Id of the item to update.</param>
        /// <param name="entity">Entity of type T.</param>
        /// <returns>The updated item of type T.</returns>
        Task<T> Update(int id, T entity);
        /// <summary>
        /// Deletes an item of type T.
        /// </summary>
        /// <param name="id">Id of the item to delete.</param>
        /// <returns>Boolean indicating the success/failure.</returns>
        Task<bool> Delete(int id);
    }
}
