using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Permissions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TheaterLogic.Abstraction.IServices;
using TheaterLogic.Models;
using TheaterLogic.Models.Enum;
using TheaterAPI.Model;
using TheaterAPI.Model.QueryProcessing;
using TheaterLogic.Implementation.Services.Utility;
using Microsoft.AspNetCore.Routing;
using System.Text.Json.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TheaterAPI.Controllers
{
    [Route("api/shows")]
    [ApiController]
    public class ShowController : ControllerBase
    {
        private IShowService _showService;
        private IAuthorService _authorService;
        private LinkGenerator _linkGenerator;

        public ShowController(IShowService showService, IAuthorService authorService, LinkGenerator linkGenerator)
        {
            _showService = showService;
            _authorService = authorService;
            _linkGenerator = linkGenerator;
        }


        [HttpGet]
        public IActionResult Get([FromQuery]ShowParameters showParameters)
        {
            QueryHandler byName = new NameHandler();
            QueryHandler byGenre = new GenreHandler();
            QueryHandler byDate = new DateHandler();
            QueryHandler byAuthor = new AuthorHandler();

            byName.Successor = byAuthor;
            byAuthor.Successor = byGenre;
            byGenre.Successor = byDate;

            List<Show> result = byName.HandleRequest(showParameters, _showService, _showService.GetShowList() );

            if (result.Count > 0)    
                return Ok(FormatShowList(result));
            else
                return NotFound();
        }

        

        [HttpGet("{id}",Name = "GetShowById")]
        public IActionResult GetById(Guid id)
        {
            Show show = _showService.GetShow(id);

            if (show == null)
            {
                return NotFound();
            }

            return Ok(FormatShow(show));
        }


        [HttpPost]
        public IActionResult Post([FromBody] ShowCreationModel showParams)
        {
            try
            {
                Show show = _showService.GetShow(showParams.Id);
                Author author = _authorService.GetAuthor(showParams.AuthorId);
                if(show == null && author != null)
                {
                    _showService.NewShow(showParams);
                    
                }else
                    return StatusCode(409, "Conflict. Check your input.");
            }
            catch
            {
                return StatusCode(500);
            }

            return StatusCode(201);
        }

        //[Route("customers/{id:int}")]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var showToDelete = _showService.GetShow(id);

                if (showToDelete == null)
                {
                    return NotFound();
                }
                _showService.RemoveShow(id);
                return StatusCode(204);
            }
            catch
            {
                return StatusCode(500);
            }
        }





        private IEnumerable<Link> CreateLinksForShow(Show show)
        {
            var links = new List<Link>
            {
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetById), values: new { show.Id }),
                "self",
                "GET"),

                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(Get), values: new { show.Id, show.Name,
                    show.Genre, show.AuthorId, show.ShowTime}),
                "self",
                "GET"),

                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(Delete), values: new { show.Id }),
                "delete_owner",
                "DELETE"),

                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(Post), values: new { show.Id }),
                "create_show",
                "POST")
            };

            return links;
        }

        private object FormatShow(Show show)
        {
            var FormatedShow = new
            {
                show.Id,
                show.Name,
                show.Genre,
                show.Author,
                show.ShowTime,
                Links = CreateLinksForShow(show)
            };
            return FormatedShow;
        }

        private List<object> FormatShowList(List<Show> shows)
        {
            List<object> formatedShows = new List<object>();
            foreach(Show s in shows)
            {
                formatedShows.Add(FormatShow(s));
            }
            return formatedShows;
        }
    }
}
