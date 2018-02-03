using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Picks.SchoolProject.Utility;

namespace Picks.SchoolProject.Controllers
{
    public class BasketController : Controller
    {
        private readonly IDistributedCache _cache;
        const string SessionKeyName = "_Name";

        public BasketController(IDistributedCache cache)
        {
            _cache = cache;
        }

        public IActionResult Basket()
        {
            var sessionImage = HttpContext.Session.GetString("file");

            ViewBag.Images = sessionImage;

            return View();
        }
    }
}