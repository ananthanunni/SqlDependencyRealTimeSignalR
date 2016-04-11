using SqlChangeTrackingPoc.Entity.Models;
using SqlChangeTrackingPoc.Models.DevTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlChangeTrackingPoc.Service.Mappers.Contracts
{
    public interface IMapper<TEntity,TModel>
        where TEntity:class
        where TModel:class
    {
        TEntity ConvertSingleToEntity(TModel model);

        TModel ConvertSingleFromEntity(TEntity entity);

        IEnumerable<TEntity> ConvertMultipleToEntityCollection(IEnumerable<TModel> models);

        IEnumerable<TModel> ConvertMultipleFromEntityCollection(IEnumerable<TEntity> entities);
    }
}
