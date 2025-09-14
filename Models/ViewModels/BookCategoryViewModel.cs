using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using LearnMvc.Models;


namespace LearnMvc.Models.ViewModels
{
    public class BookCategoryViewModel
    {
       public Book Book { get; set; }
       public BookCategory BookCategory { get; set; }

       [ValidateNever]
       public IEnumerable<SelectListItem> Categories { get; set; }
       [ValidateNever]
       public IEnumerable<BookCategory> BookCategories { get; set; }
    }
}