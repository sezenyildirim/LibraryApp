using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Data.DTOS
{
	public class BorrowedBooksDTO
	{
		public int ID { get; set; }
		public int BookID { get; set; }
		public string BorrowerName { get; set; }
		public DateTime BorrowDate { get; set; }
		public DateTime DueDate { get; set; }
	}
}
