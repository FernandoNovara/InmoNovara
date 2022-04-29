using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InmoNovara.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Inmonovara.controllers
{
    public class InmuebleController : Controller
    {

        RepositorioInmueble repositorio;
        RepositorioPropietario repositorioPropietario;

        RepositorioContrato repositorioContrato;

        public InmuebleController()
        {
            repositorioContrato = new RepositorioContrato();
            repositorio = new RepositorioInmueble();
            repositorioPropietario = new RepositorioPropietario();
        }


        // GET: Inmueble
        [Authorize]
        public ActionResult Index()
        {
            var lista = repositorio.ObtenerInmueble();
            return View(lista);
        }

        // GET: Inmueble/Details/5
        [Authorize]
        public ActionResult Detalles(int id)
        {
            var lista = repositorio.ObtenerPorId(id);
            ViewBag.Propietario = repositorioPropietario.ObtenerPorId(lista.IdPropietario);
            return View(lista);
        }

        // GET: Inmueble/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.propietarios = repositorioPropietario.ObtenerPropietario();
            return View();
        }

        // POST: Inmueble/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inmueble i)
        {
            try
            {
                int res = repositorio.Alta(i);
                if( res > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.propietarios = repositorioPropietario.ObtenerPropietario();
                    return View();
                }
            }
            catch
            {
                ViewBag.propietarios = repositorioPropietario.ObtenerPropietario();
                return View();
            }
        }

        // GET: Inmueble/Edit/5
        [Authorize]
        public ActionResult Editar(int id)
        {
            ViewBag.propietarios =  repositorioPropietario.ObtenerPropietario();
            var lista = repositorio.ObtenerPorId(id);
            return View(lista);
        }

        // POST: Inmueble/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, IFormCollection collection)
        {
            Inmueble I;
            try
            {
                I = repositorio.ObtenerPorId(id);
                I.Uso = collection["Uso"];
                I.Tipo = collection["Tipo"];
                I.Ambiente = collection["Ambiente"];
                I.Tamaño = collection["Tamaño"];
                I.Precio = Double.Parse(collection["Precio"]);
                repositorio.Editar(I);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Inmueble/Delete/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Eliminar(int id)
        {            
            var lista = repositorio.ObtenerPorId(id);
            ViewBag.propietarios =  repositorioPropietario.ObtenerPorId(lista.IdPropietario);
            return View(lista);
        }

        // POST: Inmueble/Delete/5
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

        public ActionResult Contratos(int id)
        {
            var lista = repositorio.ObtenerPorId(id);
            ViewBag.contratos = repositorioContrato.ObtenerPorIdInmueble(id);
            return View(lista);
        }
    }
}