using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Categoria { get; set; }
        public string Descripcion { get; set; }
        public string Status { get; set; }
    }
}

