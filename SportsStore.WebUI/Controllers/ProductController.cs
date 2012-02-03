using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        public int PageSize = 2;
        private IProductRepository repository;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        //
        // GET: /Product/

        public ViewResult List(string category, int page = 1)
        {
            ProductListViewModel productListViewModel = new ProductListViewModel()
            {
                Products = repository.Products
                    .Where(e => category == null || e.CategoryName == category)
                    .OrderByDescending(e => e.ProductID)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),

                PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Where(p => category == null || p.CategoryName == category).Count()
                },

                CurrentCategory = category
            };

            return View(productListViewModel);
        }

    }
}
