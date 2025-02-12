using Library_Management_System.Data;
using Library_Management_System.Models;
using LibraryManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Repositories
{
   
        public class BookRepository : GenericRepository<Book>, IBookRepository
        {
            private readonly AppDbContext _context;

            public BookRepository(AppDbContext context) : base(context)
            {
                _context = context;
            }

            public async Task<IEnumerable<Book>> SearchBooksAsync(string word)
            {

                return await _context.Books
                    .Where(b => b.Title.Contains(word) || b.Author.Contains(word))
                    .ToListAsync();
            }

            public async Task<IEnumerable<Book>> GetBooksByCopiesAsync(int minCopies)
            {
                return await _context.Books.Where(b => b.copiesAvailable > minCopies).ToListAsync();
            }

            public async Task<IEnumerable<Book>> GetBooksAlphabeticallyAsync()
            {
                return await _context.Books.OrderBy(b => b.Title).ToListAsync();
            }
        }
    

}
