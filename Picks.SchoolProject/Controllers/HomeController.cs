using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Picks.SchoolProject.Data;
using Picks.SchoolProject.Models;
using Picks.SchoolProject.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Picks.SchoolProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IDistributedCache _cache;

        public HomeController(ApplicationDbContext context, IHostingEnvironment environment, IDistributedCache cache)
        {
            _context = context;
            _hostingEnvironment = environment;
            _cache = cache;
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
            var fileUrl = guid + file.FileName;
            var filePath = Path.Combine(uploads, fileUrl);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var category = _context.Categories.FirstOrDefault(x => x.Name == name);

            var newImage = new Image();
            {
                newImage.Uploaded = DateTime.UtcNow;
                newImage.Url = fileUrl;
                newImage.CategoryId = category.Id;
                newImage.Name = file.FileName;
            }

            var value = _cache.GetValue<List<string>>("key");

            List<string> newList = new List<string>();
            newList.AddRange(value);
            newList.Add(newImage.Url);
            _cache.SetValue("key", newList);

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