namespace Taller_REST_API.Entities
{
    public class ActivoDto
    {
        public long ActivoId { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public DateTime FechaCompra { get; set; }

    }
}
