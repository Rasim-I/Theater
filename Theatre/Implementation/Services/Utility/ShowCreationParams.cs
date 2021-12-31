using System;
using System.Collections.Generic;
using System.Text;
using TheaterLogic.Models.Enum;

namespace TheaterLogic.Implementation.Services.Utility
{
    public class ShowCreationParams
    {
        public Guid showId { get; set; }
        public readonly string authorIdStr;
        public readonly Guid authorId;
        public readonly string showName;
        public readonly Genre genre;
        public readonly int vipTicketQuantity;
        public readonly int commonTicketQuantity;
        public readonly int budgetTicketQuantity;

        public ShowCreationParams(string authorIdStr, string showName, Genre genre,
            int vipTicketQuantity, int commonTicketQuantity, int budgetTicketQuantity)
        {
            this.authorIdStr = authorIdStr;
            this.showName = showName;
            this.genre = genre;
            this.vipTicketQuantity = vipTicketQuantity;
            this.commonTicketQuantity = commonTicketQuantity;
            this.budgetTicketQuantity = budgetTicketQuantity;
        }

        public ShowCreationParams(Guid authorId, string showName, Genre genre,
            int vipTicketQuantity, int commonTicketQuantity, int budgetTicketQuantity)
        {
            this.authorId = authorId;
            this.showName = showName;
            this.genre = genre;
            this.vipTicketQuantity = vipTicketQuantity;
            this.commonTicketQuantity = commonTicketQuantity;
            this.budgetTicketQuantity = budgetTicketQuantity;
        }

    }
}
