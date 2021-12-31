using System;
using System.Collections.Generic;
using System.Text;
using TheaterDAL.Entities;
using TheaterDAL.Entities.Enum;
using TheaterLogic.Abstraction.IMappers;
using TheaterLogic.Models;

namespace TheaterLogic.Implementation.Mappers
{
    public class PlaceMapper : IMapper<PlaceEntity, Place>
    {
        public PlaceEntity ToEntity(Place model)
        {
            return new PlaceEntity()
            {
                Id = model.Id,
                PlaceType = (PlaceType) model.PlaceType,
                PriceMultiplier = model.PriceMultiplier
            };
        }

        public Place ToModel(PlaceEntity entity)
        {
            return new Place()
            {
                Id = entity.Id,
                PlaceType = (Models.Enum.PlaceType) entity.PlaceType,
                PriceMultiplier = entity.PriceMultiplier
            };
        }
    }
}
