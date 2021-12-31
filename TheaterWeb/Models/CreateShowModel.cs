using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheaterWeb.Models.Enum;

namespace TheaterWeb.Models
{
    public class CreateShowModel
    {
        public string Name { get; set; }
        public DateTime ShowTime { get; set; }
        public Genre Genre { get; set; }
        public Guid AuthorId { get; set; }

        public int VipTicketQuantity { get; set; }
        public int CommonTicketQuantity { get; set; }
        public int BudgetTicketQuantity { get; set; }

    }
}
