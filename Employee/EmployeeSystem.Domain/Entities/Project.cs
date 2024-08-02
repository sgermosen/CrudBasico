using System.ComponentModel.DataAnnotations;

namespace EmployeeSystem.Domain.Entities
{
    public class Project
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Client { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; }

        // Navigation property
        public ICollection<Assignment> Assignments { get; set; }
    }
}
