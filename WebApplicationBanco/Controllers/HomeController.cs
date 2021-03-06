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
        public IActionResult LogTarjetaNumero(long tarje)
        {
            bool returnable = false;
            int id = 0;
            using (var context = new TestBancoContext())
            {
                foreach (var b in context.Tarjeta.ToList())
                {
                    if (b.NumeroTarjeta.Equals(tarje))
                    {
                        if (!b.Bloqueo)
                        {
                            id = b.IdTarjeta;
                            TempData["idT"] = b.IdTarjeta;
                            returnable = true;
                        }
                        break;
                    }
                }
            }

            return returnable ? View("_BancView") : BadRequest(ModelState);
        }


        [HttpPost]
        public ActionResult LogTarjetaPIN(int pin)
        {
            bool returnable = false;
            int idTar = (int)TempData["idT"];
            Cuentum c = new Cuentum();
            using (var context = new TestBancoContext())
            {

                var b = context.Tarjeta.FirstOrDefault(o => o.IdTarjeta == idTar);

                if (b.Pin.Equals(pin))
                {

                    if (b != null)
                    {
                        if (!b.Bloqueo)
                        {
                            if (b.Pin.Equals(pin))
                            {
                                returnable = true;
                                b.IntentosFallidos = 0;
                                c = context.Cuenta.FirstOrDefault(x => x.IdTarjeta == idTar);
                                TempData["user"] = c;
                                TempData["id"] = c.IdCuenta;
                            }
                            else if (b.IntentosFallidos <= 4)
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
            }

            return returnable ? View("_Operacion", c) : BadRequest(ModelState); ;
        }


        //[HttpPost]
        //[Attribute(Name = "action", Argument = "dep")]
        //[HttpPost]
        //public ActionResult Deposito(int id, decimal monto)
        //{
        //    using (var context = new TestBancoContext())
        //    {
        //        var b = context.Cuenta.FirstOrDefault(o => o.IdTarjeta == id);
        //        if (b != null)
        //        {
        //            b.Monto = b.Monto + monto;
        //            context.SaveChanges();
        //        }
        //    }
        //    return View("_Deposito");
        //}
        //[HttpPost]
        //public ActionResult Deposito(int id)
        //{
        //    using (var context = new TestBancoContext())
        //    {
        //        var b = context.Cuenta.FirstOrDefault(o => o.IdTarjeta == id);
        //    }
        //    return View("_Deposito");
        //}
        [HttpPost]
        public ActionResult Balance()
        {

            int idCuenta = (int)TempData["id"];
            Cuentum b = new Cuentum();
            using (var context = new TestBancoContext())
            {
                b = context.Cuenta.FirstOrDefault(o => o.IdTarjeta == idCuenta);
                TempData["idB"] = b.IdCuenta;
            }

            return View("_Balance", b);
        }

        [HttpPost]
        public ActionResult Extraccion()
        {
            int idCuenta = (int)TempData["id"];
            Cuentum b = new Cuentum();
            using (var context = new TestBancoContext())
            {
                b = context.Cuenta.FirstOrDefault(o => o.IdTarjeta == idCuenta);
                TempData["idE"] = b.IdCuenta;
            }
            return View("_Extraccion", b);
        }


        [HttpPost]
        public ActionResult BalanceBack()
        {
            int idCuenta = (int)TempData["id"];
            Cuentum b = new Cuentum();
            using (var context = new TestBancoContext())
            {
                b = context.Cuenta.FirstOrDefault(o => o.IdTarjeta == idCuenta);
            }
            return View("_Operacion", b);
        }

        
        [HttpPost]
        public ActionResult Extraer(decimal monto)
        {
            int idCuenta = (int)TempData["idE"];
            Cuentum b = new Cuentum();
            using (var context = new TestBancoContext())
            {
                b = context.Cuenta.FirstOrDefault(o => o.IdTarjeta == idCuenta);
                if (b != null)
                {
                    b.Monto = b.Monto - monto;
                    Registro r = new Registro();
                    r.IdUsuario = context.Usuarios.FirstOrDefault(o => o.IdCuenta == idCuenta).IdUsuario;
                    r.Accion = "Extrae " + monto;
                    r.FechaRegistro = DateTime.Now;
                    context.SaveChanges();
                }
            }
            return View();
        }
        ////[HttpPost]
        ////[MultipleButton(Name = "action", Argument = "ext")]
        //[HttpPost]
        public ActionResult Extraccion(int id, decimal monto)
        {
            using (var context = new TestBancoContext())
            {
                var b = context.Cuenta.FirstOrDefault(o => o.IdTarjeta == id);
                if (b != null)
                {
                    b.Monto = b.Monto - monto;
                    context.SaveChanges();
                }
            }
            return View("_Extraccion");
        }
        //[HttpPost]
        //public ActionResult Extraccion(int id)
        //{
        //    using (var context = new TestBancoContext())
        //    {
        //        var b = context.Cuenta.FirstOrDefault(o => o.IdTarjeta == id);
        //    }
        //    return View("_Extraccion");
        //}


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
