using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using IAmBacon.Admin.Presentation.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IAmBacon.Admin.ViewModels.Post
{
    public class EditPostViewModel
    {
        public int PostId { get; set; }

        public bool Active { get; set; }

        [Range(1, int.MaxValue)]
        public int AuthorId { get; set; }

        [Display(Name = "Author")]
        public List<SelectListItem> Authors { get; set; }

        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        public List<SelectListItem> Categories { get; set; }

        public string Image { get; set; }

        [Required]
        public string Markdown { get; set; }

        public bool NoCss { get; set; }

        public List<CheckboxItem> Tags { get; set; }

        [MaxLength(255)]
        [Required]
        public string Title { get; set; }
    }
}
