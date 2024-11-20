using Shop.DAL.Models;
using ShopCorn.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BLL.Interfaces
{
    public interface IProductRepository : IGenericRepositories<Product>
    {
        void Update (Product product);
    }
}
