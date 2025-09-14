using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LearnMvc.Models
{
    public class LibraryCard
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CardNumber { get; set; }  // Primary key
        
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiryDate { get; set; }
        public bool IsActive { get; set; } = true;
        
        // Foreign key for one-to-one with Student
        public int StudentId { get; set; }
        
        // Navigation property
        public virtual Student Student { get; set; }
    }
}
