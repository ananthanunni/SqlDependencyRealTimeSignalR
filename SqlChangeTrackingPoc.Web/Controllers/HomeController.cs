using SqlChangeTrackingPoc.Models.DevTest;
using SqlChangeTrackingPoc.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SqlChangeTrackingPoc.Web.Controllers
{
    public class HomeController : Controller
    {
        private DevTestService _service = null;
        public HomeController()
        {
            _service = DevTestService.Instance;

            if (!_service.EventsSubscribed)
            {
                _service.OnChanged += (a, b) =>
                {

                };

                _service.OnError += (a, b) =>
                {

                };
            }
        }
        // GET: Home
        public ActionResult Index()
        {
            var items = _service.GetAllDevTests();
            return View(items);
        }

        //// GET: Home/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(DevTestModel model)
        {
            try
            {
                // TODO: Add insert logic here
                _service.Add(model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            var item = _service.GetItem(id);
            return View("Create", item);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, DevTestModel model)
        {
            try
            {
                // TODO: Add update logic here
                _service.Update(id, model);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
