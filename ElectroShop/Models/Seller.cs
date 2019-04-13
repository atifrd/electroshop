using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElectroShop.Models
{
    public class Seller
    {

        public Seller()
        {
            //this.Stores = new HashSet<Store>();
            this.Products = new HashSet<Product>();
            this.Guarantees = new HashSet<Guarantee>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(500)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string Tell { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [StringLength(200)]
        public string Facebook { get; set; }

        [StringLength(200)]
        public string Twitter { get; set; }

        [StringLength(200)]
        public string Instagram { get; set; }

        [StringLength(200)]
        public string Linkedin { get; set; }
        [StringLength(200)]
        public string Skype { get; set; }

        [StringLength(150)]
        public string WebSite { get; set; }

        [ColumnAttribute(Order = 33)]
        [StringLength(250)]
        public string Country { get; set; }


        [Description("documents for introduction")]
        [MaxLength(500)]
        public string AttachmentPath { get; set; }

        [Description("active by admin after approved")]
        [DefaultValue(false)]
        public bool IsEnabled { get; set; }

        [NotMapped]
        public string UserId { get; set; }
        [NotMapped]
        public string UserName { get; set; }
        [ForeignKey("UserId")]
        public virtual User RelatedUser { get; set; }


        //public virtual ICollection<Store> Stores { get; set; }

        [Description("کالای ثبت شده توسط فروشنده برای فروش")]
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Guarantee> Guarantees { get; set; }
    }
}