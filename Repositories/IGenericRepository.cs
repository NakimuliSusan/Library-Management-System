namespace Library_Management_System.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);


    }
}