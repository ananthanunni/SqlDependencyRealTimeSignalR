using AutoMapper;
using SqlChangeTrackingPoc.Service.Mappers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlChangeTrackingPoc.Service.Mappers
{
    public abstract class BaseMapper<TEntity, TModel> : IMapper<TEntity, TModel>
        where TEntity : class
        where TModel : class
    {
        public virtual IEnumerable<TModel> ConvertMultipleFromEntityCollection(IEnumerable<TEntity> entities)
        {
            if (entities == null || entities.Count() == 0)
                return null;

            return entities.Select(t => MapperConfig.Mapping.Map<TModel>(t));
        }

        public virtual IEnumerable<TEntity> ConvertMultipleToEntityCollection(IEnumerable<TModel> models)
        {
            if (models == null || models.Count() == 0)
                return null;

            return models.Select(t => MapperConfig.Mapping.Map<TEntity>(t));
        }

        public virtual TModel ConvertSingleFromEntity(TEntity entity)
        {
            if (entity == null)
                return null;

            return MapperConfig.Mapping.Map<TModel>(entity);
        }

        public virtual TEntity ConvertSingleToEntity(TModel model)
        {
            if (model == null)
                return null;

            return MapperConfig.Mapping.Map<TEntity>(model);
        }
    }
}
