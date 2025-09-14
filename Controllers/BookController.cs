using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            var books = await _context.Books
                .Include(b => b.Author)
                .Select(b => new BookViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    ISBN = b.ISBN,
                    PublishedDate = b.PublishedDate,
                    PageCount = b.PageCount,
                    AuthorName = b.Author != null ? $"{b.Author.FirstName} {b.Author.LastName}" : "Unknown"
                })
                .ToListAsync();

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
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            var viewModel = new BookViewModel
            {
                Id = book.Id,
                Title = book.Title,
                ISBN = book.ISBN,
                PublishedDate = book.PublishedDate,
                PageCount = book.PageCount,
                AuthorName = book.Author != null ? $"{book.Author.FirstName} {book.Author.LastName}" : "Unknown",
                AuthorBio = book.Author?.Bio,
                Categories = book.BookCategories?.Select(bc => bc.Category?.Name).Where(name => name != null).ToList() ?? new List<string>()
            };

            return View(viewModel);
        }
    }
}
