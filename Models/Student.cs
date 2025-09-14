using System;
using System.ComponentModel.DataAnnotations;

namespace LearnMvc.Models
{
    public class Student
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        
        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;
        
        // Navigation property for one-to-one with LibraryCard
        public virtual LibraryCard LibraryCard { get; set; }
    }
}
