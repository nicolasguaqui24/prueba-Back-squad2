using digitalArsv1.Models;
using digitalArsv1.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class CuentasController : ControllerBase
{
    private readonly ICuentaRepository _cuentaRepository;

    public CuentasController(ICuentaRepository cuentaRepository)
    {
        _cuentaRepository = cuentaRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Cuenta>>> GetCuentas()
    {
        var cuentas = await _cuentaRepository.GetAllWithUsuarioAsync();
        return Ok(cuentas);
    }
    // Metodo GET para obtener saldo de una cuenta 
    [HttpGet("{nroCuenta}/saldo")]
    public async Task<ActionResult<decimal>> ObtenerSaldo(int nroCuenta)
    {
        var saldo = await _cuentaRepository.ObtenerSaldoAsync(nroCuenta);
        return Ok(saldo);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Cuenta>> GetCuenta(int id)
    {
        var cuenta = await _cuentaRepository.GetByIdWithUsuarioAsync(id);
        if (cuenta == null)
            return NotFound();

        return Ok(cuenta);
    }
/*
    [HttpPost]
    public async Task<ActionResult<Cuenta>> PostCuenta(Cuenta cuenta)
    {
        await _cuentaRepository.AddAsync(cuenta);
        await _cuentaRepository.SaveAsync();

        return CreatedAtAction(nameof(GetCuenta), new { id = cuenta.nro_cuenta }, cuenta);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCuenta(int id, Cuenta cuenta)
    {
        if (id != cuenta.nro_cuenta)
            return BadRequest();

        _cuentaRepository.Update(cuenta);
        await _cuentaRepository.SaveAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCuenta(int id)
    {
        var cuenta = await _cuentaRepository.GetByIdAsync(id);
        if (cuenta == null)
            return NotFound();

        _cuentaRepository.Delete(cuenta);
        await _cuentaRepository.SaveAsync();

        return NoContent();
    }
*/}