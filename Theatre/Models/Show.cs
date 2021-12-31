using System;
using System.Collections.Generic;
using TheaterLogic.Models.Enum;

namespace TheaterLogic.Models
{

    public class Show
    {
        private Guid _Id;
        public Guid Id
        {
            get => _Id;
            set => _Id = value;
        }

        private string _name;
        public string Name
        {
            get => _name; 
            set => _name = value;
        }

        private DateTime _showTime;

        public DateTime ShowTime
        {
            get => _showTime;
            set => _showTime = value;
        }

        private Genre _genre;
        public Genre Genre
        {
            get => _genre;
            set => _genre = value;
        }

        private Author _author;
        public Author Author
        {
            get => _author;
            set
            {
                _author = value;
                AuthorId = value.Id;
            }
        } 
        private Guid _authorId;
        public Guid AuthorId
         {
             get => _authorId;
             set => _authorId = value;
         }

        private List<Ticket> _ticketList;
        public List<Ticket> TicketList
        {
            get => _ticketList;
            set => _ticketList = value;
        }

        public Show(string name, DateTime showTime, Genre genre, Author author)
        {
            Id = Guid.NewGuid();
            Name = name;
            ShowTime = showTime;
            Genre = genre;
            Author = author;
            AuthorId = author.Id;
            _ticketList = new List<Ticket>();
        }

        public Show(){}
    }
}
