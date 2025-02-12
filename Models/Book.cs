using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Models
{
    public class Book
    {
   
        public string Title { get; set; }

        public string Author { get; set; }
       
        public string ISBN { get; set; }

        public int copiesAvailable { get; set; }
    }
}
