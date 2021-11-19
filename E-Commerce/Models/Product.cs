using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Display(Name = "Product Name")]
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Column(TypeName = "money")]
        public int Price { get; set; }
        public int InventoryAmount { get; set; }
        public string Summary { get; set; }
        public string Condition { get; set; }
        //public IEnumerable<SelectListItem> ProductCondition { get; set; }

        [Display(Name = "Book Cover")]
        public string ProductImage { get; set; }
        public string ProductUrl { get; set; }
        //public string ThumbnailUri { get; set; }

        //If you leave this out, it will probably figure out that you need one - automatically makes it required
        [Display(Name = "Product Category")]
        public int ProductCategoryId { get; set; }

        //internal static Product GetOne ( int id )
       // {
       //     throw new NotImplementedException();
     //   }
    
        //Navigation Property that makes a ProductCategoryId a Foreign Key
        public ProductCategory ProductCategory { get; set; }

    }

    //public enum Condition
   // {
  //      Select,
  //      New,
  //      LikeNew,
 //       Good,
//        Ok,
//        Poor,
  //  }
}
