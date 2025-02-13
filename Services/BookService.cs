using Library_Management_System.Models;
using LibraryManagementSystem.Repositories;

namespace LibraryManagementSystem.Services
{
    public class BookService

        
    {

        // DI of IBook repo 
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // interacts with the book repository to add and save the books
        public async Task AddBookAsync(Book book) => await _bookRepository.Add(book);

        // interacts with the book repository to search books using a key word they choose to use
        public async Task<IEnumerable<Book>> SearchBooksAsync(string word) => await _bookRepository.SearchBooksAsync(word);

        // getting books in alphabetical order
        public async Task<IEnumerable<Book>> GetBooksAlphabeticallyAsync() => await _bookRepository.GetBooksAlphabeticallyAsync();

        public async Task<IEnumerable<Book>> GetBookCopies(int copies) => await _bookRepository.GetBooksByCopiesAsync(copies);






    }
}
