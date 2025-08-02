using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Book
    {
        private static int nextId = 1; 
        public int id { get; set; }
        public string title { get; set; }
        public string author { get; set; }
        public bool available { get; set; }

        public Book(string title, string author, bool available)
        {
            this.id = nextId++;
            this.title = title;
            this.author = author;
            this.available = available;
        }

        public bool IsAvailable()
        {
            return available;
        }

        public void NotAvailable()
        {
            available = false;
        }
    }
}