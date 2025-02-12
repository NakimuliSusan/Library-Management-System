using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Library_Management_System.Data;
using Library_Management_System.Models;
using Library_Management_System.Repositories;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Services;


namespace Library_Management_System
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var context = new AppDbContext();
            IBookRepository bookRepository = new BookRepository(context);
            var bookService = new BookService(bookRepository);

            while (true)
            {
                Console.WriteLine("\nLibrary Management System");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Search Books");
                Console.WriteLine("3. View Books (Alphabetical Order)");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        await AddBookAsync(bookService);
                        break;

                    case "2":
                        await SearchBooksAsync(bookService);
                        break;

                    case "3":
                        await GetBooksAlphabeticallyAsync(bookService);
                        break;

                    case "4":
                        Console.WriteLine("Exiting...");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }


        // Helper method to handle input validation
        static string GetValidatedInput(string prompt)
        {
            string input;
            while (true)
            {
                Console.Write(prompt);
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine($"{prompt} cannot be empty. Please enter a valid input.");
                    continue;
                }

                break;
            }

            return input;
        }

        // async method to add a book
        static async Task AddBookAsync(BookService bookService)
        {
            
            string title = GetValidatedInput("Enter Book Title: ");
            string author = GetValidatedInput("Enter Author: ");
            string isbn = GetValidatedInput("Enter ISBN: ");

           
            Console.Write("Enter number of book copies available: ");
            int copies = int.TryParse(Console.ReadLine(), out int parsedCopies) ? parsedCopies : 0;

            if (copies == 0)
            {
                Console.WriteLine("Invalid input. Please enter valid numbers.");
                return;
            }

            var book = new Book { Title = title, Author = author, copiesAvailable = copies, ISBN = isbn };
            await bookService.AddBookAsync(book);

            Console.WriteLine($"{book.Title} has been added successfully");
       
        }

        // async method to search a book by a keyword
        static async Task SearchBooksAsync(BookService bookService)
        {
            string keyword = GetValidatedInput("Enter keyword to search: ");
 
            var books = await bookService.SearchBooksAsync(keyword);

            if (!books.Any())
            {
                Console.WriteLine("No books found.");
                return;
            }

            Console.WriteLine("\nSearch Results:");
            foreach (var book in books)
            {
                Console.WriteLine($"{book.ISBN}. {book.Title} by {book.Author}- {book.copiesAvailable} copies available");
            }
        }


        // async method order books alphabetically
        static async Task GetBooksAlphabeticallyAsync(BookService bookService)
        {
            var books = await bookService.GetBooksAlphabeticallyAsync();

            if (!books.Any())
            {
                Console.WriteLine("No books available.");
                return;
            }

            Console.WriteLine("\nBooks (Alphabetically Sorted):");
            foreach (var book in books)
            {
                Console.WriteLine($"{book.ISBN}. {book.Title} by {book.Author} - {book.copiesAvailable} copies available");
            }
        }
    }
}
