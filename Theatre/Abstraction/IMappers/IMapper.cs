using System;
using System.Collections.Generic;
using System.Text;

namespace TheaterLogic.Abstraction.IMappers
{
    public interface IMapper<TEntity, TModel> : IBackMapper<TEntity, TModel>
    {
        TModel ToModel(TEntity entity);
    }
}
