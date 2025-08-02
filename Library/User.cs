using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class User
    {
        private static int nextId = 1;
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public List<BookLoan> borrowedBooks { get; set; }

        public User(string name, string email)
        {
            this.name = name;
            this.email = email;
            borrowedBooks = new List<BookLoan>();
        }

        public bool CanBorrow()
        {
            return borrowedBooks.Count < 3;
        }
    }
}