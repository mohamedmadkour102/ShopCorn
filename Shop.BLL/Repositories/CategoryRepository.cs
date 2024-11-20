using Shop.BLL.Interfaces;
using ShopCorn.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BLL.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _dbContext;

        public CategoryRepository(AppDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }
        public void Update(Category category)
        {
            var catego = _dbContext.Categories.FirstOrDefault(c => c.Id == category.Id);
            if (catego != null)
            {
                catego.Name = category.Name;
                catego.Description = category.Description;
                catego.CreationDate=DateTime.Now;
            }
        }
    }
}
