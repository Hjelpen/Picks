using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Picks.SchoolProject.Data;
using Picks.SchoolProject.Models;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Picks.SchoolProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _hostingEnvironment = environment;
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
                var newCategory = new Category();
                {
                    newCategory.Name = category;
                }
                _context.Categories.Add(newCategory);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");
        }

        [Route("api/file-upload/image")]
        public async Task<IActionResult> SaveImage(IFormFile file, string name)
        {
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images");
            var guid = Guid.NewGuid();
            var filePath = Path.Combine(uploads, guid + file.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var category = _context.Categories.FirstOrDefault(x => x.Name == name);

            var newImage = new Image();
            {
                newImage.Uploaded = DateTime.UtcNow;
                newImage.Url = filePath;
            }

            _context.Images.Add(newImage);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}