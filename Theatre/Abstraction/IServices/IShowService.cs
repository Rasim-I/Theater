using System;
using System.Collections.Generic;
using System.Text;
using TheaterDAL;
using TheaterDAL.Entities.Enum;
using TheaterLogic.Implementation.Services;
using TheaterLogic.Implementation.Services.Utility;
using TheaterLogic.Models;
using TheaterLogic.Models.Enum;
using Genre = TheaterLogic.Models.Enum.Genre;

//using TheaterLogic.Models.TicketTypes;

namespace TheaterLogic.Abstraction.IServices
{

    public interface IShowService
    {
        List<Show> GetShowList();
        List<Show> GetShowListNoTickets();
        public void ProcessTicket(Guid showId, int ticketType, TicketAction action, string customerName = "Console");

        public void NewShow(ShowCreationParams showParams);

        public void NewShow(ShowCreationParams showParams, DateTime showTime);

        public void NewShow(ShowCreationModel showParams);

        public Show GetShow(Guid id);

        public void RemoveShow(Guid showId);

        public List<Show> FindByAuthorGenreDate(Guid authorId, Genre genre, DateTime showTime);

        public List<Show> FindByName(string name);

        public List<Show> FindByGenre(Genre genre);

        public List<Show> FindByShowTime(DateTime showTime);

        public List<Show> FindByAuthor(Guid authorId);



    }
}
