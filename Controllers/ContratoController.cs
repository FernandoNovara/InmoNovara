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
    public class ContratoController : Controller
    {
        RepositorioContrato repositorioContrato;
        RepositorioPago repositorioPago;
        RepositorioInmueble repositorioInmueble;
        RepositorioInquilino repositorioInquilino;
        RepositorioGarante repositorioGarante;

        // GET: Contrato

        public ContratoController()
        {
            repositorioContrato = new RepositorioContrato();
            repositorioInmueble = new RepositorioInmueble();
            repositorioInquilino = new RepositorioInquilino();
            repositorioGarante = new RepositorioGarante();
            repositorioPago = new RepositorioPago();
        }

        [Authorize]
        public ActionResult Index()
        {
            var lista = repositorioContrato.ObtenerContrato();
            ViewBag.Inmueble = repositorioInmueble.ObtenerInmueble();
            ViewBag.Inquilino = repositorioInquilino.ObtenerInquilino();
            ViewBag.Garante = repositorioGarante.ObtenerGarante();
            return View(lista);
        }

        // GET: Contrato/Details/5
        [Authorize]
        public ActionResult Detalles(int id)
        {
            var lista = repositorioContrato.ObtenerPorId(id);
            ViewBag.Inmueble = repositorioInmueble.ObtenerPorId(lista.IdInmueble);
            ViewBag.Inquilino = repositorioInquilino.ObtenerPorId(lista.IdInquilino);
            ViewBag.Garante = repositorioGarante.ObtenerPorId(lista.IdGarante);
            return View(lista);
        }

        // GET: Contrato/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.inmueble = repositorioInmueble.ObtenerInmueble();
            ViewBag.inquilino = repositorioInquilino.ObtenerInquilino();
            ViewBag.garante = repositorioGarante.ObtenerGarante();
            return View();
        }

        // POST: Contrato/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contrato c )
        {
            try
            {                
                int res = repositorioContrato.Alta(c);
                if( res > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Contrato/Edit/5
        [Authorize]
        public ActionResult Editar(int id)
        {
            var lista = repositorioContrato.ObtenerPorId(id);
            ViewBag.inmueble = repositorioInmueble.ObtenerInmueble();
            ViewBag.inquilino = repositorioInquilino.ObtenerInquilino();
            ViewBag.garante = repositorioGarante.ObtenerGarante();
            return View(lista);
        }

        // POST: Contrato/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, Contrato collection)
        {
            Contrato c;
            try
            {
                c = repositorioContrato.ObtenerPorId(id);
                c.IdInmueble = collection.IdInmueble;
                c.IdInquilino = collection.IdInquilino;
                c.IdGarante = collection.IdGarante;
                c.FechaInicio = collection.FechaInicio;
                c.FechaFinal = collection.FechaFinal;
                repositorioContrato.Editar(c);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Contrato/Delete/5

        [Authorize(Policy = "Administrador")]
        public ActionResult Eliminar(int id)
        {
            var lista = repositorioContrato.ObtenerPorId(id);
            ViewBag.Inmuebles = repositorioInmueble.ObtenerPorId(lista.IdInmueble);
            ViewBag.Inquilinos = repositorioInquilino.ObtenerPorId(lista.IdInquilino);
            ViewBag.Garantes = repositorioGarante.ObtenerPorId(lista.IdGarante);
            return View(lista);
        }

        // POST: Contrato/Delete/5
        [Authorize(Policy = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int id, IFormCollection collection)
        {
            try
            {
                repositorioContrato.Baja(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


    }
}