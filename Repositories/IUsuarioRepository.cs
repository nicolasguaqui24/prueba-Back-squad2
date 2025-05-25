using digitalArsv1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace digitalArsv1.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<List<Usuario>> ObtenerUsuariosSinCuentaAsync();
        Task EliminarUsuariosAsync(List<Usuario> usuarios);
    }
}