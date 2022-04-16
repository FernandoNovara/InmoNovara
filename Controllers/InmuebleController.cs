using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InmoNovara.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inmonovara.controllers
{
    public class InmuebleController : Controller
    {

        RepositorioInmueble repositorio;
        RepositorioPropietario repositorioPropietario;

        public InmuebleController()
        {
            repositorio = new RepositorioInmueble();
            repositorioPropietario = new RepositorioPropietario();
        }


        // GET: Inmueble
        public ActionResult Index()
        {
            var lista = repositorio.ObtenerInmueble();
            return View(lista);
        }

        // GET: Inmueble/Details/5
        public ActionResult Detalles(int id)
        {
            var lista = repositorio.ObtenerPorId(id);
            return View(lista);
        }

        // GET: Inmueble/Create
        public ActionResult Create()
        {
            ViewBag.propietarios = repositorioPropietario.ObtenerPropietario();
            return View();
        }

        // POST: Inmueble/Create
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
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Inmueble/Edit/5
        public ActionResult Editar(int id)
        {
            ViewBag.propietarios =  repositorioPropietario.ObtenerPropietario();
            var lista = repositorio.ObtenerPorId(id);
            return View(lista);
        }

        // POST: Inmueble/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, IFormCollection collection)
        {
            Inmueble I;
            try
            {
                I = repositorio.ObtenerPorId(id);
                I.Tipo = collection["Tipo"];
                I.Ambiente = collection["Ambiente"];
                I.Tamaño = collection["Tamaño"];
                repositorio.Editar(I);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Inmueble/Delete/5
        public ActionResult Eliminar(int id)
        {            
            var lista = repositorio.ObtenerPorId(id);
            ViewBag.propietarios =  repositorioPropietario.ObtenerPorId(lista.IdPropietario);
            return View(lista);
        }

        // POST: Inmueble/Delete/5
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