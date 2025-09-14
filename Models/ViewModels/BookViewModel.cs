using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LearnMvc.Models.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        
        [Display(Name = "Title")]
        public string Title { get; set; }
        
        [Display(Name = "ISBN")]
        public string ISBN { get; set; }
        
        [Display(Name = "Published Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime PublishedDate { get; set; }
        
        [Display(Name = "Page Count")]
        public int PageCount { get; set; }
        
        [Display(Name = "Author")]
        public string AuthorName { get; set; }
        
        [Display(Name = "Author Bio")]
        public string AuthorBio { get; set; }
        
        [Display(Name = "Categories")]
        public List<string> Categories { get; set; } = new List<string>();
    }
}
