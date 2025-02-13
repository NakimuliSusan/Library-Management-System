using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Library_Management_System.Data;
using Library_Management_System.Models;
using Library_Management_System.Repositories;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Services;
using Library_Management_System.Services;
using static System.Reflection.Metadata.BlobBuilder;


namespace Library_Management_System
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var context = new AppDbContext();


            IBookRepository bookRepository = new BookRepository(context);
            var bookService = new BookService(bookRepository);

            IMemberRepository memberRepository = new MemberRepository(context);
            var memberService = new MemberService(memberRepository);

            while (true)
            {
                Console.WriteLine("\nLibrary Management System");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Search Books");
                Console.WriteLine("3. View Books (Alphabetical Order)");
                Console.WriteLine("4. Search books with a certain number of copies");
                Console.WriteLine("5. Add Member");
                Console.WriteLine("6. Search Member");
                Console.WriteLine("7. Search Recent Members");
                Console.WriteLine("8. Exit");
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
                        await SearchBooksWithCertainCopies(bookService);
                        break;

                    case "5":
                        await AddMembers(memberService);
                        break;

                    case "6":
                        await SearchMembersAsync(memberService);
                        break;
                    case "7":
                        await SearchRecentMembersWithinMonths(memberService);
                        break;

                    case "8":
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
                    Console.WriteLine("Value cannot be empty. Please enter a valid input.");
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

        //Search books with a certain number of copies
        static async Task SearchBooksWithCertainCopies(BookService bookService)
        {
            Console.Write("Enter number of book copies available: ");

            if (!int.TryParse(Console.ReadLine(), out int copies) || copies <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a valid number greater than 0.");
                return;
            }

            var books = await bookService.GetBookCopies(copies);

            if (books.Any())
            {
                Console.WriteLine("\nBooks Found:");
                foreach (var book in books)
                {
                    Console.WriteLine($"Title: {book.Title} | Author: {book.Author} | Copies Available: {book.copiesAvailable} | ISBN: {book.ISBN}");
                }
            }
            else
            {
                Console.WriteLine("No books found with the specified number of copies.");
            }
        }



        // async method to add members
        static async Task AddMembers(MemberService memberService)
        {
            string name = GetValidatedInput("Enter member name: ");

            string gender = GetValidatedInput("Enter member gender: ");

            DateTime joindate = DateTime.UtcNow;

            var member = new Member { Name = name, Gender = gender, dateJoined = joindate };

            await memberService.AddMemberAsync(member);

            Console.WriteLine("Member has been added successfully");

        }

        // search  members using either properties
        static async Task SearchMembersAsync(MemberService memberService)
        {
            Console.WriteLine("\n--- Search Members ---");

            string? name = null, gender = null;
            DateTime? dateJoined = null;

            while (true)
            {
                Console.WriteLine("\nSelect filter options:");
                Console.WriteLine("1. Name");
                Console.WriteLine("2. Gender");
                Console.WriteLine("3. Date Joined");
                Console.WriteLine("4. Search");
                Console.WriteLine("5. Cancel");


                string choice = GetValidatedInput("Choose an option: ");

                switch (choice)
                {
                    case "1":
                        name = GetValidatedInput("Enter Name: ");
                        break;

                    case "2":
                        gender = GetValidatedInput("Enter Gender (Male/Female: ");
                        break;

                    case "3":
                        string dateInput = GetValidatedInput("Enter Date Joined (YYYY-MM-DD): ");
                   
                        if (DateTime.TryParse(dateInput, out DateTime parsedDate))
                        {
                            dateJoined = parsedDate;
                        }
                        else
                        {
                            Console.WriteLine("Invalid date format. Please use YYYY-MM-DD.");
                        }
                        break;

                    case "4":
                        var members = await memberService.SearchMembersAsync(name, gender, dateJoined);
                        if (members.Any())
                        {
                            Console.WriteLine("\nSearch Results:");
                            foreach (var member in members)
                            {
                                Console.WriteLine($"ID: {member.Id} | Name: {member.Name} | Gender: {member.Gender} | Date Joined: {member.dateJoined:yyyy-MM-dd}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No members found matching the criteria.");
                        }
                        return; 

                    case "5":
                        Console.WriteLine("Search canceled.");
                        return;

                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        static async Task SearchRecentMembersWithinMonths(MemberService memberService)
        {
            Console.Write("Enter number of months: ");

            if (!int.TryParse(Console.ReadLine(), out int months) || months <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a valid number greater than 0.");
                return;
            }

            var members = await memberService.GetRecentMembersAsync(months);

            if (members.Any())
            {
                Console.WriteLine("Members Found:");
                foreach (var member in members)
                {
                    Console.WriteLine($"Id: {member.Id} | Name: {member.Name} | Gender: {member.Gender} | DateJoined: {member.dateJoined}");
                }
            }
        }

    }
}
