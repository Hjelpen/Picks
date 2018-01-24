using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Picks.SchoolProject.Data;
using Picks.SchoolProject.Models;

namespace Picks.SchoolProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categories = _context.Categories.ToList();
           
            ViewBag.Categories = categories;

            return View();
        }

        public async Task<IActionResult> CreateCategory(string category)
        {       
            if (ModelState.IsValid)
            {
                Category Category = new Category();
                {
                    Category.Name = category;
                }
                _context.Add(Category);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");
        }

        [Route("api/create")]
        public async Task<IActionResult> SaveImage()
        {


            return RedirectToAction("Index", "Home");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}