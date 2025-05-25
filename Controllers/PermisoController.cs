using digitalArsv1.Models;
using digitalArsv1.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace digitalArsv1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermisoController : ControllerBase
    {
        private readonly IPermisoRepository _permisoRepository;

        public PermisoController(IPermisoRepository permisoRepository)
        {
            _permisoRepository = permisoRepository;
        }

        [HttpGet("{nroUsuario}/tiene-permiso/{acceso}")]
        public async Task<ActionResult<bool>> TienePermiso(int nroUsuario, string acceso)
        {
            bool tienePermiso = await _permisoRepository.ExistePermisoAsync(nroUsuario, acceso);
            return Ok(tienePermiso);
        }
        // GET: VER PERMISO PARA NRO USUARIO
        [HttpGet("{nroUsuario}")]
        public async Task<ActionResult<IEnumerable<Permiso>>> GetPermisosByUsuario(int nroUsuario)
        {
            var permisos = await _permisoRepository.GetPermisosByUsuarioAsync(nroUsuario);
            return Ok(permisos);
        }
        // POST: api/permiso
            [HttpPost]
            public async Task<ActionResult> CrearPermiso([FromBody] Permiso permiso)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState); // devuelve los errores de validación

                await _permisoRepository.AddAsync(permiso);
                await _permisoRepository.SaveAsync();
                return CreatedAtAction(nameof(TienePermiso), new { nroUsuario = permiso.nro_usuario, acceso = permiso.acceso }, permiso);
            }

        

        // DELETE: api/permiso/5/admin
        [HttpDelete("{nroUsuario}/{acceso}")]
        public async Task<ActionResult> EliminarPermiso(int nroUsuario, string acceso)
        {
            var permisos = await _permisoRepository.GetPermisosByUsuarioAsync(nroUsuario);
            var permiso = permisos.FirstOrDefault(p => p.acceso == acceso);
            if (permiso == null)
                return NotFound();

            _permisoRepository.Delete(permiso);
            await _permisoRepository.SaveAsync();
            return NoContent();
        }
    }
}
