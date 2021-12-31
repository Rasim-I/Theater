using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheaterAPI.Model;
using TheaterLogic.Abstraction.IServices;
using TheaterLogic.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TheaterAPI.Controllers
{
    [Route("api/tickets")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private ITicketService _ticketService;
        private LinkGenerator _linkGenerator;

        public TicketController(ITicketService ticketService, LinkGenerator linkGenerator)
        {
            _ticketService = ticketService;
            _linkGenerator = linkGenerator;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            Ticket ticket = _ticketService.GetTicket(id);
            if (ticket != null)
                return Ok(FormatTicket(ticket));
            else
                return StatusCode(400);
        }

        [HttpGet]
        [Route("owner/{customerName}")]
        public IActionResult GetByName(string customerName)
        {
            List<Ticket> customerTickets = _ticketService.GetUserTickets(customerName);
            switch (customerTickets.Count)
            {
                case 0:
                    return StatusCode(404);
                default:
                    return Ok(FormatTicketList(customerTickets));
            }
        }

        [HttpGet]
        [Route("show/{showId}")]
        public IActionResult GetByShow(Guid showId)
        {
            List<Ticket> showTickets = _ticketService.GetAvailableTickets(showId);
            switch (showTickets.Count)
            {
                case 0:
                    return StatusCode(404, "No available tickets for this show");
                default:
                    return Ok(FormatTicketList(showTickets));
            }
        }


        [HttpPatch]
        [Route("reserve")]
        public IActionResult PatchReserve([FromBody] TicketParameters ticketParameters)
        {
            Ticket ticket = _ticketService.GetTicket(ticketParameters.Id);
            if (ticket == null)
                return StatusCode(404, "Ticket not found");

            if (ticket.State == TheaterLogic.Models.Enum.State.Available)
            {
                _ticketService.ReserveTicket(ticketParameters.Id, ticketParameters.Owner);
                return StatusCode(200);
            }
            else
                return StatusCode(409, "Ticket is not available for reservation");
        }


        [HttpPatch]
        [Route("buy")]
        public IActionResult PatchBuy([FromBody] TicketParameters ticketParameters)
        {
            Ticket ticket = _ticketService.GetTicket(ticketParameters.Id);
            if (ticket == null)
                return StatusCode(404, "Ticekt not found");

            if (ticket.Owner != null && ticket.Owner.ToLower() != ticketParameters.Owner.ToLower())
                return StatusCode(409, "This ticket was reserved by other customer");

            if (ticket.State == TheaterLogic.Models.Enum.State.Reserved || ticket.State == TheaterLogic.Models.Enum.State.Available)
            {
                _ticketService.BuyTicket(ticketParameters.Id, ticketParameters.Owner);
                return StatusCode(200);
            }
            else
                return StatusCode(409, "This ticket is not available for purchase");
        }
 







        private IEnumerable<Link> CreateLinksForTicket(Ticket ticket)
        {
            var links = new List<Link>
            {
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetById), values: new { ticket.Id }),
                "self",
                "GET"),

                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetByName), values: new { ticket.Owner }),
                "self",
                "GET")
            };

            if(ticket.State == TheaterLogic.Models.Enum.State.Available)
            {
                links.Add(
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(PatchBuy)),
                "self",
                "PATCH"));

                links.Add(
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(PatchReserve)),
                "self",
                "PATCH"));
            }

            if(ticket.State == TheaterLogic.Models.Enum.State.Reserved)
                links.Add(
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(PatchBuy)),
                "self",
                "PATCH"));

            return links;
        }

        private object FormatTicket(Ticket ticket)
        {
            var FormatedTicket = new
            {
                ticket.Id,
                ticket.Owner,
                ticket.State,
                ticket.Price,
                ticket.ShowId,

                Links = CreateLinksForTicket(ticket)
            };
            return FormatedTicket;
        }

        private List<object> FormatTicketList(List<Ticket> tickets)
        {
            List<object> formatedTickets = new List<object>();
            foreach (Ticket t in tickets)
            {
                formatedTickets.Add(FormatTicket(t));
            }
            return formatedTickets;
        }


    }
}
