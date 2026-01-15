using dewi.Models.common;

namespace dewi.Models
{
    public class Position:BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<Employee> Employees { get; set; } = [];
    }
}
