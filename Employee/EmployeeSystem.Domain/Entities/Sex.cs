using System.ComponentModel.DataAnnotations;

namespace EmployeeSystem.Domain.Entities
{
    public class Sex
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
