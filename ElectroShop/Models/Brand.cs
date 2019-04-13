using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElectroShop.Models
{
    public class Brand
    {
        public Brand()
        {
            this.Products = new HashSet<Product>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(500)]
        public string Name { get; set; }
        public bool IsEnabled { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}