using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class Account
    {
        [Key]
        public int Id { get; set; }
        public List<Inventory> inventory { get; set; }
        public List<Check> check { get; set; }
    }
}