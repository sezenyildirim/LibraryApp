using LibraryApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Data.Context
{
	public class LibraryDBContext : DbContext
	{
		public LibraryDBContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Book> Books { get; set; }
		//public DbSet<BorrowedBook> BorrowedBooks { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//seed data - migration işlemleri ile db oluşturulurken books tablosuna otomatik eklenecek kitaplar
			modelBuilder.Entity<Book>()
				.HasData(
				new Book() { ID = 1, Name = "Portrait photography", Author = "Adam Silber", Image = "tab-item1.jpg", IsBorrowed = true, BorrowerName = "Beste Sezen Yıldırım", BorrowDate = new DateTime(2024, 4, 2), DueDate = new DateTime(2024, 2, 15) },
				new Book() { ID = 2, Name = "Once upon a time", Author = "Klien Marry", Image = "tab-item2.jpg" },
				new Book() { ID = 3, Name = "Tips of simple lifestyle", Author = "Bratt Smith", Image = "tab-item3.jpg" },
				new Book() { ID = 4, Name = "Just felt from outside", Author = "Nicole Wilson", Image = "tab-item4.jpg" },
				new Book() { ID = 5, Name = "Peaceful Enlightment", Author = "Marmik Lama", Image = "tab-item5.jpg" },
				new Book() { ID = 6, Name = "Great travel at desert", Author = "Sanchit Howdy", Image = "tab-item6.jpg" },
				new Book() { ID = 7, Name = "Life among the pirates", Author = "Armor Ramsey", Image = "tab-item7.jpg" },
				new Book() { ID = 8, Name = "Simple way of piece life", Author = "Armor Ramsey", Image = "tab-item8.jpg" }
				);
		}
	}
}
