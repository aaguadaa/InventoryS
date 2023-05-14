using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public User User { get; set; }
        public List<Check> Checks { get; set; }
    }
}
