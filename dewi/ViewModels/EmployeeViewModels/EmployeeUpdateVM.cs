using System.ComponentModel.DataAnnotations;

namespace dewi.ViewModels.EmployeeViewModels
{
    public class EmployeeUpdateVM
    {
        public int Id { get; set; }
        [Required]
        public IFormFile? Image { get; set; }
        [Required, MaxLength(100), MinLength(2)]

        public string Name { get; set; } 
        [Required, MaxLength(100), MinLength(20)]

        public string Description { get; set; }
        [Required]

        public int PositionId { get; set; }
    }
}
