﻿using System.ComponentModel.DataAnnotations;

namespace EmployeeSystem.Web.Models
{
    public class Sex
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
