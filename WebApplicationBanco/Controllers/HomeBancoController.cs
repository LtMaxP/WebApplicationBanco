using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationBanco.Models;

namespace WebApplicationBanco.Controllers
{
    public class HomeBancoController : Controller
    {
        // GET: HomeBancoController1
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index(Tarjetum tar)
        {
            Tarjetum t = tar;
            return View();
        }

        // GET: HomeBancoController1/Details/5
        public ActionResult Details(int id)
        {
            //using(var cont = new Context)
            return View();
        }

        // GET: HomeBancoController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeBancoController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeBancoController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeBancoController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


        // GET: HomeBancoController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeBancoController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
