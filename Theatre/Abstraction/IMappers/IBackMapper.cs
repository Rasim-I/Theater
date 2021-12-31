using System;
using System.Collections.Generic;
using System.Text;

namespace TheaterLogic.Abstraction.IMappers
{
    public interface IBackMapper<TEntity, TModel>
    {
        TEntity ToEntity(TModel model);
    }
}
