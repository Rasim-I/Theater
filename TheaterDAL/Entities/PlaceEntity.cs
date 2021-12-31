using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TheaterDAL.Entities.Enum;

namespace TheaterDAL.Entities
{
    [Table("Place")]
    public class PlaceEntity
    {
        public Guid Id { get; set; }

        public double PriceMultiplier { get; set; }

        [EnumDataType(typeof(PlaceType))]
        public PlaceType PlaceType { get; set; }
    }
}
