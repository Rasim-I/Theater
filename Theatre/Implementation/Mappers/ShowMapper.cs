using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheaterDAL.Entities;
using TheaterDAL.Entities.Enum;
using TheaterLogic.Abstraction.IMappers;
using TheaterLogic.Models;

namespace TheaterLogic.Implementation.Mappers
{
    public class ShowMapper : IMapper<ShowEntity, Show>
    {
        private IMapper<AuthorEntity, Author> _authorMapper = new AuthorMapper();
        private IMapper<TicketEntity, Ticket> _ticketMapper = new TicketMapper();


        public ShowEntity ToEntity(Show model)
        {
            return new ShowEntity
            {
                Id = model.Id,
                Name = model.Name,
                ShowTime = model.ShowTime,
                Genre = (Genre) model.Genre,
                //Author = _authorMapper.ToEntity(model.Author),
                AuthorId = model.AuthorId,
                TicketList = new List<TicketEntity>(model.TicketList.ConvertAll(m=>_ticketMapper.ToEntity(m)))

            };
        }

        public Show ToModel(ShowEntity entity)
        {
            return new Show
            {
                Id = entity.Id,
                Name = entity.Name,
                ShowTime = entity.ShowTime,
                Genre = (Models.Enum.Genre) entity.Genre,
                Author = _authorMapper.ToModel(entity.Author),
                //AuthorId = entity.AuthorId,
                TicketList = new List<Ticket>(entity.TicketList.ConvertAll(e=>_ticketMapper.ToModel(e)))
            };
        }
    }
}
