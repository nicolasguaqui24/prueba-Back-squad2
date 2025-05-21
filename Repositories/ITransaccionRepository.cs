using digitalArsv1.Models;
using digitalArsv1.Repositories;

public interface ITransaccionRepository : IRepository<Transaccion>
{
    Task UpdateAsync(Transaccion transaccion);
    Task DeleteAsync(int id);

    // Otros métodos específicos que quieras agregar
}