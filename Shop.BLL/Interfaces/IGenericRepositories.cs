using Shop.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Shop.BLL.Interfaces
{
    public interface IGenericRepositories<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll(Expression<Func<T,bool>>? Predicate = null , string? IncludeWord = null);
        T GetFirst(Expression<Func<T, bool>>?  Predicate = null, string? IncludeWord = null);
        void Add(T item);
        void Remove(T item);
        void DeleteRange(IEnumerable<T> items);

    }
}
