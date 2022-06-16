using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationBanco.Models;

namespace WebApplicationBanco.Controllers
{
    public class HomeController : Controller
    {
        //private readonly Tarjetum _tar;
       
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Tarjetum tarj)
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogTarjetaNumero(Tarjetum tarj)
        {
            bool returnable = false;
            using (var context = new TestBancoContext())
            {
                foreach (var b in context.Tarjeta.ToList())
                {
                    if (b.NumeroTarjeta.Equals(tarj.NumeroTarjeta))
                    {
                        if (!b.Bloqueo) { returnable = true; }
                        break;
                    }
                }
            }
            return returnable ? View("_BancView", tarj) : BadRequest(ModelState);
        }
        //[HttpPost]
        //public bool LogTarjetaNumero(long tarj)
        //{
        //    bool returnable = false;
        //    using (var context = new TestBancoContext())
        //    {
        //        foreach (var b in context.Tarjeta.ToList())
        //        {
        //            if (b.NumeroTarjeta.Equals(tarj))
        //            {
        //                returnable = true;
        //                break;
        //            }
        //        }
        //    }
        //    //    long val = (long)tarj.NumeroTarjeta;
        //    //return _tar.NumeroTarjeta.HasValue.Equals(val) ? true : false;
        //    return returnable;
        //}
        public bool LogTarjetaPIN(Tarjetum tarj)
        {
            bool returnable = false;
            using (var context = new TestBancoContext())
            {
                foreach (var b in context.Tarjeta.ToList())
                {
                    if (b.NumeroTarjeta.Equals(tarj.NumeroTarjeta))
                    {
                        returnable = b.Pin.Equals(tarj.Pin) ? true : false;
                        break;
                    }
                }
            }
            //    long val = (long)tarj.NumeroTarjeta;
            //return _tar.NumeroTarjeta.HasValue.Equals(val) ? true : false;
            return returnable;
        }

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
