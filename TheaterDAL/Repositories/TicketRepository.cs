using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using TheaterDAL.Entities;
using TheaterDAL.Entities.Enum;
//using TheaterDAL.Entities.TicketTypes;
using TheaterDAL.IRepositories;

namespace TheaterDAL.Repositories
{
    public class TicketRepository : Repository<TicketEntity, Guid>, ITicketRepository
    {
        public TicketRepository(TheaterContext context) : base(context)
        {
        }


        public IEnumerable<TicketEntity> GetVipTickets(Guid showId)
        {
            return db.Tickets.Where(t => t.ShowId == showId && t.Place.PlaceType == PlaceType.Vip)
                .Include(t => t.Place);
        }

        public IEnumerable<TicketEntity> GetCommonTickets(Guid showId)
        {
            return db.Tickets.Where(t => t.ShowId == showId && t.Place.PlaceType == PlaceType.Common)
                .Include(t => t.Place);
        }

        public IEnumerable<TicketEntity> GetBudgetTickets(Guid showId)
        {
            return db.Tickets.Where(t => t.ShowId == showId && t.Place.PlaceType == PlaceType.Budget)
                .Include(t => t.Place);
        }
    }
}
