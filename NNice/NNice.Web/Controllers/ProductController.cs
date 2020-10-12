using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NNice.Web.Models;

namespace NNice.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IList<ProductViewModel> p;
        public ProductController()
        {
            p = new List<ProductViewModel>()
            {
                new ProductViewModel {ID = 1,Name="SaiGon",UnitPrice = 20000},
                new ProductViewModel {ID = 2,Name="Tiger",UnitPrice = 30000},
                new ProductViewModel {ID = 3,Name="StrongBow",UnitPrice = 40000}
            };
        }
        [HttpGet]
        public IActionResult Product()
        {
            return View(p.ToList());
        }
        [HttpPost]
        public IActionResult Product(string Search)
        {
            var room = new List<ProductViewModel>();

            if (!String.IsNullOrEmpty(Search))
            {
                room = p.Select(m => m).Where(n => n.Name.Contains(Search)).ToList();
                return View(room);

            }
            else
            {
                return View(p.ToList());
            }
        }
        [HttpGet]
        public IActionResult ProductDetail(int? id)
        {
            if (id.HasValue)
            {
                var model = p.First(m => m.ID == id);
                return View("ProductDetail", model);
            }
            return View();
        }
        [HttpPost]
        public IActionResult ProductDetail(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (model.ID.HasValue)
            {
                ProductViewModel sanpham = p.SingleOrDefault(m => m.ID.Equals(model.ID));
                sanpham.ID = model.ID;
                sanpham.Name = model.Name;
                sanpham.UnitPrice = model.UnitPrice;
                ViewBag.message = 2;
                return View();
            }
            else
            {
                int MaSP_last = p.LastOrDefault().ID.Value;
                ProductViewModel productView = new ProductViewModel()
                {
                    ID = MaSP_last + 1,
                    Name = model.Name,
                    UnitPrice = model.UnitPrice,
                };
                p.Add(productView);
                ViewBag.message = 1;
                return View("Product", p);
            }
        }
        public IActionResult DeleteProduct(int id)
        {
            ProductViewModel product = p.FirstOrDefault(m => m.ID.Equals(id));
            p.Remove(product);
            ViewBag.message = 3;
            return View("Product", p);
        }
    }
}