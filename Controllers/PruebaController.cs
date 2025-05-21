using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using digitalArsv1.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace digitalArsv1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PruebaController : ControllerBase
    {
        private readonly DigitalArsContext _context;

        public PruebaController(DigitalArsContext context)
        {
            _context = context;
        }

        [HttpGet("test-cuentas")]
        public async Task<ActionResult<IEnumerable<Cuenta>>> TestCuentas()
        {
            var cuentas = await _context.Cuentas.ToListAsync();

            if (cuentas.Count == 0)
            {
                return NotFound("No se encontraron cuentas en la base de datos.");
            }

            return Ok(cuentas);
        }
    }
}