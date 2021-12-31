using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using TheaterLogic.Models.Enum;

namespace TheaterLogic.Models
{
    public class Ticket
    {
        private Guid _Id;
        public Guid Id
        {
            get => _Id;
            set => _Id = value;
        }

        private int _price = 100;
        public int Price
        {
            get => _price;
            set => _price = value;
        }

        public Guid? ShowId;
        private Show _show;
        public Show Show
        {
            get => _show;
            set
            {
                _show = value;
                ShowId = _show.Id;
            }
        }

        public Guid PlaceId;
        private Place _place;
        public Place Place
        {
            get => _place;
            set
            {
                _place = value;
                PlaceId = _place.Id;
            }
        }

        private State _state;
        public State State
        {
            get => _state;
            set => _state = value;
        }

        /*
        private bool _isBought;
        public bool IsBought
        {
            get => _isBought;
            set => _isBought = value;
        }

        private bool _isReserved;
        public bool IsReserved
        {
            get => _isReserved;
            set => _isReserved = value;
        }
        */

        private string _owner;
        public string Owner
        {
            get => _owner;
            set => _owner = value;
        }

        public Ticket(Show show, Place place)
        {
            Id = Guid.NewGuid();
            Show = show;
            Place = place;
            Price = (int)Math.Round(_price * Place.PriceMultiplier);
            State = State.Available;
        }

        public Ticket(){}
    }
}
