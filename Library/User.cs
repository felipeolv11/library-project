using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class User
    {
        private int id { get; set; }
        private string name { get; set; }
        private string email { get; set; }

        public User(int id, string name, string email)
        {
            this.id = id;
            this.name = name;
            this.email = email;
        }

        public bool CanBorrow()
        {
        }
    }
}