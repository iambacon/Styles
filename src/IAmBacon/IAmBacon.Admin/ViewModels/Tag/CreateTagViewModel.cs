using System.ComponentModel.DataAnnotations;

namespace IAmBacon.Admin.ViewModels.Tag
{
    public class CreateTagViewModel
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
    }
}
