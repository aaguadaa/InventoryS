using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class Check
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Account")]
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public DateTime Date { get; set; }
        public List<string> Notes { get; set; }
        public List<Product> Products { get; set; }

        // Constructor
        public Check()
        {
            Notes = new List<string>();
            Products = new List<Product>();
        }
    }
}
