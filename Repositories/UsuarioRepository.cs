using digitalArsv1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace digitalArsv1.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(DigitalArsContext context) : base(context)
        {
        }

        public async Task<List<Usuario>> ObtenerUsuariosSinCuentaAsync()
        {
            return await _context.Usuarios
                .Where(u => !_context.Cuentas.Any(c => c.nro_cliente == u.nro_cliente))
                .ToListAsync();
        }

        public async Task EliminarUsuariosAsync(List<Usuario> usuarios)
        {
            _context.Usuarios.RemoveRange(usuarios);
            await _context.SaveChangesAsync();
        }
    }
}