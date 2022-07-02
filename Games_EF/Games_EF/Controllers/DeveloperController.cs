using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Games_EF.Data;
using Games_EF.Models;
using Games_EF.Models.Developers;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace Games_EF.Controllers
{
    public class CRUDDeveloperController : Controller
    {
        private readonly Games_EFContext _context;
        private readonly ILogger<GameController> _logger;

        public CRUDDeveloperController(Games_EFContext context, ILogger<GameController> logger)
        {
            _context = context;
            _logger = logger;
        }
        // GET: CRUDDeveloperController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CRUDDeveloperController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CRUDDeveloperController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CRUDDeveloperController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,YearOfEstablishment")] DeveloperModel developer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(developer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(developer);
        }

        // GET: CRUDDeveloperController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CRUDDeveloperController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,YearOfEstablishment")] DeveloperModel developer)
        {
            if (id != developer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(developer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(developer);
        }
        
        // GET: DeveloperController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DeveloperController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Delete));
            }
            catch
            {
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var developer = await _context.Developers.FindAsync(id);
            _context.Developers.Remove(developer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        // PRIVATE
        // ====================================================================================================================
        private DeveloperModel GetDeveloper(int Id)
        {
            return _context.Developers
                    .ToList()
                    .Where(b => b.Id == Id)
                    .FirstOrDefault();
        }
    }
}
