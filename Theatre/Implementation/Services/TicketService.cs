using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheaterDAL;
using TheaterDAL.Entities;
using TheaterDAL.Entities.Enum;
using TheaterLogic.Abstraction.IMappers;
using TheaterLogic.Abstraction.IServices;
using TheaterLogic.Implementation.Mappers;
using TheaterLogic.Models;
using Genre = TheaterLogic.Models.Enum.Genre;

//using TheaterLogic.Models.TicketTypes;

namespace TheaterLogic.Implementation.Services
{
    public class TicketService : ITicketService
    {
        private IUnitOfWork _unitOfWork;
        private IMapper<TicketEntity, Ticket> _ticketMapper;
        private IMapper<PlaceEntity, Place> _placeMapper;

        public TicketService(IUnitOfWork unitOfWork, IMapper<TicketEntity, Ticket> ticketMapper, IMapper<PlaceEntity, Place> placeMapper)
        {
            _unitOfWork = unitOfWork;
            _ticketMapper = ticketMapper;
            _placeMapper = placeMapper;
        }

        public void AddTickets(Show show, int vipTicketQuantity, int commonTicketQuantity, int budgetTicketQuantity)
        {
            Place vipPlace = _placeMapper.ToModel(_unitOfWork.Places.Find(p => p.PlaceType == PlaceType.Vip).FirstOrDefault());
            Place commonPlace = _placeMapper.ToModel(_unitOfWork.Places.Find(p => p.PlaceType == PlaceType.Common).FirstOrDefault());
            Place budgetPlace = _placeMapper.ToModel(_unitOfWork.Places.Find(p => p.PlaceType == PlaceType.Budget).FirstOrDefault());

            for (int i = 0; i < vipTicketQuantity; i++)
            {
                _unitOfWork.Tickets.Create(_ticketMapper.ToEntity(new Ticket(show, vipPlace) ));
            }
            for (int i = 0; i < commonTicketQuantity; i++)
            {
                _unitOfWork.Tickets.Create(_ticketMapper.ToEntity(new Ticket(show, commonPlace)));
            }
            for (int i = 0; i < budgetTicketQuantity; i++)
            {
                _unitOfWork.Tickets.Create(_ticketMapper.ToEntity(new Ticket(show, budgetPlace)));
                //TicketEntity ticket = _ticketMapper.ToEntity(new Ticket(show, budgetPlace));
            }

            _unitOfWork.Save();

        }

        public int GetTicketPrice(Models.Enum.PlaceType placeType)
        {
            int price = 0;
            try
            {
                price = _unitOfWork.Tickets.Find(t => t.Place.PlaceType == (PlaceType)placeType).FirstOrDefault().Price;
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException("No tickets with such place");
            }

            return price;
        }

        public List<Ticket> GetTickets(Guid ShowId)
        {
            return _unitOfWork.Tickets.GetAll().Where(t => t.ShowId == ShowId).ToList().ConvertAll(t=>_ticketMapper.ToModel(t));
        }

        public List<Ticket> GetAvailableTickets(Guid showId)
        {
            return _unitOfWork.Tickets.GetAll()
                .Where(t => t.ShowId == showId)
                .Where(t => t.State == State.Available)
                .ToList()
                .ConvertAll(t => _ticketMapper.ToModel(t));
        }

        public List<Genre> GetGenres()
        {
            List<Genre> genres = new List<Genre>();
            genres.Add(Genre.Comedy);
            genres.Add(Genre.Drama);
            genres.Add(Genre.Parody);
            return genres;
        }

        public List<Ticket> GetUserTickets(string customerName)
        {
            return _unitOfWork.Tickets.Find(t=>t.Owner == customerName).ToList().ConvertAll(t => _ticketMapper.ToModel(t));
        }

        public void BuyReserved(Guid id)
        {
            _unitOfWork.Tickets.Get(id).State = State.Bought;
            _unitOfWork.Save();
        }

        public void ReserveTicket(Guid id, string customer)
        {
            TicketEntity ticket = _unitOfWork.Tickets.Get(id);
            ticket.Owner = customer;
            ticket.State = State.Reserved;

            _unitOfWork.Save();
        }

        public void BuyTicket(Guid id, string customer)
        {
            TicketEntity ticket = _unitOfWork.Tickets.Get(id);

            if(ticket.Owner != customer)
                ticket.Owner = customer;

            ticket.State = State.Bought;

            _unitOfWork.Save();
        }

        public Ticket GetTicket(Guid Id)
        {
            TicketEntity ticket = _unitOfWork.Tickets.Get(Id);
            if (ticket != null)
                return _ticketMapper.ToModel(ticket);
            else
                return null;
           
        }

    }
}
