using System;
using System.Collections.Generic;
using System.Text;
using TheaterLogic.Models.Enum;

namespace TheaterLogic.Models
{
    public class Place
    {
        private Guid _Id;

        public Guid Id
        {
            get => _Id;
            set => _Id = value;
        }

        private PlaceType _placeType;
        public PlaceType PlaceType
        {
            get => _placeType;
            set => _placeType = value;
        }

        public double PriceMultiplier { get; set; }

        public Place(PlaceType placeType, double priceMultiplier = 1)
        {
            Id = Guid.NewGuid();
            PriceMultiplier = priceMultiplier;
            PlaceType = placeType;
        }

        public Place(){}
    }
}
