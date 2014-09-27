using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data.Entity;
 
namespace Lojack.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Insert(T entity);
        void Delete(T entity);
        IQueryable<T> GetAll();
        T GetById(int id);
        void Update(T entity);
    }
}
