using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheaterConsolePL.App_start;
using TheaterDAL.Entities.Enum;
using TheaterLogic.Abstraction.IServices;
using TheaterLogic.Implementation.Services;
using TheaterLogic.Implementation.Services.Utility;
using TheaterLogic.Models;
using Genre = TheaterLogic.Models.Enum.Genre;
using PlaceType = TheaterLogic.Models.Enum.PlaceType;


//using TheaterLogic.Models.TicketTypes;

namespace TheaterConsolePL
{
    public class Window
    {
        private readonly IServiceFactory _serviceFactory;

        private Show currentShow;

        public Window(IServiceFactory serviceFactory, string action)
        {
            _serviceFactory = serviceFactory;

            if (action == "user")
            {
                Affiche();
            }else if (action == "admin_create")
            {
                NewShow();
            }else if (action == "admin_remove")
            {
                RemoveShow();
            }
            

        }

        public void ShowDescription(Show show) //FIX this
        {
            var _ticketService = _serviceFactory.CreateTicketService();
            int vipPrice = _ticketService.GetTicketPrice(PlaceType.Vip);
            int commonPrice = _ticketService.GetTicketPrice(PlaceType.Common);
            int budgetPrice = _ticketService.GetTicketPrice(PlaceType.Budget);

            Console.WriteLine("\n Show: {0}\n" +
                              "Genre of this show is {1}\n" +
                              "Author: {2}\n" +
                              "time of performance: {3}\n" +
                              "There are vip tickets ({4}), ordinary tickets ({5}) and economy-type tickets({6}).\n\n",
                show.Name, show.Genre.ToString(), show.Author.Name + " " + show.Author.Surname, show.ShowTime, 
                vipPrice, commonPrice, budgetPrice );

        }

        public List<Show> SearchForShow(string keyword, List<Show> showList)
        {
            List<Show> result = new List<Show>();
            foreach (Show show in showList)
            {
                if((show.Name+show.Author.Name+show.Author.Surname+show.Genre+show.ShowTime).ToLower().Contains(keyword.ToLower()))
                {
                    result.Add(show);
                }
            }
            return result;
        }

        public Show SelectShow(List<Show> showList)
        {
            Show selectedShow = null;
            Console.WriteLine("Search for show.\n" +
                              "(try search for author name/show title/show genre/show date)");

            string searchCondition = Console.ReadLine();

            List<Show> matchingShows = SearchForShow(searchCondition, showList);

            switch (matchingShows.Count)
            {
                case 1:
                    selectedShow = matchingShows.First();
                    Console.WriteLine("Show found.");
                    break;
                case 0:
                    Console.WriteLine("No show matching this criteria. Try again.");
                    return SelectShow(showList);

                default:
                    Console.WriteLine("There are several shows that satisfy the search criteria:");
                    foreach (Show show in matchingShows)
                    {
                        ShowDescription(show);
                    }
                    return SelectShow(matchingShows);
            }
            return selectedShow;
        }

        public void BuyTicket()
        {
            var ticketService = _serviceFactory.CreateTicketService();
            int vipTicketPrice = ticketService.GetTicketPrice(PlaceType.Vip);
            int commonTicketPrice = ticketService.GetTicketPrice(PlaceType.Common);
            int budgetTicketPrice = ticketService.GetTicketPrice(PlaceType.Budget);

            Console.WriteLine("Press '1' to select vip ticket, then '1' to buy it for {0}, or '2' to reserve it\n" +
                              "Press '2' to select ordinary ticket, then '1' to buy it for {1}, or '2' to reserve it\n" +
                              "Press '3' to select budget ticket, then '1' to buy it for {2}, or '2' to reserve it", 
                vipTicketPrice, commonTicketPrice, budgetTicketPrice);

            string ticketTypeSelected = Console.ReadLine();
            string ticketAction = Console.ReadLine();
            TicketAction action = TicketAction.Buy;

            if (ticketAction == "1")
                action = TicketAction.Buy;
            else if (ticketAction == "2")
                action = TicketAction.Reserve;
            else
            {
                Console.WriteLine("Incorrect input. Try again.");
                BuyTicket();
            }
            

            bool transactionResult = false;
            var showService = _serviceFactory.CreateShowService();

            switch (ticketTypeSelected)
            {
                case "1":
                    showService.ProcessTicket(currentShow.Id, 1, action);
                    transactionResult = true;
                    break;
                case "2":
                    showService.ProcessTicket(currentShow.Id, 2, action);
                    transactionResult = true;
                    break;
                case "3":
                    showService.ProcessTicket(currentShow.Id, 3, action);
                    transactionResult = true;
                    break;
                default:
                    Console.WriteLine("Try again.");
                    BuyTicket();
                    break;
            }

            if (transactionResult == true)
            {
                Console.WriteLine("Operation completed successfully. Have a good time.");
            }
            else
                Console.WriteLine("There is a problem with ticket availability.");

        }

        public void Affiche()
        {
            var showService = _serviceFactory.CreateShowService();
            foreach (Show show in showService.GetShowList())
            {
                ShowDescription(show);
            }

            currentShow = SelectShow(showService.GetShowList());
            BuyTicket();
        }


        public void NewShow()
        {
            var authorService = _serviceFactory.CreateAuthorService();
            var showService = _serviceFactory.CreateShowService();

            Console.WriteLine("Choose author by Id:");
            
            foreach (Author author in authorService.GetAll())
            {
                Console.WriteLine("Id: " + author.Id +" " + author.Name+ " " + author.Surname);
            }
            string selectedAuthorId = Console.ReadLine();
            

            Console.WriteLine("Type show name");
            string showName = Console.ReadLine();

            Console.WriteLine("Type show genre. (Capital letter first)"); ////////
            Genre genre = (Genre)Enum.Parse(typeof(Genre), Console.ReadLine());

            Console.WriteLine("How many tickets are available? Enter 3 values, that represent the number of tickets. From vip to budget.");
            int vipTicketQuantity = Convert.ToInt32(Console.ReadLine());
            int commonTicketQuantity = Convert.ToInt32(Console.ReadLine());
            int budgetTicketQuantity = Convert.ToInt32(Console.ReadLine());


            ShowCreationParams showParams = new ShowCreationParams(selectedAuthorId, showName, genre,
                vipTicketQuantity, commonTicketQuantity, budgetTicketQuantity);
            showService.NewShow(showParams);
            
        }

        public void RemoveShow()
        {
            var showService = _serviceFactory.CreateShowService();

            Console.WriteLine("To remove show, select it first (try search for author name/show title/show genre");
            foreach (Show show in showService.GetShowList())
            {
                ShowDescription(show);
            }

            currentShow = SelectShow(showService.GetShowList());
            if (currentShow != null)
            {
                showService.RemoveShow(currentShow.Id);
            }
            else
            {
                RemoveShow();
            }
            
            

        }

    }
}
