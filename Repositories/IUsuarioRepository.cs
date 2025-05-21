using digitalArsv1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace digitalArsv1.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(int id);
       
    }
}