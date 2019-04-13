using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ElectroShop.Models
{
    public class Property
    {
        public Property()
        {
            this.ProductProperties = new HashSet<ProductProperty>();
            this.CategoryProperties = new HashSet<CategoryProperty>();
            this.PropertyOptions = new HashSet<PropertyOption>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Description("پردازنده")]
        [MaxLength(500)]
        public string Name { get; set; }

        [Description("cpu")]
        [MaxLength(500)]
        public string OroginalName { get; set; }

        [Description("hint or popup info")]
        [MaxLength(500)]
        public string Description { get; set; }

        [Description("html tag element : text , select , checkbox...")]
        [MaxLength(500)]
        public string ControlType { get; set; }

        [Description("value type : number , text , bool...")]
        [MaxLength(500)]
        public string ValueType { get; set; }

        [Description("fill is manadatory")]
        [DefaultValue(true)]
        public bool Required { get; set; }

        [Description("label or placeholer like : please enter number or Company Name")]
        public string PlaceHolder { get; set; }
        public int Order { get; set; }

        [Description("search product's by value of this property ")]
        [DefaultValue(true)]
        public bool IsSearchable { get; set; }
        public bool ShowInDetails { get; set; }

        [Description("show in menu after category")]
        [DefaultValue(false)]
        public bool UseAsMenu { get; set; }

        #region forigen key
       [ForeignKey("ParentId")]
        public int? ParentId { get; set; }
        public virtual Property ParentProperty { get; set; }

        #endregion forigen key


        #region Relations
        public virtual ICollection<CategoryProperty> CategoryProperties { get; set; }
        public virtual ICollection<PropertyOption> PropertyOptions { get; set; }
        public virtual ICollection<ProductProperty> ProductProperties { get; set; }
        #endregion Relations
        //faghat mitavand 3 sath dashte bashad
        //sathe aval be ye catagoryId vasl mishavad
        //sath 2 daste bandy propery manand dorbin ke khodesh zir majmoe darad v dar sathe 3 gharar migirad

        // در حالتی سایتی مانند دیوار فقط باید یک سطح داشته باشد
    }
}