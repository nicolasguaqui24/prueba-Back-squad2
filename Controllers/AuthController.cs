using digitalArsv1.Models;
using digitalArsv1.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace digitalArsv1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPermisoRepository _permisoRepository;
        private readonly IConfiguration _configuration;

        public AuthController(IUsuarioRepository usuarioRepository, IPermisoRepository permisoRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _permisoRepository = permisoRepository;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // 1. Buscar usuario por nro_cliente
            var usuario = await _usuarioRepository.GetByIdAsync(request.nro_usuario);
            if (usuario == null || usuario.estado == false) // 0 = inactivo
                return Unauthorized(new { mensaje = "Usuario no encontrado o inactivo" });

            // 2. Validar acceso según tipo_cliente y permisos
            if (usuario.tipo_cliente == "ADMINISTRADOR")
            {
                // Verificar si tiene permiso 'ADMINISTRADOR' en tabla Permisos
                bool tienePermiso = await _permisoRepository.ExistePermisoAsync(request.nro_usuario, "ADMINISTRADOR");
                if (!tienePermiso)
                    return Unauthorized(new { mensaje = "Permiso administrador no concedido" });
            }
            else if (usuario.tipo_cliente != "BILLETERA")
            {
                // Sólo permiten BILLETERA o ADMINISTRADOR
                return Unauthorized(new { mensaje = "Tipo de cliente no autorizado" });
            }

            // 3. Generar token JWT
            var token = GenerarToken(usuario.nro_cliente, usuario.tipo_cliente);

            // 4. Responder con token y mensaje
            return Ok(new
            {
                token,
                mensaje = "Inicio de sesión exitoso",
                tipo_cliente = usuario.tipo_cliente
            });
        }

        // Método privado para generar JWT
        private string GenerarToken(int nroUsuario, string tipoCliente)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, nroUsuario.ToString()),
                new Claim(ClaimTypes.Role, tipoCliente)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [Authorize(Roles = "ADMINISTRADOR")]
        [HttpDelete("eliminar-usuarios-sin-cuenta")]
        public async Task<IActionResult> EliminarUsuariosSinCuenta()
        {
            var usuariosSinCuenta = await _usuarioRepository.ObtenerUsuariosSinCuentaAsync();

            if (!usuariosSinCuenta.Any())
                return NotFound("No hay usuarios sin cuentas para eliminar.");

            await _usuarioRepository.EliminarUsuariosAsync(usuariosSinCuenta);

            return Ok(new
            {
                mensaje = $"{usuariosSinCuenta.Count} usuario(s) sin cuenta eliminados exitosamente.",
                usuariosEliminados = usuariosSinCuenta.Select(u => new
                {
                    u.nro_cliente,
                    u.nombre,
                    u.apellido,
                    u.mail
                })
            });
        }
    }
}
