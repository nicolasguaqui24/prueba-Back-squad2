using digitalArsv1.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace digitalArsv1.Repositories;

    public interface IMovimientoRepository : IRepository<Movimiento>
    {
        // Métodos específicos para Movimiento que no están en IRepository
        Task<IEnumerable<Movimiento>> GetAllWithRelationsAsync();
    }
