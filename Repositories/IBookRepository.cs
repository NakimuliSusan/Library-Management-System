using Library_Management_System.Models;
using Library_Management_System.Repositories;

namespace LibraryManagementSystem.Repositories
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<IEnumerable<Book>> SearchBooksAsync(string keyword);
        Task<IEnumerable<Book>> GetBooksByCopiesAsync(int minCopies);
        Task<IEnumerable<Book>> GetBooksAlphabeticallyAsync();
    }
}
