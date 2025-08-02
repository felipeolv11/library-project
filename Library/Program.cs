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

                                List<Book> foundBooks = new List<Book>();

                                foreach (var book in books)
                                {
                                    if (book.title.Equals(bookTitle))
                                    {
                                        foundBooks.Add(book);
                                    }
                                }

                                if (foundBooks.Count > 0)
                                {
                                    foreach (var bookFound in foundBooks)
                                    {
                                        Console.WriteLine("id: {0}, book found: {1} by {2}", bookFound.id, bookFound.title, bookFound.author);
                                    }
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

                                bool bookExistsBorrow = false;

                                foreach (var book in books)
                                {
                                    if (book.id.Equals(borrowBookId))
                                    {
                                        if (book.IsAvailable() && users.Last().CanBorrow())
                                        {
                                            bookExistsBorrow = true;

                                            book.available = false;
                                            user.borrowedBooks.Add(new BookLoan(user, book, DateTime.Now));
                                            Console.WriteLine("loan made successfully");

                                            break;
                                        }
                                    }
                                }

                                if (!bookExistsBorrow)
                                {
                                    Console.WriteLine("book not available or user cannot borrow"); // could be more specific
                                }

                                break;

                            case 4:
                                Console.Write("enter the book id: ");
                                int returnBookId = int.Parse(Console.ReadLine());

                                bool bookExistsReturn = false;

                                foreach (var loan in user.borrowedBooks)
                                {
                                    if (loan.book.id.Equals(returnBookId) && !loan.IsReturned())
                                    {
                                        loan.RegisterReturn();
                                        loan.book.available = true;
                                        Console.WriteLine("book returned successfully");

                                        bookExistsReturn = true;
                                        break;
                                    }
                                }

                                if (!bookExistsReturn)
                                {
                                    Console.WriteLine("book not found or already returned"); // could be more specific v2
                                }

                                break;

                            case 5:
                                Console.WriteLine("{0}'s borrowed books", user.name);

                                foreach (var loan in user.borrowedBooks)
                                {
                                    if(!loan.book.available)
                                    {
                                        Console.WriteLine("id: {0}, book: {1}, loan date: {2}", loan.id, loan.book.title, loan.loanDate);
                                    }
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

                                bool bookExistsAdd = false;

                                foreach (var book in books)
                                {
                                    if (book.title.Equals(bookTitle) && book.author.Equals(bookAuthor))
                                    {
                                        Console.WriteLine("book already exists");
                                        bookExistsAdd = true;
                                        break;
                                    }
                                }

                                if (!bookExistsAdd)
                                {
                                    Book newBook = new Book(bookTitle, bookAuthor, true);
                                    books.Add(newBook);

                                    Console.WriteLine("book added successfully with id: {0}", newBook.id);
                                }

                                break;

                            case 2:
                                Console.Write("enter the book id: ");
                                int removeBookId = int.Parse(Console.ReadLine());

                                bool bookExistsRemove = false;

                                foreach (var book in books)
                                {
                                    if (book.id.Equals(removeBookId))
                                    {
                                        books.Remove(book);

                                        Console.WriteLine("book removed successfully");
                                        bookExistsRemove = true;
                                        break;
                                    }
                                }

                                if (!bookExistsRemove)
                                {
                                    Console.WriteLine("book not found");
                                }

                                break;

                            case 3:
                                Console.Write("enter the book id: ");
                                int editBookId = int.Parse(Console.ReadLine());

                                bool bookExistsEdit = false;

                                foreach (var book in books)
                                {
                                    if (book.id.Equals(editBookId))
                                    {
                                        Console.WriteLine("enter which one you want to update");
                                        Console.WriteLine("1 - title");
                                        Console.WriteLine("2 - author");
                                        int editChoice = int.Parse(Console.ReadLine());

                                        switch (editChoice)
                                        {
                                            case 1:
                                                Console.Write("enter new title: ");
                                                book.title = Console.ReadLine();

                                                Console.WriteLine("title updated successfully");
                                                break;

                                            case 2:
                                                Console.Write("enter new author: ");
                                                book.author = Console.ReadLine();

                                                Console.WriteLine("author updated successfully");
                                                break;

                                            default:
                                                Console.WriteLine("invalid choice");
                                                break;
                                        }

                                        bookExistsRemove = true;
                                        break;
                                    }
                                }

                                if (!bookExistsEdit)
                                {
                                    Console.WriteLine("book not found");
                                }

                                break;

                            case 4:
                                Console.WriteLine("all books (including borrowed)");

                                foreach (var book in books)
                                {
                                    Console.WriteLine("id: {0}, title: {1}, author: {2}, available: {3}", book.id, book.title, book.author, book.IsAvailable() ? "yes" : "no");
                                }

                                break;

                            case 5:
                                Console.WriteLine("all customers and their borrowings");

                                foreach (var user in users)
                                {
                                    Console.WriteLine("customer: {0}, email: {1}", user.name, user.email);

                                    if (user.borrowedBooks.Count > 0)
                                    {
                                        Console.WriteLine("borrowed books:");
                                        foreach (var loan in user.borrowedBooks)
                                        {
                                            Console.WriteLine("id: {0}, book: {1}, loan date: {2}, returned: {3}", loan.id, loan.book.title, loan.loanDate, loan.IsReturned() ? "yes" : "no");
                                        }
                                    }

                                    else
                                    {
                                        Console.WriteLine("no borrowed books");
                                    }
                                }

                                break;

                            case 6:
                                Console.WriteLine("enter the costumer name: ");
                                string customerName = Console.ReadLine();

                                List<User> customersSameName = new List<User>();

                                foreach (var user in users)
                                {
                                    if (user.name.Equals(customerName))
                                    {
                                        customersSameName.Add(user);
                                    }
                                }

                                if (customersSameName.Count > 0)
                                {
                                    foreach (var customer in customersSameName)
                                    {
                                        Console.WriteLine("customer: {0}, email: {1}", customer.name, customer.email);
                                        if (customer.borrowedBooks.Count > 0)
                                        {
                                            Console.WriteLine("borrowing history:");
                                            foreach (var loan in customer.borrowedBooks)
                                            {
                                                Console.WriteLine("id: {0}, book: {1}, loan date: {2}, returned: {3}", loan.id, loan.book.title, loan.loanDate, loan.IsReturned() ? "yes" : "no");
                                            }
                                        }

                                        else
                                        {
                                            Console.WriteLine("no borrowed books");
                                        }
                                    }
                                }

                                else
                                {
                                    Console.WriteLine("customer not found");
                                }

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