using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LearnMvc.Models
{
    public class Category
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        // Navigation property for many-to-many with Book
        public virtual ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
    }
}
