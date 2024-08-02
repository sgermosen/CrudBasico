using System.ComponentModel.DataAnnotations;

namespace EmployeeSystem.Application.Dtos.Employees
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Department { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Position { get; set; }
    }
}
