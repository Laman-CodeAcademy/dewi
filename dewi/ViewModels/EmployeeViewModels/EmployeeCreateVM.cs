using System.ComponentModel.DataAnnotations;

namespace dewi.ViewModels.EmployeeViewModels
{
    public class EmployeeCreateVM
    {
        [Required]
        public IFormFile Image { get; set; } = null!;
        [Required, MaxLength(100), MinLength(2)]

        public string Name { get; set; } = string.Empty;
        [Required, MaxLength(100), MinLength(20)]

        public string Description { get; set; } = string.Empty;
        [Required]

        public int PositionId { get; set; }
    }
}
