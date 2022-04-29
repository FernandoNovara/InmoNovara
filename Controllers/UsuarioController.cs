using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using InmoNovara.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace InmoNovara.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IWebHostEnvironment environment;
        private readonly IConfiguration configuration;
        public RepositorioUsuario repositorio;

        // GET: Usuario

        public UsuarioController(IConfiguration configuration,IWebHostEnvironment environment)
        {
            this.environment = environment;
            this.configuration = configuration;
            repositorio = new RepositorioUsuario();
        }

        [Authorize]
        public ActionResult Index()
        {
            if (User.IsInRole("Administrador"))
            {
                var lista = repositorio.ObtenerUsuario();
                return View(lista);
            }
            else
            {
                return RedirectToAction("Index","Propietario");
            }

        }

        // GET: Usuario/Detalles/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Detalles(int id)
        {
            var lista = repositorio.ObtenerPorId(id);
            return View(lista);
        }

        // GET: Usuario/Create
        [Authorize(Policy = "Administrador")]
        public ActionResult Create()
        {
            ViewBag.Roles = Usuario.ObtenerRoles();
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administrador")]
        public ActionResult Create(Usuario u)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: u.Clave,
                        salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 1000,
                        numBytesRequested: 256 / 8));
                u.Clave = hashed;
                repositorio.Alta(u);
                if(u.AvatarFile != null && u.IdUsuario > 0)
                {
                    string wwwPath = environment.WebRootPath;
                    string path = Path.Combine(wwwPath,"Upload");
                    if(!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    string fileName = "avatar" + u.IdUsuario + Path.GetExtension(u.AvatarFile.FileName);
                    string pathCompleto = Path.Combine(path,fileName);
                    u.Avatar = Path.Combine("/Upload",fileName);
                    using(FileStream stream = new FileStream(pathCompleto,FileMode.Create))
                    {
                         u.AvatarFile.CopyTo(stream);
                    }
                    repositorio.Editar(u);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Editar/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Editar(int id)
        {
            var lista = repositorio.ObtenerPorId(id);
            ViewBag.Roles = Usuario.ObtenerRoles();
            return View(lista);
        }

        // POST: Usuario/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Editar(int id, IFormCollection collection)
        {
            Usuario u;
            try
            {
                u = repositorio.ObtenerPorId(id);
                u.Nombre = collection["Nombre"];
                u.Apellido = collection["Apellido"];
                u.Email = collection["Email"];
                if(!collection["Clave"].Equals(u.Clave))
                {
                    if(!String.IsNullOrEmpty(collection["Clave"]))
                    {
                        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: collection["Clave"],
                        salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 1000,
                        numBytesRequested: 256 / 8));
                        
                        u.Clave = hashed;
                    }
                }
                if(!collection["Avatar"].Equals(u.Avatar))
                {
                    if(!String.IsNullOrEmpty(collection["Avatar"]))
                    {
                        string wwwPath = environment.WebRootPath;
                        string path = Path.Combine(wwwPath,"Upload");
                        if(!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        string fileName = "avatar" + u.IdUsuario + Path.GetExtension(u.AvatarFile.FileName);
                        string pathCompleto = Path.Combine(path,fileName);
                        u.Avatar = Path.Combine("/Upload",fileName);
                        using(FileStream stream = new FileStream(pathCompleto,FileMode.Create))
                        {
                            u.AvatarFile.CopyTo(stream);
                        }
                    }
                }
                u.Rol = Int32.Parse(collection["Rol"]);
                repositorio.Editar(u);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Eliminar/5
        [Authorize(Policy = "Administrador")]
        public ActionResult Eliminar(int id)
        {
            var lista = repositorio.ObtenerPorId(id);
            return View(lista);
        }

        // POST: Usuario/Eliminar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Administrador")]
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

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            TempData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginView login)
        {
            try
            {
                var returnUrl = String.IsNullOrEmpty(TempData["returnUrl"] as string)? "/Propietario" : TempData["returnUrl"].ToString();                
                if(ModelState.IsValid)
                {
                    string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                        password: login.Clave,
                        salt:System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
                        prf: KeyDerivationPrf.HMACSHA1,
                        iterationCount: 1000,
                        numBytesRequested: 256 / 8));
                    
                    var e = repositorio.ObtenerPorEmail(login.Usuario);
                    if (e == null || e.Clave != hashed)
                    {
                        ModelState.AddModelError("", "El email o la clave no son correctos");
                        TempData["returnUrl"] = returnUrl;
                        return View();
                    }

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, e.Email),
                        new Claim("FullName", e.Nombre + " " + e.Apellido),
                        new Claim(ClaimTypes.Role, e.RolNombre),
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));
                    TempData.Remove("returnUrl");
                    return Redirect(returnUrl);
                }
                TempData["returnUrl"] = returnUrl;
                return View();
            }
            catch(Exception ex)
            {   
                return View(ex);
            }
        }

        [Route("salir", Name = "logout")]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}