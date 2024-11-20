using Shop.BLL.Interfaces;
using Shop.DAL.Models;
using ShopCorn.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BLL.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }
        public void Update(Product product)
        {
            var prod = _dbContext.Products.FirstOrDefault(p => p.Id == product.Id);
            if (prod != null)
            {
                prod.Name = product.Name;
                prod.Description = product.Description;
                prod.Price = product.Price;
                prod.Image = product.Image;
                prod.CategoryId = product.CategoryId;

            }
        }
    }
}
