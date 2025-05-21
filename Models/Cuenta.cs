using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace digitalArsv1.Models
{
    public class Cuenta
    {
        [Key]
        public int nro_cuenta { get; set; }
        public string producto { get; set; } = string.Empty;
        public string CBU { get; set; } = string.Empty;
        public bool estado { get; set; }
        public int nro_cliente { get; set; }
        public string? rol_cta { get; set; }

        // Relación con Usuario (NroCliente)
        [ForeignKey("nro_cliente")]
        public Usuario? Usuario { get; set; }

        // Relación 1:N con Movimiento (origen y destino)
        public ICollection<Movimiento> MovimientosOrigen { get; set; } = new List<Movimiento>();
        public ICollection<Movimiento> MovimientosDestino { get; set; } = new List<Movimiento>();

    
    }
}