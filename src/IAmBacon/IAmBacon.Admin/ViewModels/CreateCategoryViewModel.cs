using System.ComponentModel.DataAnnotations;

namespace IAmBacon.Admin.ViewModels
{
    public class CreateCategoryViewModel
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
    }
}
