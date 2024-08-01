using System.ComponentModel.DataAnnotations;

namespace EmployeeSystem.Web.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

       // [Required]
        [MaxLength(50)]
        public string? Department { get; set; }

       // [Required]
        [MaxLength(50)]
        public string? Position { get; set; }

      //  [Required]
        public DateTime? HireDate { get; set; } 

        public int? SexId { get; set; }
        public  Sex? Sex { get; set; }

        public  ICollection<Assignment>? Assignments { get; set; }
    }
}
