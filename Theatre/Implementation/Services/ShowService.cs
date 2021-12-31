using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TheaterDAL;
using TheaterDAL.Entities;
using TheaterDAL.Entities.Enum;
using TheaterLogic.Abstraction.IMappers;
using TheaterLogic.Abstraction.IServices;
using TheaterLogic.Implementation.Mappers;
using TheaterLogic.Implementation.Services.Utility;
using TheaterLogic.Models;
using TheaterLogic.Models.Enum;
using Genre = TheaterLogic.Models.Enum.Genre;
using State = TheaterDAL.Entities.Enum.State;


namespace TheaterLogic.Implementation.Services
{
    public class ShowService : IShowService
    {

        private IUnitOfWork _unitOfWork;
        private IMapper<ShowEntity, Show> _showMapper;
        private IAuthorService _authorService;
        private ITicketService _ticketService;

        public ShowService(IUnitOfWork unitOfWork, IAuthorService authorService, 
            ITicketService ticketService, IMapper<ShowEntity, Show> showMapper)
        {
            _unitOfWork = unitOfWork;
            _authorService = authorService;
            _showMapper = showMapper;
            _ticketService = ticketService;
        }


        public List<Show> GetShowList()
        {
            return new List<Show>(_unitOfWork.Shows.GetAll_IncludeAll().ToList().ConvertAll(x=> _showMapper.ToModel(x)));
        }

        public List<Show> GetShowListNoTickets()
        {
            return new List<Show>(_unitOfWork.Shows.GetAll().ToList().ConvertAll(x => _showMapper.ToModel(x)));
        }

        public Show GetShow(Guid id)
        {
            ShowEntity show = _unitOfWork.Shows.Get(id);
            if (show != null)
            {
                return _showMapper.ToModel(show);
            }
            else
            {
                return null;
            }

        }


        public void BuyTicket(Guid showId, int ticketType, string customerName)
        {
            try
            {
                TicketEntity ticket;
                if (ticketType == 1)
                {
                    ticket = _unitOfWork.Tickets.GetVipTickets(showId).First(t => t.State == State.Available);
                    ticket.State = State.Bought;
                    //.State = State.Bought; 
                }
                else if (ticketType == 2)
                {
                    ticket = _unitOfWork.Tickets.GetCommonTickets(showId).First(t => t.State == State.Available);
                    ticket.State = State.Bought; //add customer name
                }
                else
                {
                    ticket = _unitOfWork.Tickets.GetBudgetTickets(showId).First(t => t.State == State.Available);
                    ticket.State = State.Bought; //add customer name
                }

                ticket.Owner = customerName;
                _unitOfWork.Tickets.Update(ticket);
                _unitOfWork.Save();
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException("No available tickets");
            }
            //catch (Exception)
            //{
            //    throw new Exception("Problem with database");
            //}
            
        }

        public void ReserveTicket(Guid showEntityId, int ticketType, string customerName)
        {
            try
            {
                TicketEntity ticket;
                if (ticketType == 1)
                {
                    ticket = _unitOfWork.Tickets.GetVipTickets(showEntityId).First(t => t.State == State.Available);
                    ticket.State = State.Reserved; //add customer name
                }
                else if (ticketType == 2)
                {
                    ticket = _unitOfWork.Tickets.GetCommonTickets(showEntityId).First(t => t.State == State.Available);
                    ticket.State = State.Reserved; //add customer name
                }
                else
                {
                    ticket = _unitOfWork.Tickets.GetBudgetTickets(showEntityId).First(t => t.State == State.Available);
                    ticket.State = State.Reserved; //add customer name
                }

                ticket.Owner = customerName;
                _unitOfWork.Tickets.Update(ticket);
                _unitOfWork.Save();
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException("No available tickets");
            }
            catch (Exception)
            {
                throw new Exception("Problem with database");
            }

        }

        public void ProcessTicket(Guid showId, int ticketType, TicketAction action, string customerName = "Console")
        {
            //ShowEntity showEntity = _showMapper.ToEntity(show);
            if (action == TicketAction.Buy)
            {
                BuyTicket(showId, ticketType, customerName);
            }
            else
            {
                ReserveTicket(showId, ticketType, customerName);
            }

        }

        public void NewShow(ShowCreationParams showParams)
        {
            Author author = _authorService.GetAuthor(Guid.Parse(showParams.authorIdStr));
            Show show = new Show(showParams.showName, DateTime.Today , showParams.genre, author);
            _unitOfWork.Shows.Create(_showMapper.ToEntity(show));
            _unitOfWork.Save();

            _ticketService.AddTickets(show, 
                showParams.vipTicketQuantity, showParams.commonTicketQuantity, showParams.budgetTicketQuantity);

        }

        public void NewShow(ShowCreationModel showParams)
        {
            Author author = _authorService.GetAuthor(showParams.AuthorId);
            Show show = new Show(showParams.Name, showParams.ShowTime, showParams.Genre, author) { Id = showParams.Id };
            _unitOfWork.Shows.Create(_showMapper.ToEntity(show));
            _unitOfWork.Save();

            _ticketService.AddTickets(show,
                showParams.VipTicketsQuantity, showParams.CommonTicketsQuantity, showParams.BudgetTicketsQuantity);
        }


        public void RemoveShow(Guid showId)
        {
            ShowEntity show =_unitOfWork.Shows.Find(s => s.Id == showId).FirstOrDefault();
            _unitOfWork.Shows.Remove(show);
            _unitOfWork.Save();
        }


        public void NewShow(ShowCreationParams showParams, DateTime showTime)
        {
            Author author = _authorService.GetAuthor(showParams.authorId);
            Show show = new Show(showParams.showName, showTime, showParams.genre, author);
            _unitOfWork.Shows.Create(_showMapper.ToEntity(show));
            _unitOfWork.Save();

            _ticketService.AddTickets(show,
                showParams.vipTicketQuantity, showParams.commonTicketQuantity, showParams.budgetTicketQuantity);

        }


        public List<Show> FindByAuthorGenreDate(Guid authorId, Genre genre, DateTime showTime)
        {
            return _unitOfWork.Shows
                .Find(s => (s.AuthorId == authorId && s.Genre == (TheaterDAL.Entities.Enum.Genre) genre && s.ShowTime == showTime))
                .ToList()
                .ConvertAll(s=> _showMapper.ToModel(s));
        }

        public List<Show> FindByName(string name)
        {
            return _unitOfWork.Shows.Find(s => s.Name.ToLower() == name.ToLower())
                .ToList()
                .ConvertAll(s=> _showMapper.ToModel(s));
        }

        public List<Show> FindByGenre(Genre genre)
        {
            return _unitOfWork.Shows
                .Find(s => s.Genre == (TheaterDAL.Entities.Enum.Genre) genre)
                .ToList()
                .ConvertAll(s => _showMapper.ToModel(s));
        }

        public List<Show> FindByShowTime(DateTime showTime)
        {
            return _unitOfWork.Shows
                .Find(s => s.ShowTime == showTime)
                .ToList()
                .ConvertAll(s => _showMapper.ToModel(s));
        }

        public List<Show> FindByAuthor(Guid authorId)
        {
            return _unitOfWork.Shows
                .Find(s => s.AuthorId == authorId)
                .ToList()
                .ConvertAll(s => _showMapper.ToModel(s));
        }

    }
}
