using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TheaterDAL.Entities.Enum;

namespace TheaterDAL.Entities
{
    [Table("Show")]
    public class ShowEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime ShowTime { get; set; }

        [EnumDataType(typeof(Genre))]
        public Genre Genre { get; set; }

        [ForeignKey(nameof(Author))]
        public Guid AuthorId { get; set; }
        public virtual AuthorEntity Author { get; set; }

        public virtual List<TicketEntity> TicketList { get; set; }

    }
}
