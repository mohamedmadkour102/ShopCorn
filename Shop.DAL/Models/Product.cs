using Shop.DAL.Data;
using ShopCorn.DAL.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace Shop.DAL.Models
{
    public class Product : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [ValidateNever]
        public string Image { get; set; }
        [Required]
        public decimal Price { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
        [DisplayName ("Category")]
      

        public int CategoryId { get; set; }

    }
}
