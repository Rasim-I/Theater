using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheaterLogic.Models.Enum;

namespace TheaterAPI.Model
{
    public class ShowParameters
    {
        public string ShowName { get; set; }
        public Guid AuthorId { get; set; }
        public string Genre { get; set; }
        public DateTime ShowTime { get; set; }

    }
}
