using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AmazonWebApp.DAO;
using AmazonWebApp.Helpers;
using AmazonWebApp.Models.Home;

namespace AmazonWebApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string q, int lastIndexOnPage = 0, int page = 1)
        {
            var items = new List<Item>();
            FetchHelper.LoadItems(items, q, page, lastIndexOnPage);
            var model = new SearchItemViewModel()
            {
                Items = items.Take(13).ToList()
            };
            return PartialView("_SearchResults", model);
        }

    }
}
