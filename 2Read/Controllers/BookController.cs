using _2Read.Models;
using Microsoft.AspNetCore.Mvc;

namespace _2Read.Controllers
{
	public class BookController : Controller
	{

		private readonly ApplicationDbContext _db;

		public BookController(ApplicationDbContext db)
		{
			_db = db; 
		}

		// GET: Books
		public IActionResult Index()
		{
			return View(_db.Books.ToList());
		}

		//GET: Books/Create
		public IActionResult Create()
		{
			return View();
		}
		// POST: Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create([Bind("Id, Name, Author")] Book book)
		{
			if (ModelState.IsValid)
			{
				_db.Add(book);
				_db.SaveChanges();

				return RedirectToAction(nameof(Index));
			}

			return View(book);
		}
	}
}
