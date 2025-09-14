using System;
using System.Collections.Generic;
﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using LearnMvc.Models;

namespace LearnMvc.Models.ViewModels
{
    public class CreateBookViewModel
    {
       public Book Book { get; set; }
        
        [ValidateNever]
        public IEnumerable<SelectListItem> Authors { get; set; }
    }
}
