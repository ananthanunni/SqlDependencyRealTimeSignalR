using SqlChangeTrackingPoc.Entity.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlChangeTrackingPoc.Repository
{
    public class DevTestRepository : GenericRepository<DevTest>
    {
        public DevTestRepository(DbContext dbContext)
            :base(dbContext)
        {
        }

    }
}
