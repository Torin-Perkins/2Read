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

		// GET: Books/Edit/id
		public IActionResult Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var book = _db.Books.FirstOrDefault(m => m.Id == id);
			if (book == null)
			{
				return NotFound();
			}
			return View(book);

		}
		// POST: Books/Edit/id
		[HttpPost]
		public IActionResult Edit(int id, [Bind("Id, Name, Author")] Book book)
		{
			if (ModelState.IsValid)
			{
				if(id != book.Id)
				{
					return NotFound();
				} 
				else
				{
					_db.Update(book);
					_db.SaveChanges();
				}
				return RedirectToAction(nameof(Index));
			}
			return View(book);
		}
		//GET: Books/Delete/id
		public IActionResult Delete(int? id)
		{
            if (id == null)
            {
                return NotFound();
            }
            var book = _db.Books.FirstOrDefault(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

		//POST: Books/Delete/id
		[HttpPost]
		public IActionResult Delete(int id, bool notUsed)
		{
			var book = _db.Books.FirstOrDefault(m => m.Id == id);
			_db.Books.Remove(book);
			_db.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		//GET: Search Book
		public IActionResult Search(string searchString)
		{
			if (searchString == null)
			{
				return NotFound();
			}
			var book = _db.Books.Where(m => m.Name == searchString).ToList();

			if (book == null)
			{
				return RedirectToAction(nameof(Index));
			}
			return View(book);
		}
	}
}
