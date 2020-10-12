using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NNice.Web.Helpers;
using NNice.Web.Helpers.Responses;
using NNice.Web.Models;

namespace NNice.Web.Controllers
{
    public class BookingServiceController : Controller
    {
        private Connector _connector;

        public BookingServiceController()
        {
            var client = new HttpClient();
            // Update port # in the following line.
            client.BaseAddress = new Uri(Routes.BaseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            _connector = new Connector(client);
        }
        // GET: BookingService
        public async Task<ActionResult> Index()
        {
            var response = await _connector.GetAsync<OrderResponse>(Routes.BookingServiceUrl);
            var invoices = response.data;

            if (invoices == null)
            {
                return View();
            }

            var models = invoices.Select(x => new BookingServiceViewModel()
            {
                CreatedBy = x.CreatedBy,
                RoomName = x.RoomName,
                ID = x.ID,
                BookingParty = x.BookingParty,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                PartyName = x.PartyName,
                TotalAmount = x.TotalAmount
            });

            return View(models);
        }

        // GET: BookingService/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var response = await _connector.GetAsync<BookingServiceDetailViewModel>(Routes.BookingServiceUrl + $"/{id}");
                var model = response.data;
                return View(model.FirstOrDefault());
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: BookingService/Create
        public async Task<ActionResult> Create()
        {
            var emptyToCart = await _connector.GetAsync<CartViewModel>(Routes.EmptyToCart);
            //get all room
            var rooms = await _connector.GetAsync<RoomViewModel>(Routes.RoomUrl);
            var roomDataSource = new List<SelectBox>();

            if (rooms.data != null && rooms.data.Count() != 0)
            {
                roomDataSource = rooms.data.Where(w => w.IsAvailable == true).Select(x => new SelectBox()
                {
                    Id = x.ID.Value,
                    Name = x.Name
                }).ToList();
            }

            if (roomDataSource.Count() == 0)
            {
                roomDataSource.Add(new SelectBox() { Id = 0, Name = "No Room Chose..." });
            }

            var products = await _connector.GetAsync<ProductViewModel>(Routes.ProductUrl);
            var carts = await _connector.GetAsync<CartViewModel>(Routes.ShoppingCart);
            var shoppingCarts = new List<ShoppingCartViewModel>();

            if (products.data != null && products.data.Count() != 0)
            {
                shoppingCarts = products.data.Join(
                   carts.data,
                   iner => iner.ID,
                   outer => outer.ProductID,
                   (iner, outer) => new ShoppingCartViewModel()
                   {
                       CartId = outer.CartId,
                       Count = outer.Count,
                       Name = iner.Name,
                       ProductID = iner.ID.Value,
                       UnitPrice = iner.UnitPrice,
                   }).ToList();
            }

            var bookingService = new BookingServiceViewModel()
            {
                RoomItems = new SelectList(roomDataSource, "Id", "Name"),
                ShoppingCarts = shoppingCarts
            };
            return View(bookingService);
        }

        // POST: BookingService/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BookingServiceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            try
            {
                // TODO: Add insert logic here
                model.UserID = int.Parse(User.Identity.Name);
                await _connector.CreateAsync<BookingServiceViewModel>(model, Routes.BookingServiceUrl);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookingService/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookingService/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookingService/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookingService/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        public async Task<ActionResult> AddProductPartial(CartViewModel model)
        {
            var response = await _connector.CreateAsync<CartViewModel>(model, Routes.AddToCart);

            return Json("successfully");
        }
    }
}