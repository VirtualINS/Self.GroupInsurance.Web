using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Self.GroupInsurance.Web.Models;

namespace Self.GroupInsurance.Web.Controllers
{
    public class StateController : Controller
    {
        string uri = "http://localhost:55105/api/";

        public StateController(IOptions<AppConfig> config)
        {
            //this.config = config;
            this.uri = config.Value.URI;
        }

        // GET: State
        public ActionResult Index()
        {
            List<State> states = new List<State>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri+"State/");
                var responseTask = client.GetAsync("GetStates");
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var sts = result.Content.ReadAsStringAsync();
                    states = Newtonsoft.Json.JsonConvert.DeserializeObject<List<State>>(sts.Result);
                }
            }
            return View("States", states);
        }

        // GET: State/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: State/Create
        public ActionResult Create()
        {
            return View("AddState");
        }

        // POST: State/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                using (var client = new HttpClient())
                {
                    State state= new State()
                    {
                        ID = collection["ID"],
                        StateName = collection["StateName"],
                        Country = collection["Country"]
                    };
                    client.BaseAddress = new Uri(uri+"State/");
                    var responseTask = client.PostAsJsonAsync<State>("AddState", state);
                    var result = responseTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<State>();
                        var stateID = readTask.Result;
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: State/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: State/Edit/5
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

        // GET: State/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: State/Delete/5
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