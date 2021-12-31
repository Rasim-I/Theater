using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheaterLogic.Models.Enum;

namespace TheaterWeb.Models
{
    public class SearchModel
    {
        public DateTime ShowTime { get; set; }
        public Guid AuthorId { get; set; }
        public Genre Genre { get; set; }


    }
}
