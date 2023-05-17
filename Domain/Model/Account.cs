using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Check> Checks { get; set; }
        public DateTime Date { get; set; }
        public List<string> Notes { get; set; }
        public Inventory Inventory { get; set; } // Propiedad de navegación hacia la clase Inventory
    }
}
