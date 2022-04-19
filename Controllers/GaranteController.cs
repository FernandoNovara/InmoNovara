using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InmoNovara.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InmoNovara.Controllers
{
    public class GaranteController : Controller
    {
        RepositorioGarante repositorioGarante;
        public GaranteController()
        {
            repositorioGarante = new RepositorioGarante();
        }
        
        // GET: Garante
        public ActionResult Index()
        {
            var lista = repositorioGarante.ObtenerGarante();
            return View(lista);
        }

        // GET: Garante/Details/5
        public ActionResult Detalles(int id)
        {
            var lista = repositorioGarante.ObtenerPorId(id);
            return View(lista);
        }

        // GET: Garante/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Garante/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Garante i)
        {
            try
            {
                int res = repositorioGarante.Alta(i);
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

        // GET: Garante/Edit/5
        public ActionResult Editar(int id)
        {
            var lista = repositorioGarante.ObtenerPorId(id);
            return View(lista);
        }

        // POST: Garante/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, IFormCollection collection)
        {
            Garante I;
            try
            {
                I = repositorioGarante.ObtenerPorId(id);    
                I.Nombre = collection["Nombre"];
                I.Dni = collection["Dni"];
                I.Direccion = collection["Direccion"];
                I.Correo = collection["Correo"];
                I.Telefono = collection["Telefono"];
                repositorioGarante.Editar(I);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Garante/Delete/5
        public ActionResult Eliminar(int id)
        {
            var lista = repositorioGarante.ObtenerPorId(id);
            return View(lista);
        }

        // POST: Garante/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int id, IFormCollection collection)
        {
            try
            {
                
                repositorioGarante.Baja(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}