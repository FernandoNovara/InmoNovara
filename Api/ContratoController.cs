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

    public class ContratoController : ControllerBase
	{
        private readonly DataContext Contexto;

        public ContratoController(DataContext contexto)
        {
            this.Contexto = contexto;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // try
            // {
            //     var entidad = await Contexto.Contrato.FirstOrDefaultAsync(x => x.IdInmueble == id);                
			// 	return entidad != null ? Ok(entidad) : NotFound();
            // }
            // catch(Exception ex)
            // {
            //     return BadRequest(ex);
            // }

            try
            {
                var usuario = User.Identity.Name;
                var contratos=  (Contexto.Contrato.Include(c=> c.inmueble).Include(i=>i.inquilino).Include(g=>g.garante).Where(c => c.inmueble.Propietario.Email == usuario)).ToListAsync();

            
                return contratos != null ? Ok(await contratos) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        

    }
}