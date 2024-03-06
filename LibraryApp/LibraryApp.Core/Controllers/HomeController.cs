using LibraryApp.BLL.Services;
using LibraryApp.Core.Models;
using LibraryApp.Data.DTOS;
using LibraryApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LibraryApp.Core.Controllers
{
	public class HomeController : Controller
	{
		private BookService _bookService;
		public HomeController(BookService bookService)
		{
			_bookService = bookService;
		}

		public IActionResult Index()
		{
			BookDTO model = new BookDTO();
			model.Books = _bookService.GetBooks();
			//model.BorrowedBooksDTO = _bookService.GetBorrowedBooks(bookID);
			return View(model);
		}
		[HttpPost]
		public JsonResult BorrowBook([FromBody] BookDTO model)
		{
			if (model == null)
			{
				return Json(new { success = false, message = "Geçersiz istek." });
			}

			if (string.IsNullOrEmpty(model.BorrowerName))
			{
				return Json(new { success = false, message = "Ödünç alanýn adý boþ olamaz." });
			}

			if (model.BorrowDate == null || model.BorrowDate.Value.Date > DateTime.Now.Date)
			{
				return Json(new { success = false, message = "Geçersiz ödünç alma tarihi." });
			}

			try
			{
				// Ödünç alma tarihi kontrol edilir.
				if (model.BorrowDate.Value.AddDays(15) < DateTime.Now.Date)
				{
					return Json(new { success = false, message = "Kitabý geri dönüþ tarihinden sonra ödünç alamazsýnýz." });
				}

				// Kitabýn zaten ödünç olup olmadýðýný kontrol edilir.
				if (model.IsBorrowed)
				{
					return Json(new { success = false, message = "Kitap zaten ödünç alýnmýþ durumda." });
				}

				// Kitap ödünç alýndýðýnda, ödünç alýnma tarihine 15 gün ekleyerek son tarih belirlenir.
				model.DueDate = model.BorrowDate.Value.AddDays(15);

				_bookService.SaveBorrowedBook(model);

				return Json(new { success = true, message = "Kitap ödünç verildi." });
			}
			catch (Exception ex)
			{
				// Hata durumunda uygun bir iþlem yapýlabilir, þu an sadece hatayý geri döner.
				return Json(new { success = false, message = "Kitap ödünç verilirken bir hata oluþtu: " + ex.Message });
			}
		}


		[HttpPost]

		public JsonResult NewBook(BookDTO model)
		{
			if (model == null)
			{
				return Json(new { success = false, message = "Geçersiz istek." });
			}


			try
			{

				if (Request.Form.Files.Count > 0)
				{
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
					var file = Request.Form.Files[0];
					//var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploaded-img"); // Yükleme klasörünün yolu
					var uniqueFileName =Guid.NewGuid().ToString().Substring(0, 6) + "_" + file.FileName; // Dosya adý benzersiz bir þekilde olýuþsun

					var ImgPath = Path.Combine(filePath, uniqueFileName); // Dosyanýn tam yolu
					using (var fileStream = new FileStream(ImgPath, FileMode.Create))
					{
						file.CopyTo(fileStream); // Dosyayý sunucuya kaydet
					}
					model.Image = uniqueFileName;

				}
				_bookService.AddBook(model);
				return Json(new { success = true, message = "Kitap baþarýyla eklendi." });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = "Kitap eklenirken bir hata oluþtu: " + ex.Message });
			}
		}






		//[HttpPost]
		//public JsonResult NewBook([FromForm] BookDTO model, IFormFile image)
		//{
		//	if (model == null || image == null)
		//	{
		//		return Json(new { success = false, message = "Geçersiz istek." });
		//	}

		//	try
		//	{
		//		if (image.Length > 0)
		//		{
		//			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploaded-img");
		//			var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName; // Dosya adý benzersiz bir þekilde oluþturuluyor

		//			var ImgPath = Path.Combine(filePath, uniqueFileName); // Dosyanýn tam yolu
		//			using (var fileStream = new FileStream(ImgPath, FileMode.Create))
		//			{
		//				image.CopyTo(fileStream); // Dosyayý sunucuya kaydet
		//			}
		//		}

		//		_bookService.AddBook(model);
		//		return Json(new { success = true, message = "Kitap baþarýyla eklendi." });
		//	}
		//	catch (Exception ex)
		//	{
		//		return Json(new { success = false, message = "Kitap eklenirken bir hata oluþtu: " + ex.Message });
		//	}
		//}



	}
}
