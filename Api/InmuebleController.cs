using System;
using System.Threading.Tasks;
using InmoNovara.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Inmobiliaria_.Net_Core.Api
{
	[Route("api/[controller]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class InmuebleController : ControllerBase
	{
        private readonly DataContext Contexto;

        public InmuebleController(DataContext contexto)
        {
            this.Contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var usuario = User.Identity.Name;
                return Ok(Contexto.Inmueble.Include(e => e.Propietario).Where(e => e.Propietario.Email == usuario));
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}