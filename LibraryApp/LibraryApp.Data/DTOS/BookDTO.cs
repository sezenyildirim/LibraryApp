using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Data.Models;

namespace LibraryApp.Data.DTOS
{
	public class BookDTO
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Author { get; set; }
		public string Image { get; set; }
		public bool IsBorrowed { get; set; }
		public string? BorrowerName { get; set; }
		public DateTime? BorrowDate { get; set; }
		public DateTime? DueDate { get; set; }
		public IEnumerable<BookDTO> Books;

		public BorrowedBooksDTO BorrowedBooksDTO { get; set;}
	}
}
