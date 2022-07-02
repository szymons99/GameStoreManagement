﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Games_EF.Data;
using Games_EF.Models;
using Games_EF.Models.Developers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Games_EF.Controllers
{
    public class DeveloperController : Controller
    {
        private readonly Games_EFContext _context;
        private readonly ILogger<DeveloperController> _logger;

        public DeveloperController(Games_EFContext context, ILogger<DeveloperController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // ACTIONS
        // ====================================================================================================================
        public IActionResult Create()
        {
            //DeveloperModel testDeveloper = new DeveloperModel();
            //AddDeveloper(testDeveloper);
            return View();
        }

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

        public IActionResult Delete(int Id)
        {
            return View(GetDeveloper(Id));
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


        public IActionResult Details(int Id)
        {
            return View(GetDeveloper(Id));
        }

        public IActionResult Edit(int Id)
        {
            return View(GetDeveloper(Id));
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,YearOfEstablishment")] DeveloperModel developer)
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

        public IActionResult Index()
        {
            return View(_context.Developers);
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

        private void AddDeveloper(DeveloperModel newDeveloper)
        {
            _context.Developers.Add(newDeveloper);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                Console.WriteLine("Istnieje juz deweloper o takich parametrach");
            }
            catch (DbUpdateException)
            {
                Console.WriteLine("Blad przy dodawaniu nowego dewelopera do bazy.\nDane dewelopera: ID " + newDeveloper.Id + ", Name" + newDeveloper.Name + ", \nDate of Establishment: " + newDeveloper.DateOfEstablishment);
            }
        }
    }
}
