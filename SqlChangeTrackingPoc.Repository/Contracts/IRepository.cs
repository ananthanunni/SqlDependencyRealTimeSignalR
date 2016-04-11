using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlChangeTrackingPoc.Repository.Contracts
{
    public interface IRepository<T>
        where T : class
    {
        T Find(object id);

        IEnumerable<T> Get();

        T FindSingle(Func<T,bool> expression);

        IEnumerable<T> FindMultiple(Func<T,bool> expression);

        void Add(T newItem);

        void Update(T item);

        void Remove(T item);
    }
}
