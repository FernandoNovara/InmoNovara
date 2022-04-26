using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InmoNovara.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InmoNovara.Controllers
{
    public class PagoController : Controller
    {
        RepositorioPago repositorio;
        RepositorioContrato repositorioContrato;
        public PagoController()
        {
            repositorio = new RepositorioPago();
            repositorioContrato = new RepositorioContrato();
        }

        // GET: Pago
        public ActionResult Index()
        {
            var lista = repositorio.ObtenerPago();
            return View(lista);
        }

        // GET: Pago/Details/5
        public ActionResult Detalles(int id)
        {
            var lista = repositorio.ObtenerPorId(id);
            return View(lista);
        }

        // GET: Pago/Create
        public ActionResult Create()
        {
            ViewBag.contrato = repositorioContrato.ObtenerContrato();
            return View();
        }

        // POST: Pago/Create
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
        public ActionResult Editar(int id)
        {
            ViewBag.contrato = repositorioContrato.ObtenerContrato();
            var lista = repositorio.ObtenerPorId(id);
            return View(lista);
        }

        // POST: Pago/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, IFormCollection collection)
        {
            Pago p;
            try
            {
                p = repositorio.ObtenerPorId(id);
                p.IdContrato = Int32.Parse(collection["IdContrato"]);
                p.FechaPago = DateTime.Parse(collection["FechaPago"]);
                p.Importe = Double.Parse(collection["Importe"]);
                repositorio.Editar(p);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Pago/Delete/5
        public ActionResult Eliminar(int id)
        {
            ViewBag.pago =  repositorio.ObtenerPago();
            var lista = repositorio.ObtenerPorId(id);
            return View(lista);
        }

        // POST: Pago/Delete/5
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
    }
}