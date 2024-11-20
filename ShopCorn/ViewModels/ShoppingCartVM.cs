using Shop.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace ShopCorn.ViewModels
{
    public class ShoppingCartVM
    {
        public Product Product { get; set; }
        [Range(1,100,ErrorMessage ="You Must Enter Value Between 1 to 100")]
        public int Count { get; set; }
    }
}
