using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using TheaterLogic.Abstraction.IServices;
using TheaterLogic.Models;
using TheaterAPI.Model;
using Microsoft.AspNetCore.Routing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TheaterAPI.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private IAuthorService _authorService;
        private LinkGenerator _linkGenerator;

        public AuthorController(IAuthorService authorService, LinkGenerator linkGenerator)
        {
            _authorService = authorService;
            _linkGenerator = linkGenerator;
        }

        // GET: api/<AuthorController>
        [HttpGet]
        public IActionResult Get(string authorName = "All")
        {
            if (authorName == null || authorName.ToLower() == "all")
            {
                return Ok(FormatAuthorList(_authorService.GetAll()));
            }

            List<Author> authors = _authorService.FindByName(authorName);
            switch (authors.Count)
            {
                case 0:
                    return NotFound(404);
                default:
                    return Ok(FormatAuthorList(authors));
            }
        }

        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        { 
            Author author = _authorService.GetAuthor(id);
            if (author == null)
            {
                return StatusCode(404);
            }

            return Ok(FormatAuthor(author));
        }

        // POST api/<AuthorController>
        //        [Route("create")]
        [HttpPost]
        public IActionResult Post([FromBody] Author authorToCreate)
        {
            Author author = _authorService.GetAuthor(authorToCreate.Id);
            if (author == null)
            {
                _authorService.AddAuthor(authorToCreate);
                return StatusCode(201);
            }
            else
            {
                return StatusCode(400);
            }
        }


        private IEnumerable<Link> CreateLinksForAuthor(Author author)
        {
            var links = new List<Link>
            {
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetById), values: new { author.Id }),
                "self",
                "GET"),

                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(Get), values: new { author.Name }),
                "self",
                "GET"),

                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(Post)),
                "create_author",
                "POST")
            };

            return links;
        }

        private object FormatAuthor(Author author)
        {
            var FormatedAuthor = new
            {
                author.Id,
                author.Name,
                author.Surname,
                author.PhoneNumber,
                Links = CreateLinksForAuthor(author)
            };
            return FormatedAuthor;
        }

        private List<object> FormatAuthorList(List<Author> authors)
        {
            List<object> formatedAuthors = new List<object>();
            foreach(Author a in authors)
            {
                formatedAuthors.Add(FormatAuthor(a));
            }
            return formatedAuthors;
        }

    }
}
