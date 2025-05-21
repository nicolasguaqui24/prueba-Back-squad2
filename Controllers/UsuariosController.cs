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

    [HttpGet] //METODO GET
    public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
    {
        var usuarios = await _usuarioRepository.GetAllAsync();
        return Ok(usuarios);
    }

    [HttpGet("{id}")]//METODO GET por id
    public async Task<ActionResult<Usuario>> GetUsuario(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario == null)
            return NotFound();

        return Ok(usuario);
    }

    [HttpPost]//METODO POST
    public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
    {
        await _usuarioRepository.AddAsync(usuario);
        return CreatedAtAction(nameof(GetUsuario), new { id = usuario.nro_cliente }, usuario);
    }

    [HttpPut("{id}")]
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
}