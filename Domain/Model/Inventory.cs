using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Model
{
    public class Inventory
    {
        [Key, ForeignKey("Account")]
        public int Id { get; set; }

        public string Categoria { get; set; }
        public string Description { get; set; }

        public virtual Account Account { get; set; }

        public Product UpdatedProduct { get; set; }
        public List<Product> Products { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}