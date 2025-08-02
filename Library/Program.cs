using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Book> books = new List<Book>();
            List<User> users = new List<User>();
            List<BookLoan> bookLoans = new List<BookLoan>();

            while (true)
            {
                Console.WriteLine("1 - customer");
                Console.WriteLine("2 - employee");
                Console.WriteLine("3 - exit");
                int userChoice = int.Parse(Console.ReadLine());

                if (userChoice == 1)
                {
                    Console.Write("enter your name: ");
                    string userName = Console.ReadLine();

                    Console.Write("enter your email: ");
                    string userEmail = Console.ReadLine();

                    User user = users.FirstOrDefault(u => u.email == userEmail);

                    if (user == null)
                    {
                        user = new User(userName, userEmail);
                        users.Add(user);
                    }

                    int customerChoice = 0;
                    while (customerChoice != 6)
                    {
                        Console.WriteLine("1 - search a book by title");
                        Console.WriteLine("2 - list available books");
                        Console.WriteLine("3 - borrow a book");
                        Console.WriteLine("4 - return a book");
                        Console.WriteLine("5 - view my borrowed books");
                        Console.WriteLine("6 - exit");
                        customerChoice = int.Parse(Console.ReadLine());

                        switch (customerChoice)
                        {
                            case 1:
                                Console.Write("enter the book title: ");
                                string bookTitle = Console.ReadLine();

                                Book bookFound = null;

                                foreach (var book in books)
                                {
                                    if (book.title.Equals(bookTitle, StringComparison.OrdinalIgnoreCase))
                                    {
                                        bookFound = book;
                                        break;
                                    }
                                }

                                if (bookFound != null)
                                {
                                    Console.WriteLine("id: {0}, book found: {1} by {2}", bookFound.id, bookFound.title, bookFound.author);
                                }

                                else
                                {
                                    Console.WriteLine("book not found");
                                }

                                break;

                            case 2:
                                Console.WriteLine("available books");

                                foreach (var book in books.Where(b => b.IsAvailable()))
                                {
                                    Console.WriteLine("id: {0}, title: {1}, author: {2}", book.id, book.title, book.author);
                                }

                                break;

                            case 3:
                                Console.Write("enter the book id: ");
                                int borrowBookId = int.Parse(Console.ReadLine());

                                bool bookExists = false;

                                foreach (var book in books)
                                {
                                    if (book.id.Equals(borrowBookId))
                                    {
                                        if (book.IsAvailable() && users.Last().CanBorrow())
                                        {
                                            bookExists = true;

                                            book.NotAvailable();
                                            user.borrowedBooks.Add(new BookLoan(book, user, DateTime.Now));
                                            Console.WriteLine("loan made successfully");

                                            break;
                                        }
                                    }
                                }

                                if (!bookExists)
                                {
                                    Console.WriteLine("book not available or user cannot borrow");
                                }

                                break;

                            case 4:
                                Console.Write("enter the book id: ");
                                int returnBookId = int.Parse(Console.ReadLine());

                                BookLoan bookLoan = user.borrowedBooks.FirstOrDefault(b => b.book.id == returnBookId);

                                if (bookLoan != null && !bookLoan.IsReturned())
                                {
                                    bookLoan.RegisterReturn();
                                    bookLoan.book.available = true;
                                    Console.WriteLine("book returned successfully");
                                }

                                else
                                {
                                    Console.WriteLine("book not found or already returned");
                                }

                                break;

                            case 5:
                                Console.WriteLine("{0}'s borrowed books", user.name);

                                foreach (var loan in user.borrowedBooks)
                                {
                                    Console.WriteLine("id: {0}, book: {1}, loan date: {2}, returned: {3}",
                                    loan.id, loan.book.title, loan.loanDate, loan.IsReturned() ? "yes" : "no");
                                }

                                break;

                            case 6:
                                break;

                            default:
                                Console.WriteLine("invalid choice");
                                break;
                        }
                    }
                }

                else if (userChoice == 2)
                {
                    int employeeChoice = 0;
                    while (employeeChoice != 7)
                    {
                        Console.WriteLine("1 - add new book");
                        Console.WriteLine("2 - remove book from system");
                        Console.WriteLine("3 - edit book information");
                        Console.WriteLine("4 - list all books (including borrowed)");
                        Console.WriteLine("5 - view all customers and their borrowings");
                        Console.WriteLine("6 - view borrowing history");
                        Console.WriteLine("7 - exit");
                        employeeChoice = int.Parse(Console.ReadLine());

                        switch (employeeChoice)
                        {
                            case 1:
                                Console.Write("enter the books title: ");
                                string bookTitle = Console.ReadLine();

                                Console.Write("enter the books author: ");
                                string bookAuthor = Console.ReadLine();

                                // logic to add a new book

                                break;

                            case 2:
                                Console.Write("enter the book id: ");
                                int removeBookId = int.Parse(Console.ReadLine());

                                // logic to remove a book by id

                                break;

                            case 3:
                                Console.Write("enter the book id: ");
                                int editBookId = int.Parse(Console.ReadLine());

                                // logic to edit book information by id

                                break;

                            case 4:
                                Console.WriteLine("all books (including borrowed)");

                                // logic to list all books including borrowed ones

                                break;

                            case 5:
                                Console.WriteLine("all customers and their borrowings");

                                // logic to view all customers and their borrowings

                                break;

                            case 6:
                                Console.WriteLine("borrowing history");

                                // logic to view borrowing history

                                break;

                            case 7:
                                break;

                            default:
                                Console.WriteLine("invalid choice");
                                break;
                        }
                    }
                }

                else if (userChoice == 3)
                {
                    Console.WriteLine("exiting...");
                    Thread.Sleep(1000);
                    return;
                }

                else
                {
                    Console.WriteLine("invalid choice");
                }

                /* problems
                1. the code does not handle invalid inputs gracefully, which can lead to exceptions
                2. the code shows available books/borrowed books even if there are no books in system */
            }
        }
    }
}