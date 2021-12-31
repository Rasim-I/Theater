using System;
using System.Collections.Generic;
using System.Text;
using TheaterDAL.Entities;
//using TheaterDAL.Entities.TicketTypes;

namespace TheaterDAL.IRepositories
{
    public interface ITicketRepository : IRepository<TicketEntity, Guid>
    {
        IEnumerable<TicketEntity> GetVipTickets(Guid showId);
        IEnumerable<TicketEntity> GetCommonTickets(Guid showId);
        IEnumerable<TicketEntity> GetBudgetTickets(Guid showId);

    }
}
