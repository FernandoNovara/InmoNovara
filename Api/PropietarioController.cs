using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InmoNovara.Models;


namespace Inmobiliaria_.Net_Core.Api
{
	[Route("api/[controller]")]
	[ApiController]
	
    public class PropietarioController : ControllerBase
	{
        private readonly DataContext Contexto;

        public PropietarioController(DataContext dataContext)
		{
			this.Contexto = dataContext;
		}

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
		{
			try
			{
				//var usuario = User.Identity.Name;
				//return await Contexto.Propietarios.SingleOrDefaultAsync(x => x.Email == usuario);	
				return Ok(await Contexto.Propietarios.ToListAsync<Propietario>());
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			try
			{
				var entidad = await Contexto.Propietarios.SingleOrDefaultAsync(x => x.Id == id);
				return entidad != null ? Ok(entidad) : NotFound();
			}
			catch(Exception ex)
			{
				return BadRequest(ex);
			}
		}


		
    }
}