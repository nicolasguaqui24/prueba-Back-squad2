using digitalArsv1.DTOs;
using digitalArsv1.Models;
using digitalArsv1.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuariosController(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios()
    {
        var usuarios = await _usuarioRepository.GetAllAsync();
        var usuariosDto = usuarios.Select(u => new UsuarioDTO
        {
            Id = u.nro_cliente,
            mail = u.mail,
            Nombre = u.nombre,
            tipo_cliente = u.tipo_cliente
        });

        return Ok(usuariosDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UsuarioDTO>> GetUsuario(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario == null)
            return NotFound();

        var usuarioDto = new UsuarioDTO
        {
            Id = usuario.nro_cliente,
            mail = usuario.mail,
            Nombre = usuario.nombre,
            tipo_cliente = usuario.tipo_cliente
        };

        return Ok(usuarioDto);
    }

    [HttpPost]//METODO POST
    public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
    {
        await _usuarioRepository.CrearAsync(usuario);
        await _usuarioRepository.SaveAsync();
        return CreatedAtAction(nameof(GetUsuario), new { id = usuario.nro_cliente }, usuario);
        
    }

   /* [HttpPut("{id}")]
    public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
    {
        if (id != usuario.nro_cliente)
            return BadRequest();

        _usuarioRepository.Update(usuario);
        await _usuarioRepository.SaveAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuario(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario == null)
            return NotFound();

        _usuarioRepository.Delete(usuario);
        await _usuarioRepository.SaveAsync();

        return NoContent();
    }
    */
}