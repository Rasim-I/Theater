using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TheaterDAL;
using TheaterLogic.Abstraction.IServices;
using TheaterLogic.Implementation.Services;
using TheaterLogic.Models;
using TheaterWeb.Models;
using TheaterWeb.Models.Enum;
using TicketAction = TheaterDAL.Entities.Enum.TicketAction;

namespace TheaterWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IShowService _showService;
        private IAuthorService _authorService;
        private ITicketService _ticketService;

        public HomeController(ILogger<HomeController> logger, IShowService showService, IAuthorService authorService, ITicketService ticketService)
        {
            _logger = logger;
            _showService = showService;
            _authorService = authorService;
            _ticketService = ticketService;
        }


        public IActionResult Index()
        {
            ViewBag.Genres = _ticketService.GetGenres().ConvertAll(g=>(Genre)g);
            ViewBag.Authors = _authorService.GetAll();
            ViewBag.Shows = _showService.GetShowList();

            return View();
        }

        public IActionResult Authors()
        {
            return View(_authorService.GetAll());
        }

        [HttpGet]
        public IActionResult BuyTicket(Guid? Id)
        {
            if (Id == null)
            {
                return StatusCode(404);
            }

            Show show = _showService.GetShowList().Find(s => s.Id == Id);
            ViewBag.Show = show;
            ViewBag.VipTicketPrice = _ticketService.GetTicketPrice(TheaterLogic.Models.Enum.PlaceType.Vip);
            ViewBag.CommonTicketPrice = _ticketService.GetTicketPrice(TheaterLogic.Models.Enum.PlaceType.Common);
            ViewBag.BudgetTicketPrice = _ticketService.GetTicketPrice(TheaterLogic.Models.Enum.PlaceType.Budget);
            return View();
        }

        [HttpPost]
        public IActionResult BuyTicket(TicketPurchase ticketPurchase)
        {
            _showService.ProcessTicket(ticketPurchase.ShowId, 
                (int)ticketPurchase.TicketType, (TicketAction) ticketPurchase.TicketAction, ticketPurchase.CustomerName);

            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public IActionResult Tickets()
        {
            List<Ticket> tickets = new List<Ticket>();
            return View(tickets);
        }

        [HttpPost]
        public IActionResult UserTickets(string customerName)
        {
            List<Ticket> tickets = _ticketService.GetUserTickets(customerName);

            return View("Tickets", tickets);
        }


        public IActionResult BuyReserved(Guid Id)
        {
            _ticketService.BuyReserved(Id);

            return RedirectToAction("Tickets", "Home");
        }

        [HttpPost]
        public IActionResult SearchByProperties(SearchModel searchModel)
        {
            List<Show> filteredList = _showService
                .FindByAuthorGenreDate(searchModel.AuthorId, searchModel.Genre, searchModel.ShowTime);

            ViewBag.Genres = _ticketService.GetGenres().ConvertAll(g => (Genre)g);
            ViewBag.Authors = _authorService.GetAll();
            ViewBag.Shows = filteredList;
            return View("Index");
        }

        [HttpGet]
        public IActionResult SearchByShowName(string showName)
        {
            List<Show> filteredList = _showService.FindByName(showName);

            ViewBag.Genres = _ticketService.GetGenres().ConvertAll(g => (Genre)g);
            ViewBag.Authors = _authorService.GetAll();
            ViewBag.Shows = filteredList;
            return View("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
