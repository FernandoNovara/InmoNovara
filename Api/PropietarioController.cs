using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using InmoNovara.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Inmobiliaria_.Net_Core.Api
{
	[Route("api/[controller]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ApiController]
	
    public class PropietarioController : ControllerBase
	{

		private readonly IConfiguration config;
        private readonly DataContext Contexto;

        public PropietarioController(DataContext dataContext, IConfiguration config)
		{
			this.Contexto = dataContext;
			this.config = config;
		}

		[HttpGet]
		public async Task<ActionResult<Propietario>> Get()
		{
			try
			{
				var entidad = await Contexto.Propietarios.FirstOrDefaultAsync(x => x.Email == User.Identity.Name); 
				return entidad != null ? Ok(entidad) : NotFound();
			}
			catch(Exception ex)
			{
				return BadRequest(ex);
			}
		}

		// POST api/<controller>/login
		[HttpPost("login")]
		[AllowAnonymous]
		public async Task<IActionResult> Login([FromBody] LoginView loginView)
		{
			try
			{
				string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: loginView.Clave,
				salt: System.Text.Encoding.ASCII.GetBytes(config["Salt"]),
				prf: KeyDerivationPrf.HMACSHA1,
				iterationCount: 1000,
				numBytesRequested: 256 / 8));
				var p = await Contexto.Propietarios.FirstOrDefaultAsync(x => x.Email == loginView.Usuario);
				if (p == null || p.Clave != hashed)
				{
					return BadRequest("Nombre de usuario o clave incorrecta");
				}
				else
				{
					var key = new SymmetricSecurityKey(
						System.Text.Encoding.ASCII.GetBytes(config["TokenAuthentication:SecretKey"]));
					var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
					var claims = new List<Claim>
					{
						new Claim(ClaimTypes.Name, p.Email),
						new Claim("FullName", p.Nombre + " " + p.Apellido),
						new Claim(ClaimTypes.Role, "Propietarios"),
					};

					var token = new JwtSecurityToken(
						issuer: config["TokenAuthentication:Issuer"],
						audience: config["TokenAuthentication:Audience"],
						claims: claims,
						expires: DateTime.Now.AddMinutes(60),
						signingCredentials: credenciales
					);
					return Ok(new JwtSecurityTokenHandler().WriteToken(token));
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}

		// POST api/<controller>
		[HttpPost]
		public async Task<IActionResult> Post([FromForm] Propietario entidad)
		{
			try
			{
				if (ModelState.IsValid)
				{
					await Contexto.Propietarios.AddAsync(entidad);
					Contexto.SaveChanges();
					return CreatedAtAction(nameof(Get), new { id = entidad.Id }, entidad);
				}
				return BadRequest();
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}

		[HttpPut]
		public async Task<IActionResult> Put([FromBody] Propietario entidad)
		{
			try
			{
				if (ModelState.IsValid)
				{
					Propietario original = await Contexto.Propietarios.AsNoTracking().SingleAsync(x=>x.Email == User.Identity.Name);
					entidad.Id = original.Id;
					if (String.IsNullOrEmpty(entidad.Clave))
					{
						entidad.Clave = original.Clave;
					}
					else
					{
						entidad.Clave = Convert.ToBase64String(KeyDerivation.Pbkdf2(
							password: entidad.Clave,
							salt: System.Text.Encoding.ASCII.GetBytes(config["Salt"]),
							prf: KeyDerivationPrf.HMACSHA1,
							iterationCount: 1000,
							numBytesRequested: 256 / 8));
					}
					Contexto.Propietarios.Update(entidad);
					await Contexto.SaveChangesAsync();
					return Ok(entidad);
				}
				return BadRequest();
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
        }
    }
}