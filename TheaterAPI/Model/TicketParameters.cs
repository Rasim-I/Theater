using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheaterAPI.Model
{
    public class TicketParameters
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Owner { get; set; }

    }
}
