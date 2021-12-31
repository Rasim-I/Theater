using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TheaterLogic.Models.Enum;

namespace TheaterLogic.Implementation.Services.Utility
{
    public class ShowCreationModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime ShowTime { get; set; }
        
        [Required]
        public Genre Genre { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        [Required]
        [Range(0, 50, ErrorMessage = "Valid range 0 - 50. Check your input")]
        public int VipTicketsQuantity { get; set; }

        [Required]
        [Range(0, 50, ErrorMessage = "Valid range 0 - 50. Check your input")]
        public int CommonTicketsQuantity { get; set; }

        [Required]
        [Range(0, 50, ErrorMessage = "Valid range 0 - 50. Check your input")]
        public int BudgetTicketsQuantity { get; set; }


    }
}
