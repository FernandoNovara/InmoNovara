using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InmoNovara.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace InmoNovara.Controllers
{
    public class PagoController : Controller
    {
        RepositorioPago repositorio;
        RepositorioContrato repositorioContrato;
        RepositorioInmueble repositorioInmueble;
        public PagoController()
        {
            repositorio = new RepositorioPago();
            repositorioContrato = new RepositorioContrato();
            repositorioInmueble = new RepositorioInmueble();
        }

        // GET: Pago
        [Authorize]
        public ActionResult Index()
        {
            var lista = repositorio.ObtenerPago();
            return View(lista);
        }

        // GET: Pago/Details/5
        [Authorize]
        public ActionResult Detalles(int id)
        {
            var lista = repositorio.ObtenerPorId(id);
            return View(lista);
        }

        // GET: Pago/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.contrato = repositorioContrato.ObtenerContrato();
            ViewBag.Inmueble = repositorioInmueble.ObtenerInmueble();
            return View();
        }

        // POST: Pago/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pago p)
        {
            try
            {
                int res = repositorio.Alta(p);
                if( res > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.propietarios = repositorio.ObtenerPago();
                    return View();
                }
            }
            catch
            {
                ViewBag.propietarios = repositorio.ObtenerPago();
                return View();
            }
        }

        // GET: Pago/Edit/5
        [Authorize]
        public ActionResult Editar(int id)
        {
            ViewBag.contrato = repositorioContrato.ObtenerContrato();
            var lista = repositorio.ObtenerPorId(id);
            return View(lista);
        }

        // POST: Pago/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, Pago collection)
        {
            Pago p;
            try
            {
                p = repositorio.ObtenerPorId(id);
                p.IdContrato = collection.IdContrato;
                p.FechaPago = collection.FechaPago;
                p.Importe = collection.Importe;
                repositorio.Editar(p);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Pago/Delete/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Eliminar(int id)
        {
            ViewBag.pago =  repositorio.ObtenerPago();
            var lista = repositorio.ObtenerPorId(id);
            return View(lista);
        }

        // POST: Pago/Delete/5
        [Authorize(Policy = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int id, IFormCollection collection)
        {
            try
            {
                repositorio.Baja(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Pagos(int id)
        {
            var lista = repositorioContrato.ObtenerPorId(id);
            ViewBag.Pagos = repositorio.ObtenerPorContrato(id);
            return View(lista);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RealizarPago(int id,IFormCollection collection)
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