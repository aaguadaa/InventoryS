using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }
        public string Categoria { get; set; }
        public string Description { get; set; }
        public Account account { get; set; }
        public Product productUpdt { get; set; }
        public List<Product> product { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
