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
using Games_EF.Models.Awards;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Games_EF.Controllers
{
    public class AwardController : Controller
    {
        private readonly Games_EFContext _context;
        private readonly ILogger<GameController> _logger;

        public AwardController(Games_EFContext context, ILogger<GameController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // ACTIONS
        // ====================================================================================================================
        public IActionResult Create()
        {
            PopulateGamesDropDownList();
            return View();
        }

        public IActionResult Delete(int Id)
        {
            return View(_context.Awards.Find(Id));
        }

        public IActionResult Details(int Id)
        {
            return View(_context.Awards.Find(Id));
        }

        public IActionResult Edit(int Id)
        {
            var award = _context.Awards.Find(Id);
            PopulateGamesDropDownList(award.GameId);
            //ViewBag.GameName = game.Game.Name;
            return View(award);
        }

        public IActionResult Index()
        {
            return View(_context.Awards);
        }


        // DB CONFIRMED ACTIONS
        // ====================================================================================================================
        // source:
        // https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/updating-related-data-with-the-entity-framework-in-an-asp-net-mvc-application
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,GameId")] AwardModel award)
        {
            if (ModelState.IsValid)
            {
                _context.Add(award);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(award);
        }
        //public ActionResult Create([Bind(Include = "CourseID,Title,Credits,DepartmentID")] Course course) {

        // source:
        // https://docs.microsoft.com/pl-pl/aspnet/mvc/overview/getting-started/introduction/examining-the-details-and-delete-methods
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var award = await _context.Awards.FindAsync(id);
            _context.Awards.Remove(award);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,GameId")] AwardModel award)
        {
            if (id != award.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(award);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(award);
        }

        // PRIVATE
        // ====================================================================================================================
        // source:
        // https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/updating-related-data-with-the-entity-framework-in-an-asp-net-mvc-application#get-the-code
        private void PopulateGamesDropDownList(object selectedGame = null)
        {
            var gameQuery = from a in _context.Games
                            orderby a.Title
                                 select a;
            ViewBag.GameId = new SelectList(gameQuery, "Id", "Title", selectedGame);
        }


        // ERROR RESPONSE
        // ====================================================================================================================
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
