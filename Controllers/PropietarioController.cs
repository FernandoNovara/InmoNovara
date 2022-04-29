using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InmoNovara.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;

namespace InmoNovara.Controllers
{
    public class PropietarioController : Controller
    {
        RepositorioPropietario repositorio;
        RepositorioInmueble repositorioInmueble;

        public PropietarioController()
        {
            repositorio = new RepositorioPropietario(); 
            repositorioInmueble = new RepositorioInmueble();
        }

        // GET: Propietario
        [Authorize]
        public ActionResult Index()
        
        {
             var lista = repositorio.ObtenerPropietario();
            return View(lista);
        }

        // GET: Propietario/Details/5
        [Authorize]
        public ActionResult Detalles(int id)
        {
            var lista = repositorio.ObtenerPorId(id);
            return View(lista);
        }

        // GET: Propietario/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Propietario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(Propietario p)
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
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Propietario/Edit/5
        [Authorize]
        public ActionResult Editar(int id)
        {
            var lista = repositorio.ObtenerPorId(id);
            return View(lista);
        }

        // POST: Propietario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Editar(int id, IFormCollection collection)
        {
            Propietario p;
            try
            {
                p = repositorio.ObtenerPorId(id);
                p.Nombre = collection["Nombre"];
                p.Apellido = collection["Apellido"];
                p.Dni = collection["Dni"];
                p.Direccion = collection["Direccion"];
                p.Telefono = collection["Telefono"];
                repositorio.Editar(p);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Propietario/Delete/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Eliminar(int id)
        {
            var lista = repositorio.ObtenerPorId(id);
            return View(lista);
        }

        // POST: Propietario/Delete/5
        [Authorize(Policy = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int id, Propietario p)
        {
            try
            {
                RepositorioPropietario rp = new RepositorioPropietario();
                rp.Baja(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [Authorize]
        public ActionResult Propiedades(int id)
        {
            var lista = repositorio.ObtenerPorId(id);
            ViewBag.Inmuebles = repositorioInmueble.obtenerPorIdPropietario(id);
            return View(lista);
        }

    }
}