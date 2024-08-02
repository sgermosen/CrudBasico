using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EmployeeSystem.Domain.Entities
{
    public class Assignment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; }

        [Required]
        public int ProjectID { get; set; }

        [ForeignKey("ProjectID")]
        public Project Project { get; set; }

        [Required]
        [MaxLength(50)]
        public string Role { get; set; }

        [Required]
        public int HoursAssigned { get; set; }
    }
}
