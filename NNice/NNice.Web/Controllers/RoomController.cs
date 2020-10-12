using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators.Internal;
using NNice.DAL;
using NNice.Web.Helpers;
using NNice.Web.Models;


namespace NNice.Web.Controllers
{
    public class RoomController : Controller
    {
        private Connector _connector;
        public RoomController()
        {
            var client = new HttpClient();
            // Update port # in the following line.
            client.BaseAddress = new Uri(Routes.BaseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            _connector = new Connector(client);
        }

        [HttpGet]
        public async Task<IActionResult> RoomManagement()
        {
            var response = await _connector.GetAsync<RoomViewModel>(Routes.RoomUrl);

            return View(response.data);
        }
        //[HttpPost]
        //public IActionResult RoomManager(string Search)
        //{
        //    var room = new List<RoomViewModel>();

        //    if (!String.IsNullOrEmpty(Search))
        //    {
        //        room = us.Select(p => p).Where(m => m.Name.Contains(Search)).ToList();
        //        return View(room);

        //    }
        //    else
        //    {
        //        return View(us.ToList());
        //    }

        //}
        [HttpGet]
        public async Task<IActionResult> RoomDetail(int? id)
        {
            if (id.HasValue)
            {
                var response = await _connector.GetAsync<RoomViewModel>(Routes.RoomUrl + $"/{id}");
                return View(response.data.FirstOrDefault());
            }

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RoomDetail(RoomViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if(model.ID.HasValue)
            {
                var response = await _connector.UpdateAsync<RoomViewModel>(model , Routes.RoomUrl + $"/{model.ID.Value}");
            }
            else
            {
                await _connector.CreateAsync<RoomViewModel>(model, Routes.RoomUrl);
            }

            return RedirectToAction("RoomManagement");
        }

        public async Task<IActionResult> DeleteRoom(int id)
        {
            var response = await _connector.DeleteAsync(Routes.RoomUrl + $"/{id}");
            return RedirectToAction("RoomManagement");
        }

    }


}