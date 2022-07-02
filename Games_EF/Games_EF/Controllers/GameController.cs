using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Games__MVN_EF_Razor.Models.GameStore;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage;
using Games_EF.Data;
using Games_EF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace Games_EF.Controllers
{
    public class GameController : Controller
    {
        private readonly Games_EFContext _context;
        private readonly ILogger<GameController> _logger;

        public GameController(Games_EFContext context, ILogger<GameController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // ACTIONS
        // ====================================================================================================================
        public IActionResult Create()
        {
            PopulateDevelopersDropDownList();
            return View();
        }

        public IActionResult Delete(int Id)
        {
            return View(_context.Games.Find(Id));
        }

        public IActionResult Details(int Id)
        {
            return View(_context.Games.Find(Id));
        }

        public IActionResult Edit(int Id)
        {
            var game = _context.Games.Find(Id);
            PopulateDevelopersDropDownList(game.DeveloperId);
            //ViewBag.DeveloperName = game.Developer.Name;
            return View(game);
        }

        public IActionResult Index()
        {
            return View(_context.Games);
        }


        // DB CONFIRMED ACTIONS
        // ====================================================================================================================
        // source:
        // https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/updating-related-data-with-the-entity-framework-in-an-asp-net-mvc-application
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,DeveloperId,Description,Price")] GameModel game)
        {
            if(ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }
        public ActionResult CreateConfirmed(GameModel model)
        {
            GameModel game = new GameModel();
            try
            {
                game.Developer = _context.Developers.Find(model.DeveloperId);
                game.Title = model.Title;
                game.Description = model.Description;
                game.Published = model.Published;
                game.Price = model.Price;


                if (ModelState.IsValid)
                {
                    _context.Games.Add(game);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            PopulateDevelopersDropDownList(game.Developer.Id);
            return View(game);
        }

        // source:
        // https://docs.microsoft.com/pl-pl/aspnet/mvc/overview/getting-started/introduction/examining-the-details-and-delete-methods
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var station = await _context.Games.FindAsync(id);
            _context.Games.Remove(station);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //public ActionResult DeleteConfirmed(int Id)
        //{
        //    var game = _context.Games.Find(Id);
        //    _context.Games.Remove(game);
        //    _context.SaveChanges();
        //    return RedirectToAction("Index");
        //}



        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Title,DeveloperId,Description,Price")] GameModel game)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }
        public ActionResult EditConfirmed(GameModel model)
        {

            GameModel game = _context.Games.Find(model.Id);
            try
            {
                game.Developer = _context.Developers.Find(model.DeveloperId);
                game.Title = model.Title;
                game.Description = model.Description;
                game.Published = model.Published;
                game.Price = model.Price;


                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }

            PopulateDevelopersDropDownList(game.DeveloperId);
            return View(game);
        }


        // PRIVATE
        // ====================================================================================================================
        // source:
        // https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/updating-related-data-with-the-entity-framework-in-an-asp-net-mvc-application#get-the-code
        private void PopulateDevelopersDropDownList(object selectedDeveloper = null)
        {
            var developerQuery = from a in _context.Developers
                              orderby a.Name
                              select a;
            ViewBag.DeveloperId = new SelectList(developerQuery, "Id", "Name", selectedDeveloper);
        }


        // ERROR RESPONSE
        // ====================================================================================================================
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
}
