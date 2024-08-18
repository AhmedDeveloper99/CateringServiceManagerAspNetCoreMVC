using CateringServiceManagerAspNetCoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CateringServiceManagerAspNetCoreMVC
{
    public class DaigController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DaigController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Daig
        public IActionResult Index()
        {
            // Retrieve and display notification message if available
            ViewBag.Notification = TempData["Notification"];
            // Fetch and return the list of Daigs from the database
            return View(_context.tbl_daig.ToList());
        }

        // GET: Daig/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Daig/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Status")] Daig daig)
        {
            if (ModelState.IsValid)
            {
                _context.Add(daig);
                _context.SaveChanges();

                // Set notification for successful creation
                TempData["Notification"] = $"Daig '{daig.Name}' has been added successfully!";
                return RedirectToAction(nameof(Index));
            }
            // Return the view with the model to show validation errors if any
            return View(daig);
        }

        // GET: Daig/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var daig = _context.tbl_daig.Find(id);
            if (daig == null)
            {
                return NotFound();
            }
            return View(daig);
        }

        // POST: Daig/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("DaigId,Name,Status")] Daig daig)
        {
            if (id != daig.DaigId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(daig);
                _context.SaveChanges();

                // Set customized notification based on Daig's status
                switch (daig.Status)
                {
                    case "Unavailable":
                        TempData["Notification"] = $"Daig '{daig.Name}' is now unavailable.";
                        break;
                    case "Idle":
                        TempData["Notification"] = $"Daig '{daig.Name}' is now idle.";
                        break;
                    case "In Use":
                        TempData["Notification"] = $"Daig '{daig.Name}' is now in use.";
                        break;
                    default:
                        TempData["Notification"] = $"Daig '{daig.Name}' has been updated successfully!";
                        break;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(daig);
        }

        // GET: Daig/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var daig = _context.tbl_daig.FirstOrDefault(m => m.DaigId == id);
            if (daig == null)
            {
                return NotFound();
            }

            return View(daig); // Returns the Delete view
        }

        // POST: Daig/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var daig = _context.tbl_daig.Find(id);
            if (daig == null)
            {
                return NotFound();
            }

            _context.tbl_daig.Remove(daig);
            _context.SaveChanges();

            // Set notification for successful deletion
            TempData["Notification"] = $"Daig '{daig.Name}' has been deleted successfully from inventory!";
            return RedirectToAction(nameof(Index));
        }
    }
}
