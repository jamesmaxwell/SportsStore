using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Controllers
{
    [Authorize] 
    public class AdminController : Controller
    {
        private IProductRepository productRepo;

        public AdminController(IProductRepository productRepository)
        {
            productRepo = productRepository;
        }

        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View(productRepo.Products);
        }

        //
        // GET: /Admin/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Create

        public ActionResult Create()
        {
            return View("Edit", new Product());
        }

        //
        // POST: /Admin/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Edit/5

        public ViewResult Edit(int productID)
        {
            Product product = productRepo.Products.FirstOrDefault(p => p.ProductID == productID);

            return View(product);
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                productRepo.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved.", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost]
        public ActionResult Delete(int productID)
        {
            Product product = productRepo.Products.FirstOrDefault(p => p.ProductID == productID);
            if (product != null)
            {
                productRepo.DeleteProduct(product);
                TempData["message"] = string.Format("{0} has been deleted.", product.Name);
            }
            return RedirectToAction("Index");
        }
    }
}
