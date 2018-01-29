using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Picks.SchoolProject.Data;

namespace Picks.SchoolProject.Controllers
{
    public class DownloadController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DownloadController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Download()
        {
            var images = _context.Images.ToList();

            return View(images);
        }
    }
}