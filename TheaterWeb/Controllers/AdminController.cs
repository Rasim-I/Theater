using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheaterLogic.Abstraction.IServices;
using TheaterLogic.Implementation.Services.Utility;
using TheaterLogic.Models;
using TheaterLogic.Models.Enum;
using TheaterWeb.Models;

namespace TheaterWeb.Controllers
{
    public class AdminController : Controller
    {

        private IShowService _showService;
        private IAuthorService _authorService;

        public AdminController(IShowService showService, IAuthorService authorService)
        {
            _showService = showService;
            _authorService = authorService;
        }

        public IActionResult Index()
        {

            List<Show> showList = _showService.GetShowList();
            //display show list with delete button
            return View(showList);
        }


        [HttpGet]
        public IActionResult CreateShow()
        {
            List<Author> authors = _authorService.GetAll();
            ViewBag.Authors = authors;
            return View();
        }

        [HttpPost]
        public IActionResult CreateShow(CreateShowModel createShowModel)
        {
           //return (createShowModel.AuthorId.ToString() + " " +createShowModel.BudgetTicketQuantity.ToString() + " " +createShowModel.CommonTicketQuantity.ToString()+ " "+
            //                  createShowModel.Name + createShowModel.ShowTime + createShowModel.Genre);

            ShowCreationParams showCreationParams = new ShowCreationParams(createShowModel.AuthorId,
                createShowModel.Name,
                (Genre) createShowModel.Genre, createShowModel.VipTicketQuantity,
                createShowModel.CommonTicketQuantity, createShowModel.BudgetTicketQuantity);
            _showService.NewShow(showCreationParams, createShowModel.ShowTime);

            return RedirectToAction("Index", "Home");
            //View("~/Views/Admin/CreateShow.cshtml");
        }



        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            Show show = _showService.GetShow(id);
            if (show == null)
            {
                return StatusCode(404);
            }
            return View(show);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Show show = _showService.GetShow(id);
            if (show == null)
            {
                return StatusCode(404);
            }

            _showService.RemoveShow(id);
            return RedirectToAction("Index", "Home");
        }



        /*
        // GET: AdminController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AdminController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AdminController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        */
    }
}
