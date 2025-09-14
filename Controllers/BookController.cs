using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LearnMvc.Data;
using LearnMvc.Models;
using LearnMvc.Models.ViewModels;

namespace LearnMvc.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Book
        public async Task<IActionResult> Index()
        {
            List<Book> books = await _context.Books.Include(b => b.Author).ToListAsync();
            return View(books);
        }

        // GET: Book/Details/5
        public async Task<IActionResult> BookDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Book/Create
        public async Task<IActionResult> Create()
        {
            var authors = await _context.Authors
                .OrderBy(a => a.LastName)
                .ThenBy(a => a.FirstName)
                .Select(a => new SelectListItem 
                { 
                    Value = a.Id.ToString(),
                    Text = $"{a.LastName}, {a.FirstName}"
                })
                .ToListAsync();

            var viewModel = new CreateBookViewModel
            {
                Book = new Book
                {
                    PublishedDate = DateTime.Today
                },
                Authors = authors
            };

            return View(viewModel);
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Authors = await _context.Authors
                    .OrderBy(a => a.LastName)
                    .ThenBy(a => a.FirstName)
                    .Select(a => new SelectListItem 
                    { 
                        Value = a.Id.ToString(),
                        Text = $"{a.LastName}, {a.FirstName}"
                    })
                    .ToListAsync();
                return View(viewModel);
            }

                _context.Add(viewModel.Book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }
    }
}
