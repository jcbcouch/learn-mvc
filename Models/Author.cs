using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LearnMvc.Models
{
    public class Author
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        
        public string Bio { get; set; }
        
        // Navigation property for one-to-many with Book
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
        
        // Computed property for full name
        public string FullName => $"{FirstName} {LastName}";
    }
}
