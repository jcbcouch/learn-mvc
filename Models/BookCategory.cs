using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnMvc.Models
{
    public class BookCategory
    {
        // Composite primary key
        public int BookId { get; set; }
        public int CategoryId { get; set; }
        
        // Navigation properties
        [ForeignKey("BookId")]
        public virtual Book Book { get; set; }
        
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        
        // Additional properties for the join table
        public DateTime AddedDate { get; set; } = DateTime.UtcNow;
        public string AddedBy { get; set; }  // Could be a username or system identifier
    }
}
