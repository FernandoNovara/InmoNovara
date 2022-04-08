using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InmoNovara.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InmoNovara.Controllers
{
    public class PropietarioController : Controller
    {
        RepositorioPropietario repositorio;

        public PropietarioController()
        {
            repositorio = new RepositorioPropietario(); 
        }

        // GET: Propietario
        public ActionResult Index()
        {
             var lista = repositorio.ObtenerPropietario();
            return View(lista);
        }

        // GET: Propietario/Details/5
        public ActionResult Detalles(int id)
        {
            var lista = repositorio.ObtenerPorId(id);
            return View(lista);
        }

        // GET: Propietario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Propietario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public ActionResult Editar(int id)
        {
            var lista = repositorio.ObtenerPorId(id);
            return View(lista);
        }

        // POST: Propietario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        public ActionResult Eliminar(int id)
        {
            var lista = repositorio.ObtenerPorId(id);
            return View(lista);
        }

        // POST: Propietario/Delete/5
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
    }
}