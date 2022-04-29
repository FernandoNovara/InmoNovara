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
    public class InquilinoController : Controller
    {
        RepositorioInquilino repositorioInquilino;
        public InquilinoController()
        {
            repositorioInquilino = new RepositorioInquilino();
        }
        
        // GET: Inquilino
        [Authorize]
        public ActionResult Index()
        {
            var lista = repositorioInquilino.ObtenerInquilino();
            return View(lista);
        }

        // GET: Inquilino/Details/5
        [Authorize]
        public ActionResult Detalles(int id)
        {
            var lista = repositorioInquilino.ObtenerPorId(id);
            return View(lista);
        }

        // GET: Inquilino/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inquilino/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Inquilino i)
        {
            try
            {
                int res = repositorioInquilino.Alta(i);
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

        // GET: Inquilino/Edit/5
        [Authorize]
        public ActionResult Editar(int id)
        {
            var lista = repositorioInquilino.ObtenerPorId(id);
            return View(lista);
        }

        // POST: Inquilino/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, IFormCollection collection)
        {
            Inquilino I;
            try
            {
                I = repositorioInquilino.ObtenerPorId(id);    
                I.Nombre = collection["Nombre"];
                I.Dni = collection["Dni"];
                I.LugarTrabajo = collection["LugarTrabajo"];
                I.Direccion = collection["Direccion"];
                I.Correo = collection["Correo"];
                I.Telefono = collection["Telefono"];
                repositorioInquilino.Editar(I);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Inquilino/Delete/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Eliminar(int id)
        {
            var lista = repositorioInquilino.ObtenerPorId(id);
            return View(lista);
        }

        // POST: Inquilino/Delete/5
        [Authorize(Policy = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Eliminar(int id, IFormCollection collection)
        {
            try
            {
                
                repositorioInquilino.Baja(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}