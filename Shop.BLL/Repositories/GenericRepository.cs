using Microsoft.EntityFrameworkCore;
using Shop.BLL.Interfaces;
using Shop.DAL.Data;
using ShopCorn.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepositories<T> where T : BaseEntity
    {
        private readonly AppDbContext _dbContext;
        private DbSet<T> _dbSet;
        public GenericRepository(AppDbContext dbContext) 
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public void Add(T item)
        {
            _dbSet.Add(item);
        }

        public void DeleteRange(IEnumerable<T> items)
        {
            _dbSet.RemoveRange(items);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> Predicate, string? IncludeWord)
        {
            IQueryable<T> query = _dbSet;
            // handle the criteria ==> where(p=>p.id == ID)
            if (Predicate != null)
                query = query.Where(Predicate);


            if (IncludeWord != null)
            {
                // because we may have more than one Include 
                foreach(var item in IncludeWord.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return query.ToList();

        }

        public T GetFirst(Expression<Func<T, bool>> Predicate, string? IncludeWord)
        {
            IQueryable<T> query = _dbSet;
            // handle the criteria ==> where(p=>p.id == ID)
            if (Predicate != null)
                query = query.Where(Predicate);


            if (IncludeWord != null)
            {
                // because we may have more than one Include 
                foreach (var item in IncludeWord.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }
            return query.SingleOrDefault();
        }

        public void Remove(T item)
        {
            _dbContext.Remove(item);
        }


    }
}
