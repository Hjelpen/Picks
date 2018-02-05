using System;
using System.Collections.Generic;
using System.IO;
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
        public IActionResult DownloadImage(string url)
        {
            var filepath = @"~/images/" + url;

            return File(filepath, System.Net.Mime.MediaTypeNames.Application.Octet, Path.GetFileName(filepath));
        }

        public IActionResult Basket()
        {

            var sessionImage = HttpContext.Session.Get<List<string>>("var");

            return View(sessionImage);
        }

        public IActionResult RemoveFromBasket(string image)
        {

            var sessionImage = HttpContext.Session.Get<List<string>>("var");

            List<string> myList = new List<string>(sessionImage);

            myList.Remove(image);

            HttpContext.Session.Set("var", myList);

            return RedirectToAction("Basket", "Basket");
        }
    }
}   