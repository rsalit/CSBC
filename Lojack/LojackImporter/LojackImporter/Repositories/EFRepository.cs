using System;
using System.Data;
using System.Linq;
using System.Data.Entity;
using Lojack.Interfaces;
using System.Data.Entity.Infrastructure;
using EntityState = System.Data.Entity.EntityState;

namespace Lojack.Repositories
{
 
    //Changes are being saved in this repository. Eventually, if we move to a Unit of Work model, we will remove and put the 
    //login in the UOM calls
 
    public class EFRepository<T> : IRepository<T> where T : class
    {
        public EFRepository(){}
        public EFRepository(DbContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext");
            DataContext = dbContext;
            DbSet = DataContext.Set<T>();
        }
 
        protected DbContext DataContext { get; set; }
 
        protected DbSet<T> DbSet { get; set; }
 
        public virtual IQueryable<T> GetAll()
        {
            return DbSet;
        }
 
        public virtual T GetById(int id)
        {
            return DbSet.Find(id);
        }
 
        public virtual T Insert(T entity)
        {
            DbEntityEntry dbEntityEntry = DataContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = (EntityState) System.Data.EntityState.Added;
            }
            else
            {
                DbSet.Add(entity);
            }
            DataContext.SaveChanges();
            return entity;
        }
 
        public virtual void Update(T entity)
        {
            DbEntityEntry dbEntityEntry = DataContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Modified;
            }
            dbEntityEntry.State = EntityState.Modified;
            DataContext.SaveChanges();
        }
 
        public virtual void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = DataContext.Entry(entity);
            if (dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Deleted;
                
            }
            else
            {
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
            DataContext.SaveChanges();
        }
 
        public virtual void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null) return;
            Delete(entity);
        }
 
 
        public void Dispose()
        {
            if (DataContext != null)
            {
                DataContext.Dispose();
                DataContext = null;
            }
        }
 
    }
}
