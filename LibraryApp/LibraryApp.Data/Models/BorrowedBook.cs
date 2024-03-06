using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Data.Models
{
	public class BorrowedBook
	{
        public int ID { get; set; }
        public int BookID { get; set; }
        public string BorrowerName { get; set; }
        public bool IsReturned { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}
