﻿using System.ComponentModel.DataAnnotations;

namespace IAmBacon.Admin.ViewModels.Category
{
    public class CreateCategoryViewModel
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
    }
}
