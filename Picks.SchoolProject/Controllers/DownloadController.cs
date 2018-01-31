﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.OData.Atom;
using Picks.SchoolProject.Data;
using Picks.SchoolProject.Models;

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
 
            var allImages = _context.Images.ToList();

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");

            return View(allImages);
        }

        public IActionResult DownloadImage (string url)
        {
            var filepath = @"~/images/" + url;

            return File(filepath, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(filepath));
        }

        public IActionResult Filter(int id)
        {
            if (id == 0)
            {
                ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
                var allImages = _context.Images.ToList();
                return View("Download", allImages);
            }

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");

            var images = _context.Images.Where(x => x.CategoryId == id).ToList();

            return View("Download", images);
        }
    }
}