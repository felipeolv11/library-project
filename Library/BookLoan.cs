using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class BookLoan
    {
        private static int nextId = 1;
        public int id { get; set; }
        public User user { get; set; }
        public Book book { get; set; }
        public DateTime loanDate { get; set; }
        public DateTime? returnDate { get; set; }

        public BookLoan(User user, Book book, DateTime loanDate)
        {
            this.id = nextId++;
            this.book = book;
            this.user = user;
            this.loanDate = loanDate;
            this.returnDate = null;
        }

        public bool IsReturned()
        {
            return returnDate.HasValue;
        }

        public void RegisterReturn()
        {
            this.returnDate = DateTime.Now;
        }
    }
}