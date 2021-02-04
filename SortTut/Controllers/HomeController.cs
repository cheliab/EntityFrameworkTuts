using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SortTut.Data;
using SortTut.Models;

namespace SortTut.Controllers
{
    public class HomeController : Controller
    {
        private UsersContext db;

        public HomeController(UsersContext context)
        {
            db = context;

            if (!db.Companies.Any())
                InitData.Start(context);
        }

        public async Task<IActionResult> Index(UserSortState sortOrder = UserSortState.NameAsc)
        {
            IQueryable<User> users = db.Users.Include(user => user.Company);

            ViewBag.NameSort = sortOrder == UserSortState.NameAsc ? UserSortState.NameDecs : UserSortState.NameAsc;
            ViewBag.AgeSort = sortOrder == UserSortState.AgeAsc ? UserSortState.AgeDesc : UserSortState.AgeAsc;
            ViewBag.CompSort = sortOrder == UserSortState.CompanyAsc ? UserSortState.CompanyDesc : UserSortState.CompanyAsc;

            users = sortOrder switch
            {
                UserSortState.NameDecs => users.OrderByDescending(user => user.Name),
                UserSortState.AgeAsc => users.OrderBy(user => user.Age),
                UserSortState.AgeDesc => users.OrderByDescending(user => user.Age),
                UserSortState.CompanyAsc => users.OrderBy(user => user.Company.Name),
                UserSortState.CompanyDesc => users.OrderByDescending(user => user.Company.Name),
                _ => users.OrderBy(user => user.Name),
            };
            
            return View(await users.AsNoTracking().ToListAsync());
        }
    }
}