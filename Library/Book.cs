using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Book
    {
        private int td { get; set; }
        private string title { get; set; }
        private string author { get; set; }
        private bool available { get; set; }

        public Book(int id, string title, string author, bool available)
        {
            this.td = id;
            this.title = title;
            this.author = author;
            this.available = available;
        }

        public bool IsAvailable()
        {
        }
    }
}