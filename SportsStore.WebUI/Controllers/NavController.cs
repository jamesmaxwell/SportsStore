using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Abstract;

namespace SportsStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository repo;

        public NavController(IProductRepository repository)
        {
            repo = repository;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.Category = category;

            IEnumerable<string> categorys = repo.Products
                .Select(e => e.CategoryName)
                .Distinct()
                .OrderBy(x => x);

            return PartialView(categorys);
        }

    }
}
