using digitalArsv1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace digitalArsv1.Repositories
{
   
    // CuentaRepository hereda de Repository<Cuenta> y implmnta ICuentaRepository
    public class CuentaRepository : Repository<Cuenta>, ICuentaRepository
    {
        private readonly DigitalArsContext _context;

        public CuentaRepository(DigitalArsContext context) : base(context)
        {
            _context = context;
        }
      
        public async Task<IEnumerable<Cuenta>> GetAllWithUsuarioAsync()
        {
            return await _context.Cuentas
                .Include(c => c.Usuario)
                .ToListAsync();
        }
        // Obtiene una cuenta por id incluyendo el usuario
        public async Task<Cuenta> GetByIdWithUsuarioAsync(int id)
        {
            return await _context.Cuentas
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.nro_cuenta == id);
        }
        //metodo especifico, para obtener saldo de una cuenta
        // los montos recibidos (ingresos) y resta los montos enviados

        public async Task<decimal> ObtenerSaldoAsync(int nroCuenta)
        {
            var ingresos = await _context.Movimientos
                .Where(m => m.nro_cuenta_dest == nroCuenta)
                .SumAsync(m => (decimal?)m.monto) ?? 0;

            var egresos = await _context.Movimientos
                .Where(m => m.nro_cuenta_orig == nroCuenta)
                .SumAsync(m => (decimal?)m.monto) ?? 0;

            return ingresos - egresos;
        }
    
    }
}