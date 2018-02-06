using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Picks.SchoolProject.Utility;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace Picks.SchoolProject.Controllers
{
    public class BasketController : Controller
    {
        private readonly IDistributedCache _cache;
        const string SessionKeyName = "_Name";
        private readonly IHostingEnvironment _hostingEnvironment;

        public BasketController(IDistributedCache cache, IHostingEnvironment environment)
        {
            _cache = cache;
            _hostingEnvironment = environment;
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

        public IActionResult DownloadAsZip()
        {
            var sessionImageList = HttpContext.Session.Get<List<string>>("var");
            List<string> filepaths = new List<string>();

            foreach (var item in sessionImageList)
            {
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "images\\" + item);
                filepaths.Add(uploads);
            }

            return View("Basket");
        }
    }
}
