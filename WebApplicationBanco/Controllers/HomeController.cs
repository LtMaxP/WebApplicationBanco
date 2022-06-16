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
            Tarjetum tarjeta = new Tarjetum();
            using (var context = new TestBancoContext())
            {
                foreach (var b in context.Tarjeta.ToList())
                {
                    if (b.NumeroTarjeta.Equals(tarj.NumeroTarjeta))
                    {
                        if (!b.Bloqueo)
                        {
                            returnable = true;
                            tarjeta.IdTarjeta = b.IdTarjeta;
                            tarjeta.NumeroTarjeta = b.NumeroTarjeta;
                        }
                        break;
                    }
                }
            }
            return returnable ? View("_BancView", tarjeta) : BadRequest(ModelState);
        }

        [HttpPost]
        public ActionResult LogTarjetaPIN(Tarjetum tarj)
        {
            bool returnable = false;
            using (var context = new TestBancoContext())
            {
                var b = context.Tarjeta.FirstOrDefault<Tarjetum>(o => o.IdTarjeta == tarj.IdTarjeta);
                if (b != null)
                {
                    if (!b.Bloqueo)
                    {
                        if (b.Pin.Equals(tarj.Pin))
                        {
                            returnable = true;
                            b.IntentosFallidos = 0;
                        }
                        else if (b.IntentosFallidos <= 3)
                        {
                            b.IntentosFallidos++;
                        }
                        else
                        {
                            b.Bloqueo = true;
                        }
                        context.SaveChanges();
                    }
                }
            }

            return returnable ? View("_Operacion", tarj) : BadRequest(ModelState); ;
        }


        //[HttpPost]
        //[Attribute(Name = "action", Argument = "dep")]
        public ActionResult Deposito(Tarjetum mm, decimal monto)
        {
            using (var context = new TestBancoContext())
            {
                var b = context.Cuenta.FirstOrDefault(o => o.IdTarjeta == mm.IdTarjeta);
                if (b != null)
                {
                    b.Monto = b.Monto + monto;
                    context.SaveChanges();
                }
            }
            return View();
        }

        //[HttpPost]
        //[MultipleButton(Name = "action", Argument = "ext")]
        public ActionResult Extraccion(Tarjetum mm, decimal monto)
        {
            using (var context = new TestBancoContext())
            {
                var b = context.Cuenta.FirstOrDefault(o => o.IdTarjeta == mm.IdTarjeta);
                if (b != null)
                {
                    b.Monto = b.Monto - monto;
                    context.SaveChanges();
                }
            }
            return View();
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
