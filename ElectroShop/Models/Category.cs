using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElectroShop.Models
{
    public class Category
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
            this.CategoryProperties = new HashSet<CategoryProperty>();
            this.SubCategories = new HashSet<Category>();
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }


        #region Forigen Key
        [ForeignKey("ParentId")] //ati
        public Nullable<int> ParentId { get; set; }
        public virtual Category ParentCategory { get; set; }
        #endregion Forigen Key

        #region Relations
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Category> SubCategories { get; set; }
        public virtual ICollection<CategoryProperty> CategoryProperties { get; set; }
        #endregion Relations

        #region notmapped
        //[NotMapped]
        //public bool SelectProducts { get; set; }
        #endregion notmapped

    }
}