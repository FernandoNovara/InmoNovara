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

    public class InquilinoController : ControllerBase
	{
        private readonly DataContext Contexto;

        public InquilinoController(DataContext contexto)
        {
            this.Contexto = contexto;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {                
				var entidad = await Contexto.Inquilino.FirstOrDefaultAsync(x => x.Id == id);                
				return entidad != null ? Ok(entidad) : NotFound();
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}