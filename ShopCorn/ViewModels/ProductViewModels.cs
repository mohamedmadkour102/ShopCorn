using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shop.DAL.Models;

namespace ShopCorn.ViewModels
{
    public class ProductViewModels
    {
        public Product product { get; set; }
        [ValidateNever]

        public IEnumerable<SelectListItem> CategoryList { get; set; }
    }
}
