using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NNice.Web.Helpers;
using NNice.Web.Models;

namespace NNice.Web.Controllers
{
    public class EmployeeController : Controller
    {
        //private readonly List<EmployeeViewModel> employees;
        private Connector _connector;

        public EmployeeController()
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
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _connector.CreateAsync(model, Routes.EmployeeUrl);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var response = await _connector.GetAsync<EmployeeViewModel>(Routes.EmployeeUrl + $"/{id}");
            return View(response.data.FirstOrDefault());
        }

        [HttpPost]
        public async Task<IActionResult> Update(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await _connector.UpdateAsync(model, Routes.EmployeeUrl + $"/{model.ID}");
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _connector.DeleteAsync(Routes.EmployeeUrl + $"/{id}");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var response = await _connector.GetAsync<EmployeeViewModel>(Routes.EmployeeUrl);
            return View(response.data);
        }
    }
}