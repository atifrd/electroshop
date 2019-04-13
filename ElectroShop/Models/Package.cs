using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElectroShop.Models
{
    public class Package
    {
        public Package()
        {
            this.Stores = new HashSet<Store>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(500)]
        public string Name { get; set; }
        public bool IsEnabled { get; set; }

        public virtual ICollection<Store> Stores { get; set; }
    }
}