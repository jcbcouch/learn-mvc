using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> Create(Book book, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    // Create the uploads directory if it doesn't exist
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "books");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    // Generate a unique filename
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ImageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    // Save the file
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(fileStream);
                    }

                    // Save the relative path to the database
                    book.ImagePath = Path.Combine("images", "books", uniqueFileName).Replace("\\", "/");
                }

                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            // If we got this far, something failed; repopulate the view model and return
            var viewModel = new CreateBookViewModel
            {
                Book = book,
                Authors = new SelectList(await _context.Authors.ToListAsync(), "Id", "Name", book.AuthorId)
            };
            
            return View(viewModel);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            // Delete the image file if it exists
            if (!string.IsNullOrEmpty(book.ImagePath))
            {
                string filePath = Path.Combine(_webHostEnvironment.WebRootPath, book.ImagePath.TrimStart('~', '/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult ManageCategories(int id)
        {
            var book = _context.Books
                .Include(b => b.BookCategories)
                    .ThenInclude(bc => bc.Category)
                .FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            var assignedCategoryIds = book.BookCategories?.Select(bc => bc.CategoryId).ToList() ?? new List<int>();
            
            var availableCategories = _context.Categories
                .Where(c => !assignedCategoryIds.Contains(c.Id))
                .ToList();

            var viewModel = new BookCategoryViewModel
            {
                Book = book,
                BookCategories = book.BookCategories ?? new List<BookCategory>(),
                Categories = availableCategories.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCategory(int bookId, int categoryId)
        {
            var book = await _context.Books
                .Include(b => b.BookCategories)
                .FirstOrDefaultAsync(b => b.Id == bookId);

            if (book == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(categoryId);
            if (category == null)
            {
                return NotFound();
            }

            // Check if the category is already assigned to the book
            var existingAssignment = book.BookCategories?.FirstOrDefault(bc => bc.CategoryId == categoryId);
            if (existingAssignment != null)
            {
                // Category already assigned, just return to the same page
                return RedirectToAction(nameof(ManageCategories), new { id = bookId });
            }

            // Add the new category
            var bookCategory = new BookCategory
            {
                BookId = bookId,
                CategoryId = categoryId,
                AddedBy = "System",
                AddedDate = DateTime.UtcNow
            };

            if (book.BookCategories == null)
            {
                book.BookCategories = new List<BookCategory>();
            }

            book.BookCategories.Add(bookCategory);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ManageCategories), new { id = bookId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveCategory(int bookId, int categoryId)
        {
            var book = await _context.Books
                .Include(b => b.BookCategories)
                .FirstOrDefaultAsync(b => b.Id == bookId);

            if (book == null)
            {
                return NotFound();
            }

            var bookCategory = book.BookCategories?.FirstOrDefault(bc => bc.CategoryId == categoryId);
            if (bookCategory == null)
            {
                return NotFound();
            }

            book.BookCategories.Remove(bookCategory);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ManageCategories), new { id = bookId });
        }
    }
}
