namespace digitalArsv1.DTOs
{
    public class MovimientoDTO
    {
        public DateTime fecha { get; set; }
        public decimal monto { get; set; }
        public int nro_cuenta_orig { get; set; }
        public int? nro_cuenta_dest { get; set; }
        public int? codigo_transaccion { get; set; }
    }
}