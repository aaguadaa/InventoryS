using Domain;
using Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Check
    {
        [Key]
        public int Id { get; set; }
        public Inventory Inventory { get; set; }
        public Product Product { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public User User { get; set; }
        public Account Account { get; set; }
        public List<string> Notes { get; set; }
        public int ProductId { get; set; }
    }
}