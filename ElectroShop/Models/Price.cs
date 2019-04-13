using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ElectroShop.Models
{
    public class Price
    {
        [Key]
        public long PriceId { get; set; }
        public long StoreId { get; set; }
        public decimal MainPrice { get; set; }
        public decimal SalesPrice { get; set; }
        public int OfferTypeId { get; set; }
        public DateTime DatePrice { get; set; }
        public string DatePriceFa { get; set; }
        public bool Enabled { get; set; }

        public virtual Store Store { get; set; }

    }
}