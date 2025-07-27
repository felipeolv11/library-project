using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class BookLoan
    {
        private int id { get; set; }
        private Book book { get; set; }
        private User user { get; set; }
        private DateTime loanDate { get; set; }
        private DateTime? returnDate { get; set; }

        public BookLoan(int id, Book book, User user, DateTime loanDate)
        {
            this.id = id;
            this.book = book;
            this.user = user;
            this.loanDate = loanDate;
            this.returnDate = null;
        }

        public bool IsReturned()
        {
        }

        public void RegisterReturn()
        {
        }
    }
}