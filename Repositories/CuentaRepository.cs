using digitalArsv1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace digitalArsv1.Repositories
{
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

        public async Task<Cuenta> GetByIdWithUsuarioAsync(int id)
        {
            return await _context.Cuentas
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.nro_cuenta == id);
        }

        // Los métodos AddAsync, Update, Delete y SaveAsync ya están implementados en Repository<T>
        // Por ejemplo, para agregar:
        // await AddAsync(cuenta);
        // await SaveAsync();
    }
}