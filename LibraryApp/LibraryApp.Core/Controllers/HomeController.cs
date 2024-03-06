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
				return Json(new { success = false, message = "Ge�ersiz istek." });
			}

			if (string.IsNullOrEmpty(model.BorrowerName))
			{
				return Json(new { success = false, message = "�d�n� alan�n ad� bo� olamaz." });
			}

			if (model.BorrowDate == null || model.BorrowDate.Value.Date > DateTime.Now.Date)
			{
				return Json(new { success = false, message = "Ge�ersiz �d�n� alma tarihi." });
			}

			try
			{
				// �d�n� alma tarihi kontrol edilir.
				if (model.BorrowDate.Value.AddDays(15) < DateTime.Now.Date)
				{
					return Json(new { success = false, message = "Kitab� geri d�n�� tarihinden sonra �d�n� alamazs�n�z." });
				}

				// Kitab�n zaten �d�n� olup olmad���n� kontrol edilir.
				if (model.IsBorrowed)
				{
					return Json(new { success = false, message = "Kitap zaten �d�n� al�nm�� durumda." });
				}

				// Kitap �d�n� al�nd���nda, �d�n� al�nma tarihine 15 g�n ekleyerek son tarih belirlenir.
				model.DueDate = model.BorrowDate.Value.AddDays(15);

				_bookService.SaveBorrowedBook(model);

				return Json(new { success = true, message = "Kitap �d�n� verildi." });
			}
			catch (Exception ex)
			{
				// Hata durumunda uygun bir i�lem yap�labilir, �u an sadece hatay� geri d�ner.
				return Json(new { success = false, message = "Kitap �d�n� verilirken bir hata olu�tu: " + ex.Message });
			}
		}


		[HttpPost]

		public JsonResult NewBook(BookDTO model)
		{
			if (model == null)
			{
				return Json(new { success = false, message = "Ge�ersiz istek." });
			}


			try
			{

				if (Request.Form.Files.Count > 0)
				{
					var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
					var file = Request.Form.Files[0];
					//var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploaded-img"); // Y�kleme klas�r�n�n yolu
					var uniqueFileName =Guid.NewGuid().ToString().Substring(0, 6) + "_" + file.FileName; // Dosya ad� benzersiz bir �ekilde ol�u�sun

					var ImgPath = Path.Combine(filePath, uniqueFileName); // Dosyan�n tam yolu
					using (var fileStream = new FileStream(ImgPath, FileMode.Create))
					{
						file.CopyTo(fileStream); // Dosyay� sunucuya kaydet
					}
					model.Image = uniqueFileName;

				}
				_bookService.AddBook(model);
				return Json(new { success = true, message = "Kitap ba�ar�yla eklendi." });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = "Kitap eklenirken bir hata olu�tu: " + ex.Message });
			}
		}






		//[HttpPost]
		//public JsonResult NewBook([FromForm] BookDTO model, IFormFile image)
		//{
		//	if (model == null || image == null)
		//	{
		//		return Json(new { success = false, message = "Ge�ersiz istek." });
		//	}

		//	try
		//	{
		//		if (image.Length > 0)
		//		{
		//			var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploaded-img");
		//			var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName; // Dosya ad� benzersiz bir �ekilde olu�turuluyor

		//			var ImgPath = Path.Combine(filePath, uniqueFileName); // Dosyan�n tam yolu
		//			using (var fileStream = new FileStream(ImgPath, FileMode.Create))
		//			{
		//				image.CopyTo(fileStream); // Dosyay� sunucuya kaydet
		//			}
		//		}

		//		_bookService.AddBook(model);
		//		return Json(new { success = true, message = "Kitap ba�ar�yla eklendi." });
		//	}
		//	catch (Exception ex)
		//	{
		//		return Json(new { success = false, message = "Kitap eklenirken bir hata olu�tu: " + ex.Message });
		//	}
		//}



	}
}
