using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Self.GroupInsurance.Web.Models;
using Microsoft.Extensions.Options;

namespace Self.GroupInsurance.Web.Controllers
{
    public class EmployeeController : Controller
    {
        string uri = "http://localhost:55105/api/Employee/";

        //private readonly IOptions<AppConfig> config;

        public EmployeeController(IOptions<AppConfig> config)
        {
            //this.config = config;
            this.uri = config.Value.URI;
        }

        // GET: Employee
        public ActionResult Index()
        {
            List<Employee> employees = new List<Employee>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                var responseTask = client.GetAsync("GetEmployee");
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var emps = result.Content.ReadAsStringAsync();
                    employees = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Employee>>(emps.Result);
                }
            }
            return View("Employees",employees);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View("AddEmployee");
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                using (var client = new HttpClient())
                {
                    Employee emp = new Employee() { ID = collection["ID"], FirstName = collection["FirstName"],
                        LastName = collection["LastName"], Age = Convert.ToInt16(collection["Age"]) };
                    client.BaseAddress = new Uri(uri);
                    var responseTask = client.PostAsJsonAsync<Employee>("AddEmployee", emp);
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Employee>();
                        var employeeID = readTask.Result;
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employee/Edit/5
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

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
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
    }
}