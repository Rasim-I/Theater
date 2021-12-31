using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Text;
using TheaterDAL.Entities.Enum;

namespace TheaterDAL.Entities
{
    [Table("Ticket")]
    public class TicketEntity
    {
        public Guid Id { get; set; }

        public int Price { get; set; }

        [ForeignKey(nameof(Show))]
        public Guid? ShowId { get; set; }
        public ShowEntity Show { get; set; }

        [ForeignKey(nameof(Place))]
        public Guid PlaceId { get; set; }
        public PlaceEntity Place { get; set; }

        public string Owner { get; set; }
        public State State { get; set; }



    }
}
