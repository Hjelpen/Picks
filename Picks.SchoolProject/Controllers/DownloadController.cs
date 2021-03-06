﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.OData.Atom;
using Microsoft.Extensions.Caching.Distributed;
using Picks.SchoolProject.Data;
using Picks.SchoolProject.Models;
using Picks.SchoolProject.Utility;

namespace Picks.SchoolProject.Controllers
{
    public class DownloadController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IDistributedCache _cache;

        public DownloadController(ApplicationDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public IActionResult Download(int id)
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");

            var value = _cache.GetValue<List<string>>("key");

            List<string> myList = new List<string>();

            if (value == null)
            {
                var imageList = _context.Images.OrderByDescending(x => x.Uploaded).ToList();

                foreach (var item in imageList)
                {
                    myList.Add(item.Url);
                }

                _cache.SetValue("key", myList);
                return View(myList);
            }

            if (id == 0)
            {
                myList = _cache.GetValue<List<string>>("key");
                return View(myList);
            }
            else
            {
                var imagequery = _context.Images.Where(x => x.CategoryId == id).OrderByDescending(x => x.Uploaded).ToList();
                List<string> queryList = new List<string>();
                foreach (var item in imagequery)
                {
                    queryList.Add(item.Url);
                }
                return View(queryList);
            }
        }

        public IActionResult DownloadImage (string url)
        {
            var filepath = @"~/images/" + url;

            return File(filepath, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(filepath));
        }

        public IActionResult Basket(string file)
        {
            var sessionImage = HttpContext.Session.Get<List<string>>("var");

            List<string> myList = new List<string>();

            if (sessionImage == null)
            {             
                myList.Add(file);
                HttpContext.Session.Set("var", myList);
            }
            else
            {
               List<string> myList1 = new List<string>(sessionImage);
               myList1.Add(file);
               HttpContext.Session.Set("var", myList1);
            }
            

            return RedirectToAction("Download", "Download");
        }

    }

}