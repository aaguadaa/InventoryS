using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string UserName { get; set; }
        public bool IsBlocked { get; set; } // Indica si el usuario está bloqueado o no
        public List<Inventory> Inventory { get; set; } // Lista de inventarios del usuario
        public Account Account { get; set; }
    }
}
