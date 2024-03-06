using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Data.Models
{
	public class Book
	{
        public int ID { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public bool? IsBorrowed { get; set; }
		public string? BorrowerName { get; set; }
		public DateTime? BorrowDate { get; set; }
		public DateTime? DueDate { get; set; }
		//[NotMapped]
		//public IEnumerable<Book> Books { get; set; }
	}
}
