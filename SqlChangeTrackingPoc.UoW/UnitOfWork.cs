using SqlChangeTrackingPoc.Entity.Models;
using SqlChangeTrackingPoc.Repository;
using SqlChangeTrackingPoc.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlChangeTrackingPoc.UoW
{
    public class UnitOfWork:IDisposable
    {
        private IRepository<DevTest> _devTestRepository = null;
        private DbContext _dbContext = null;

        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<DevTest> DevTestRepository
        {
            get
            {
                return _devTestRepository ?? (_devTestRepository = new DevTestRepository(_dbContext));
            }
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            
        }
    }
}
