using digitalArsv1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace digitalArsv1.Repositories
{
    public class MovimientoRepository : Repository<Movimiento>, IMovimientoRepository
    {
        private readonly DigitalArsContext _context;

        public MovimientoRepository(DigitalArsContext context) : base(context)
        {
            _context = context;
        }

        // Método para traer todos los movimientos 
        public async Task<IEnumerable<Movimiento>> GetAllWithRelationsAsync()
        {
            return await _context.Movimientos
                .Include(m => m.CuentaOrig)
                .Include(m => m.CuentaDest)
                .Include(m => m.Transaccion)
                .ToListAsync();
        }

        
    }
}