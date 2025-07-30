using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

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

                switch (userChoice)
                {
                    case 1:
                        Console.WriteLine("1 - search a book by title");
                        Console.WriteLine("2 - list available books");
                        Console.WriteLine("3 - borrow a book");
                        Console.WriteLine("4 - return a book");
                        Console.WriteLine("5 - view my borrowed books");
                        Console.WriteLine("6 - exit");
                        int customerChoice = int.Parse(Console.ReadLine());

                        switch (customerChoice)
                        {
                            case 1:
                                Console.Write("enter the book name: ");
                                string bookTitle = Console.ReadLine();

                                // search logic for book by title

                                break;

                            case 2:
                                Console.WriteLine("available books");

                                // logic to list available books

                                break;

                            case 3:
                                Console.Write("enter the book id: ");
                                int bookId = int.Parse(Console.ReadLine());

                                // logic to borrow a book by id

                                break;

                            case 4:
                                Console.Write("enter the book id: ");
                                int returnBookId = int.Parse(Console.ReadLine());

                                // logic to return a book by id

                                break;

                            case 5:
                                Console.WriteLine("your borrowed books");

                                // logic to view borrowed books

                                break;

                            case 6:
                                return;

                            default:
                                Console.WriteLine("invalid choice");
                                break;  
                        }

                        break;

                    case 2:
                        Console.WriteLine("1 - add new book");
                        Console.WriteLine("2 - remove book from system");
                        Console.WriteLine("3 - edit book information");
                        Console.WriteLine("4 - list all books (including borrowed)");
                        Console.WriteLine("5 - view all customers and their borrowings");
                        Console.WriteLine("6 - view borrowing history");
                        Console.WriteLine("7 - exit");
                        int employeeChoice = int.Parse(Console.ReadLine());

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
                                return;

                            default:
                                Console.WriteLine("invalid choice");
                                break;
                        }

                        break;

                    case 3:
                        return;

                    default:
                        Console.WriteLine("invalid choice");
                        break;
                }
            }
        }
    }
}