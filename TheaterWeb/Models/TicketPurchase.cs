using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheaterWeb.Models.Enum;

namespace TheaterWeb.Models
{
    public class TicketPurchase
    {
        public Guid ShowId { get; set; }

        public string CustomerName { get; set; }

        public PlaceType TicketType { get; set; }

        public TicketAction TicketAction { get; set; }
    }
}
