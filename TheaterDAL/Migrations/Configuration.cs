using System.Collections.Generic;
using System.Data.Entity.Migrations;
using TheaterDAL.Entities;
using TheaterDAL.Entities.Enum;
//using TheaterDAL.Entities.TicketTypes;

namespace TheaterDAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    //using Microsoft.EntityFrameworkCore;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TheaterDAL.TheaterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "TheaterDAL.TheaterContext";
        }

        protected override void Seed(TheaterDAL.TheaterContext context)
        {

            
            List<AuthorEntity> storedAuthors = new List<AuthorEntity>();
            List<TicketEntity> storedTickets = new List<TicketEntity>();
            List<ShowEntity> storedShows = new List<ShowEntity>();

            AuthorEntity author1 = new AuthorEntity()
                {Id = Guid.NewGuid(), Name = "Joe", PhoneNumber = 12345, Surname = "Johnson"};
            AuthorEntity author2 = new AuthorEntity()
                {Id = Guid.NewGuid(), Name = "William", PhoneNumber = 54321, Surname = "Saylor"};

            storedAuthors.Add(author1);
            storedAuthors.Add(author2);



            PlaceEntity vipPlace = new PlaceEntity()
                { Id = Guid.NewGuid(), PlaceType = PlaceType.Vip, PriceMultiplier = 3 };

            PlaceEntity commonPlace = new PlaceEntity()
                {Id = Guid.NewGuid(), PlaceType = PlaceType.Common, PriceMultiplier = 1.5};

            PlaceEntity budgetPlace = new PlaceEntity()
                {Id = Guid.NewGuid(), PlaceType = PlaceType.Budget, PriceMultiplier = 0.5};


            TicketEntity vipTicket1 = new TicketEntity()
                {Id = Guid.NewGuid(), State = State.Available, Place = vipPlace, Price = 300};
            TicketEntity commonTicket1 = new TicketEntity()
                {Id = Guid.NewGuid(), State = State.Available, Place = commonPlace, Price = 150};
            TicketEntity budgetTicket1 = new TicketEntity()
                {Id = Guid.NewGuid(), State = State.Available, Place = budgetPlace, Price = 50};

            

            List<TicketEntity> ticketList = new List<TicketEntity>();
            ticketList.Add(vipTicket1);
            ticketList.Add(commonTicket1);
            ticketList.Add(budgetTicket1);


            TicketEntity vipTicket2 = new TicketEntity()
                { Id = Guid.NewGuid(), State = State.Available, Place = vipPlace, Price = 300};
            TicketEntity commonTicket2 = new TicketEntity()
                { Id = Guid.NewGuid(), State = State.Available, Place = commonPlace, Price = 150};
            TicketEntity budgetTicket2 = new TicketEntity()
                { Id = Guid.NewGuid(), State = State.Available, Place = budgetPlace, Price = 50};

            List<TicketEntity> ticketList2 = new List<TicketEntity>();
            ticketList2.Add(vipTicket2);
            ticketList2.Add(commonTicket2);
            ticketList2.Add(budgetTicket2);


            storedShows.Add(new ShowEntity()
            {
                Id = Guid.NewGuid(), 
                Name = "Show about something interesting", 
                AuthorId = author1.Id,
                Genre = Genre.Comedy, 
                ShowTime = new DateTime(2021, 11, 25), 
                TicketList = ticketList
            });

            storedShows.Add(new ShowEntity()
            {
                Id = Guid.NewGuid(),
                Name = "Very sad story",
                AuthorId = author2.Id,
                Genre = Genre.Drama,
                ShowTime = new DateTime(2021, 11, 27),
                TicketList = ticketList2
            });

            foreach (AuthorEntity author in storedAuthors)
            {
                context.Authors.Add(author);
            }

            
            foreach (TicketEntity ticket in storedTickets)
            {
                context.Tickets.Add(ticket);
            }
            
            foreach (ShowEntity show in storedShows)
            {
                context.Shows.Add(show);
            }
            
            context.SaveChanges();
            
        }
            
    }
            
}
