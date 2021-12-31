using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Text;
using TheaterDAL.Entities;
using TheaterDAL.Entities.Enum;
//using TheaterDAL.Entities.TicketTypes;
using TheaterLogic.Abstraction.IMappers;
using TheaterLogic.Models;
//using TheaterLogic.Models.TicketTypes;

namespace TheaterLogic.Implementation.Mappers
{
    public class TicketMapper : IMapper<TicketEntity, Ticket>
    {
        //private ShowMapper _showMapper = new ShowMapper();
        private PlaceMapper _placeMapper = new PlaceMapper();

        public TicketEntity ToEntity(Ticket model)
        {

            return new TicketEntity()
            {
                Id = model.Id,
                Price = model.Price,
                //Show = _showMapper.ToModel(entity.Show),
                ShowId = model.ShowId,
                PlaceId = model.PlaceId,
                State = (State) model.State,
                Owner = model.Owner
            };

        }
           
            
        

        public Ticket ToModel(TicketEntity entity)
        {

            return new Ticket()
            {
                Id = entity.Id,
                Price = entity.Price,
                //Show = _showMapper.ToModel(entity.Show),
                ShowId = entity.ShowId,
                PlaceId = entity.PlaceId, //_placeMapper.ToModel(entity.Place),
                State = (Models.Enum.State) entity.State,
                Owner = entity.Owner
            };

        }
    }
}
