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

                if (!int.TryParse(Console.ReadLine(), out int userChoice) || (userChoice < 1 || userChoice > 3))
                {
                    Console.WriteLine("invalid choice, please try again");
                    continue;
                }

                if (userChoice == 1)
                {
                    Console.Write("enter your name: ");
                    string userName = Console.ReadLine();

                    Console.Write("enter your email: ");
                    string userEmail = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(userEmail))
                    {
                        Console.WriteLine("invalid name or email, please try again");
                        continue;
                    }

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

                        if (!int.TryParse(Console.ReadLine(), out customerChoice))
                        {
                            Console.WriteLine("invalid choice, please try again");
                            continue;
                        }

                        switch (customerChoice)
                        {
                            case 1:
                                Console.Write("enter the book title: ");
                                string bookTitle = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(bookTitle))
                                {
                                    Console.WriteLine("invalid title, please try again");
                                    continue;
                                }

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
                                if (books.Count > 0)
                                {
                                    Console.WriteLine("available books");

                                    foreach (var book in books.Where(b => b.IsAvailable()))
                                    {
                                        Console.WriteLine("id: {0}, title: {1}, author: {2}", book.id, book.title, book.author);
                                    }

                                    break;
                                }

                                else
                                {
                                    Console.WriteLine("no books available");
                                    break;
                                }

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

                                        else if (book.available == false)
                                        {
                                            Console.WriteLine("book is not available for borrowing");
                                        }

                                        else if (!user.CanBorrow())
                                        {
                                            Console.WriteLine("user cannot borrow more books");
                                        }
                                    }
                                }

                                if (!bookExistsBorrow)
                                {
                                    Console.WriteLine("book not found");
                                }

                                break;

                            case 4:
                                Console.Write("enter the book id: ");

                                if (!int.TryParse(Console.ReadLine(), out int returnBookId))
                                {
                                    Console.WriteLine("invalid id, please try again");
                                    continue;
                                }

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

                                    else if (loan.book.id.Equals(returnBookId) && loan.IsReturned())
                                    {
                                        Console.WriteLine("book already returned");
                                        bookExistsReturn = true;
                                        break;
                                    }
                                }

                                if (!bookExistsReturn)
                                {
                                    Console.WriteLine("book not found");
                                }

                                break;

                            case 5:
                                if (user.borrowedBooks.Count > 0)
                                {
                                    Console.WriteLine("{0}'s borrowed books", user.name);

                                    foreach (var loan in user.borrowedBooks)
                                    {
                                        if (!loan.book.available)
                                        {
                                            Console.WriteLine("id: {0}, book: {1}, loan date: {2}", loan.id, loan.book.title, loan.loanDate);
                                        }
                                    }

                                    break;
                                }

                                else
                                {
                                    Console.WriteLine("no borrowed books");
                                    break;
                                }

                            case 6:
                                break;

                            default:
                                Console.WriteLine("invalid choice, please try again");
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

                        if (!int.TryParse(Console.ReadLine(), out employeeChoice))
                        {
                            Console.WriteLine("invalid choice, please try again");
                            continue;
                        }

                        switch (employeeChoice)
                        {
                            case 1:
                                Console.Write("enter the books title: ");
                                string bookTitle = Console.ReadLine();

                                Console.Write("enter the books author: ");
                                string bookAuthor = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(bookTitle) || string.IsNullOrWhiteSpace(bookAuthor))
                                {
                                    Console.WriteLine("invalid title or author, please try again");
                                    continue;
                                }

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

                                if (!int.TryParse(Console.ReadLine(), out int removeBookId))
                                {
                                    Console.WriteLine("invalid id, please try again");
                                    continue;
                                }

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

                                if (!int.TryParse(Console.ReadLine(), out int editBookId))
                                {
                                    Console.WriteLine("invalid id, please try again");
                                    continue;
                                }

                                bool bookExistsEdit = false;

                                foreach (var book in books)
                                {
                                    if (book.id.Equals(editBookId))
                                    {
                                        Console.WriteLine("enter which one you want to update");
                                        Console.WriteLine("1 - title");
                                        Console.WriteLine("2 - author");
                                        int editChoice = Console.Read();

                                        switch (editChoice)
                                        {
                                            case 1:
                                                Console.Write("enter new title: ");
                                                book.title = Console.ReadLine();

                                                if (string.IsNullOrWhiteSpace(book.title))
                                                {
                                                    Console.WriteLine("invalid title, please try again");
                                                    continue;
                                                }

                                                else
                                                {
                                                    Console.WriteLine("title updated successfully");
                                                }

                                                break;

                                            case 2:
                                                Console.Write("enter new author: ");
                                                book.author = Console.ReadLine();

                                                if (string.IsNullOrWhiteSpace(book.author))
                                                {
                                                    Console.WriteLine("invalid author, please try again");
                                                    continue;
                                                }

                                                else
                                                {
                                                    Console.WriteLine("author updated successfully");
                                                }
 
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
                                if (books.Count > 0)
                                {
                                    Console.WriteLine("all books (including borrowed)");

                                    foreach (var book in books)
                                    {
                                        Console.WriteLine("id: {0}, title: {1}, author: {2}, available: {3}", book.id, book.title, book.author, book.IsAvailable() ? "yes" : "no");
                                    }
                                }

                                else
                                {
                                    Console.WriteLine("no books in the system");
                                }

                                break;

                            case 5:
                                if (users.Count > 0)
                                {
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
                                }

                                else
                                {
                                    Console.WriteLine("no customers in the system");
                                    break;
                                }

                            case 6:
                                Console.WriteLine("enter the costumer name: ");
                                string customerName = Console.ReadLine();

                                if (string.IsNullOrWhiteSpace(customerName))
                                {
                                    Console.WriteLine("invalid name, please try again");
                                    continue;
                                }

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
                                Console.WriteLine("invalid choice, please try again");
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
            }
        }
    }
}