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
        public bool IsBlocked { get; set; }
        public virtual Inventory Inventory { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }  // Cambio de tipo a ICollection<Account>

        public User()
        {
            Accounts = new List<Account>();  // Inicialización de la colección en el constructor
        }
    }
}