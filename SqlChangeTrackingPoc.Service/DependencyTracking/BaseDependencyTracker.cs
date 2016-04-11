using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableDependency.SqlClient;

namespace SqlChangeTrackingPoc.Service.DependencyTracking
{
    public class BaseDependencyTracker<TEntity>
        where TEntity:class
    {
        private SqlTableDependency<TEntity> _tableDependency = null;

        public BaseDependencyTracker(string connectionString,string tableName)
        {
            _tableDependency = new SqlTableDependency<TEntity>(connectionString,tableName);

            _tableDependency.OnChanged += _tableDependency_OnChanged;
            _tableDependency.OnError += _tableDependency_OnError;
        }

        public void BeginTracking()
        {
            _tableDependency.Start();
        }

        public void StopTracking()
        {
            _tableDependency.Stop();
        }

        public event EventHandler<TableDependency.EventArgs.ErrorEventArgs> ErrorOccurred;
        private void _tableDependency_OnError(object sender, TableDependency.EventArgs.ErrorEventArgs e)
        {
            if (ErrorOccurred != null)
                ErrorOccurred(sender, e);
        }

        public event EventHandler<TableDependency.EventArgs.RecordChangedEventArgs<TEntity>> RecordChanged;
        private void _tableDependency_OnChanged(object sender, TableDependency.EventArgs.RecordChangedEventArgs<TEntity> e)
        {
            if (RecordChanged != null)
                RecordChanged(sender, e);
        }
    }
}
