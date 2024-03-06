using LibraryApp.Data.Context;
using LibraryApp.Data.DTOS;
using LibraryApp.Data.Models;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.BLL.Services
{
	public class BookService : EntityServices<BookDTO>
	{
		public LibraryDBContext _context;
		public BookService(LibraryDBContext context) :base(context) 
		{
			_context = context;
		}

	
		 public IEnumerable<BookDTO> GetBooks()
		{
			List<BookDTO> bookList = new List<BookDTO>();

			var books = _context.Books.OrderBy(b => b.Name).ToList();

			foreach (var book in books)
			{
				BookDTO bookDTO = new BookDTO
				{
					ID = book.ID,
					Name = book.Name,
					Author = book.Author,
					Image = book.Image
			
					
				};
				if (book.IsBorrowed.HasValue && book.IsBorrowed.Value)
				{
					bookDTO.IsBorrowed = true;
					bookDTO.BorrowerName = book.BorrowerName;
					bookDTO.BorrowDate = book.BorrowDate.Value.Date;
					bookDTO.DueDate = book.DueDate.HasValue ? book.DueDate.Value.Date : (DateTime?)null;
				}
				else
				{
					bookDTO.IsBorrowed = false;
				}


				bookList.Add(bookDTO);
			}

			return bookList;

		}

		public void SaveBorrowedBook(BookDTO bookDTO)
		{
			var existingBook = _context.Books.Find(bookDTO.ID);

			if (existingBook != null)
			{
				
				existingBook.IsBorrowed = true;
				existingBook.BorrowerName = bookDTO.BorrowerName;
				existingBook.BorrowDate = bookDTO.BorrowDate;
				existingBook.DueDate = bookDTO.BorrowDate.Value.AddDays(15);

			
				_context.SaveChanges();
			}
			else
			{
			
				throw new Exception("Belirtilen kitap ID'si bulunamadı.");
			}

		}

		public void AddBook(BookDTO bookDTO)
		{
			try
			{
				var book = new Book()
				{
					Name = bookDTO.Name,
					Author = bookDTO.Author,
					Image = bookDTO.Image,
					IsBorrowed = false,

				};
				_context.Books.Add(book);
				_context.SaveChanges();

			}
			catch
			{

				throw new Exception("Kitap ekleme işlemi sırasında bir hata oluştu!"); ;
			}
			
			
		}

	}
}
