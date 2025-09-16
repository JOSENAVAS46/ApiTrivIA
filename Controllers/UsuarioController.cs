using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiTrivIA.Models;
using ApiTrivIA.Services;

namespace ApiTrivIA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }


        [HttpGet]
        [Route("ListarUsuario")]
        public async Task<ActionResult<List<Usuario>>> listarUsuario(){
            return Ok(await _usuarioService.ListarUsuario());
        }


        [HttpGet]
        [Route("ObtenerUsuario/{usuario}")]
        public async Task<ActionResult<List<Usuario>>> obtenerUsuario(string usuario)
        {
            var objUsuario = await _usuarioService.ObtenerUsuario(usuario);

            if(objUsuario == null)
            {
                return NotFound("No se ha encontrado registro.");
            }
            else
            {
                return Ok(objUsuario);
            }            
        }

        [HttpPost]
        [Route("CrearUsuario")]
        public async Task<ActionResult> crearUsuario(Usuario objeto)
        {
            var resultado = await _usuarioService.CrearUsuario(objeto);

            if (resultado.Contains("\"status\": \"FALSE\""))
            {
                
                return BadRequest(resultado);
            }
            else
            {
                return Ok(resultado);
            }
        }

        [HttpPut]
        [Route("EditarUsuario")]
        public async Task<ActionResult> editarUsuario(Usuario objeto)
        {
            var resultado = await _usuarioService.EditarUsuario(objeto);

            if (resultado != "")
            {
                return BadRequest(resultado);
            }
            else
            {
                return Ok("Se ha modificado exitosamente.");
            }
        }

        [HttpPut]
        [Route("EliminarUsuario")]
        public async Task<ActionResult> eliminarUsuario(string uidusuario)
        {
            await _usuarioService.EliminarUsuario(uidusuario);
            return Ok();
        }
    }
}
