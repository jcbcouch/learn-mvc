using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LearnMvc.Models
{
    public class Book
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        
        [StringLength(13)]
        public string ISBN { get; set; }
        
        public DateTime PublishedDate { get; set; }
        public int PageCount { get; set; }
        
        // Foreign key for Author (one-to-many)
        public int AuthorId { get; set; }
        
        // Navigation property to Author
        public virtual Author Author { get; set; }
        
        // Navigation property for many-to-many with Category
        public virtual ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
    }
}
