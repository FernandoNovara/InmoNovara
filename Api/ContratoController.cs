// using System;
// using System.Threading.Tasks;
// using InmoNovara.Models;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using System.Linq;

// namespace Inmobiliaria_.Net_Core.Api
// {
// 	[Route("api/[controller]")]
// 	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

//     public class ContratoController : ControllerBase
// 	{
//         private readonly DataContext Contexto;

//         public ContratoController(DataContext contexto)
//         {
//             this.Contexto = contexto;
//         }

//         [HttpGet]
//         public async Task<IActionResult> Get()
//         {
//             try
//             {
//                 var usuario = User.Identity.Name;
//                 var Inmueble = contexto.Inmueble.Where(e => e.Propietario.Email == usuario);
//                 return Ok(Contexto.Contrato.Where(e=> e.Contrato.IdInmueble == Inmueble.IdInmueble ));
//             }
//             catch(Exception ex)
//             {
//                 return BadRequest(ex);
//             }
//         }

//     }
// }