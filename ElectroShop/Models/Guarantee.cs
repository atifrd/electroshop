using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElectroShop.Models
{
    public class Guarantee
    {
        public Guarantee()
        {
            this.Stores = new HashSet<Store>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        #region forigen key
        [NotMapped]
        public int SellerId { get; set; }
        [NotMapped]
        public string SellerName { get; set; }
        [ForeignKey("SellerId")]
        public virtual Seller RelatedSeller { get; set; }
        #endregion forigen key
        public virtual ICollection<Store> Stores { get; set; }
    }
}