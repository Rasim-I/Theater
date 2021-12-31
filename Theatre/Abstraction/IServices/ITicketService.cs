using System;
using System.Collections.Generic;
using System.Text;
using TheaterLogic.Models;
using TheaterLogic.Models.Enum;


namespace TheaterLogic.Abstraction.IServices
{
    public interface ITicketService
    {
        public void AddTickets(Show show, int vipTicketQuantity, int commonTicketQuantity, int budgetTicketQuantity);
        public int GetTicketPrice(PlaceType place);
        public List<Ticket> GetTickets(Guid showId);
        public List<Ticket> GetAvailableTickets(Guid showId);
        public Ticket GetTicket(Guid Id);
        public List<Genre> GetGenres();
        public List<Ticket> GetUserTickets(string customerName);
        public void BuyReserved(Guid id);
        public void BuyTicket(Guid id, string customer);
        public void ReserveTicket(Guid id, string customer);
        

    }
}
