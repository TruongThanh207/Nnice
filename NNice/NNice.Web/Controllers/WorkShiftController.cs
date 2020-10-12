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
    public class WorkShiftController : Controller
    {
        private Connector _connector;

        public WorkShiftController()
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
        public async Task<IActionResult> Create()
        {
            var response = await _connector.GetAsync<EmployeeViewModel>(Routes.EmployeeUrl);

            if (response != null && response.data != null)
                ViewBag.AllEmployees = response.data.ToList();
            else
                ViewBag.AllEmployees = new List<int>();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(WorkShiftDetailModel model)
        {

            if (ModelState.IsValid)
            {
                var tModel = new WorkShiftModel();
                tModel.ShiftNumber = model.ShiftNumber;
                tModel.WorkDate = model.WorkDate;
                tModel.Employees = model.Employees.Select(x => new EmployeeViewModel { ID = x }).ToList();
                await _connector.CreateAsync(tModel, Routes.WorkShiftUrl);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var response = await _connector.GetAsync<WorkShiftModel>(Routes.WorkShiftUrl + $"/{id}");
            var emResponse = await _connector.GetAsync<EmployeeViewModel>(Routes.EmployeeUrl);
            if (emResponse != null && emResponse.data != null)
                ViewBag.AllEmployees = emResponse.data.ToList();
            else
                ViewBag.AllEmployees = new List<int>();
            var wsModel = new WorkShiftDetailModel();
            var ws = response.data.FirstOrDefault();
            wsModel.Employees = ws.Employees.Select(x => x.ID).ToList();
            wsModel.ID = ws.ID;
            wsModel.ShiftNumber = ws.ShiftNumber;
            wsModel.WorkDate = ws.WorkDate;
            return View(wsModel);
        }


        [HttpPost]
        public async Task<IActionResult> Update(WorkShiftDetailModel model)
        {
            if (ModelState.IsValid)
            {
                var tModel = new WorkShiftModel();
                tModel.ShiftNumber = model.ShiftNumber;
                tModel.WorkDate = model.WorkDate;
                tModel.Employees = model.Employees.Select(x => new EmployeeViewModel { ID = x }).ToList();
                var response = await _connector.UpdateAsync(tModel, Routes.WorkShiftUrl + $"/{model.ID}");
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _connector.DeleteAsync(Routes.WorkShiftUrl + $"/{id}");
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Index(string from)
        {
            var model = new WorkShiftViewModel();
            if (from != null && DateTime.TryParse(from, out DateTime fromDate))
            {
                var days = GetNext7Dates(fromDate);
                model.dates = days;
            }
            else
            {
                model.dates = GetDates();
            }
            var response = await _connector.GetAsync<WorkShiftModel>(Routes.WorkShiftUrl);
            if (response != null && response.data != null)
                model.workShifts = response.data.ToList();
            return View(model);
        }

        private List<DateTime> GetDates(int offset = 0)
        {
            DateTime today = DateTime.Today + TimeSpan.FromDays(offset * 7);
            int currentDayOfWeek = (int)today.DayOfWeek;
            DateTime sunday = today.AddDays(-currentDayOfWeek);
            DateTime monday = sunday.AddDays(1);
            if (currentDayOfWeek == 0)
            {
                monday = monday.AddDays(-7);
            }
            return Enumerable.Range(0, 7).Select(days => monday.AddDays(days)).ToList();
        }

        private List<DateTime> GetNext7Dates(DateTime start)
        {
            List<DateTime> dates = new List<DateTime>();
            for (int i = 0; i < 7; i++)
            {
                dates.Add(start.AddDays(i));
            }
            return dates;
        }
    }
}