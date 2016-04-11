using SqlChangeTrackingPoc.Entity.Models;
using SqlChangeTrackingPoc.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlChangeTrackingPoc.Repository
{
    public abstract class GenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private DbContext _dbContext = null;
        private DbSet<TEntity> _collection = null;

        public GenericRepository(DbContext dbContext,bool registerDependecny=false)
        {
            _dbContext = dbContext;

            _collection = _dbContext.Set<TEntity>();

            if (registerDependecny)
            {
                SqlDependency.Start(_dbContext.Database.Connection.ConnectionString);
                var dependency = new SqlDependency();
                dependency.OnChange += Dependency_OnChange;
            }
        }

        private void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            Console.Write(e);
        }

        public void Add(TEntity newItem)
        {
            _collection.Add(newItem);
        }

        public TEntity Find(object id)
        {
            return _collection.Find(id);
        }

        public IEnumerable<TEntity> FindMultiple(Func<TEntity,bool> expression)
        {
            return _collection.Where(expression);
        }

        public TEntity FindSingle(Func<TEntity,bool> expression)
        {
           return _collection.Single(expression); 
        }

        public IEnumerable<TEntity> Get()
        {
            return _collection.ToList();
        }

        public void Remove(TEntity item)
        {
            _collection.Remove(item);
        }

        public void Update(TEntity item)
        {
            _collection.Find(item);            
        }
    }
}
