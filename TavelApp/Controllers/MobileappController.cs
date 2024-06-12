using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TavelApp.Controllers
{
    public class MobileappController : Controller
    {
        // GET: Mobileapp.cs
        public ActionResult Index()
        {
            return View();
        }

        // GET: Mobileapp.cs/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Mobileapp.cs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mobileapp.cs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Mobileapp.cs/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Mobileapp.cs/Edit/5
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

        // GET: Mobileapp.cs/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Mobileapp.cs/Delete/5
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