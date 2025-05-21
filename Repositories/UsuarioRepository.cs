using digitalArsv1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace digitalArsv1.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DigitalArsContext _context;

        public UsuarioRepository(DigitalArsContext context)
        {
            _context = context;
        }

        // Métodos del IRepository
        public IQueryable<Usuario> Query() => _context.Usuarios;

        public async Task<IEnumerable<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task AddAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public void Update(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
        }

        public void Delete(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        // Acá definís el método que usa Update + SaveAsync
        public async Task UpdateAsync(Usuario usuario)
        {
            Update(usuario);
            await SaveAsync();
        }

        // Similar para DeleteAsync por id
        public async Task DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return;

            Delete(usuario);
            await SaveAsync();
        }
    }
}