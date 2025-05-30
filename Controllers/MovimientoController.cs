﻿using digitalArsv1.DTOs;
using digitalArsv1.Models;
using digitalArsv1.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class MovimientosController : ControllerBase
{
    private readonly IMovimientoRepository _movimientoRepository;

    public MovimientosController(IMovimientoRepository movimientoRepository)
    {
        _movimientoRepository = movimientoRepository;
    }
    [HttpGet]  // Obtiene la lista de movimientos
    public async Task<ActionResult<IEnumerable<Movimiento>>> GetMovimientos()
    {
        var movimientos = await _movimientoRepository.GetAllWithRelationsAsync();
        return Ok(movimientos);
    }

    [HttpGet("{id}")] // Busca un movimiento por su id (id_trx)
    public async Task<ActionResult<Movimiento>> GetMovimiento(int id)
    {
        var movimiento = await _movimientoRepository.GetByIdAsync(id);
        if (movimiento == null)
            return NotFound();

        return Ok(movimiento);
    }
   
    [HttpPost]// Recibe un JSON(en la prueba del metodo), con los datos para crear un nuevo movimiento
    public async Task<ActionResult<Movimiento>> PostMovimiento(MovimientoDTO dto)
    {
        var movimiento = new Movimiento
        {
            fecha = dto.fecha,
            monto = dto.monto,
            nro_cuenta_orig = dto.nro_cuenta_orig,
            nro_cuenta_dest = dto.nro_cuenta_dest,
            codigo_transaccion = dto.codigo_transaccion
        };

        await _movimientoRepository.AddAsync(movimiento);
        await _movimientoRepository.SaveAsync();
        return CreatedAtAction(nameof(GetMovimiento), new { id = movimiento.id_trx }, movimiento);
    }

    [HttpPut("{id}")]// Actualiza un movimiento existente (primero valida que el id de la URL coincida con el id del objeto enviado)

    public async Task<IActionResult> PutMovimiento(int id, Movimiento movimiento)
    {
        if (id != movimiento.id_trx)
            return BadRequest();

        _movimientoRepository.Update(movimiento);
        await _movimientoRepository.SaveAsync();

        return NoContent();
    }
    [HttpDelete("{id}")] // Busca el movimiento por id
    public async Task<IActionResult> DeleteMovimiento(int id)
    {
        var movimiento = await _movimientoRepository.GetByIdAsync(id);
        if (movimiento == null)
            return NotFound();

        _movimientoRepository.Delete(movimiento);
        await _movimientoRepository.SaveAsync();

        return NoContent();
    }
}