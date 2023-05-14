using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Model
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }
        public string Categoria { get; set; }
        public string Description { get; set; }
        public Account Account { get; set; }
        public Product UpdatedProduct { get; set; }
        public List<Product> Products { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
