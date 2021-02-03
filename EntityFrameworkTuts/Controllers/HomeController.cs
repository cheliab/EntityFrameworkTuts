using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EntityFrameworkTuts.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkTuts.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext _db;

        public HomeController(ApplicationContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _db.Users.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            User foundUser = await _db.Users.FirstOrDefaultAsync(user => user.Id == id);
            if (foundUser == null)
                return NotFound();

            return View(foundUser);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            User foundUser = await _db.Users.FirstOrDefaultAsync(user => user.Id == id);
            if (foundUser == null)
                return NotFound();

            return View(foundUser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            _db.Users.Update(user);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfimDelete(int? id)
        {
            if (id == null)
                NotFound();

            User foundUser = await _db.Users.FirstOrDefaultAsync(user => user.Id == id);
            if (foundUser == null)
                NotFound();

            return View(foundUser);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                NotFound();

            User foundUser = await _db.Users.FirstOrDefaultAsync(user => user.Id == id);
            if (foundUser == null)
                NotFound();

            _db.Users.Remove(foundUser);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}