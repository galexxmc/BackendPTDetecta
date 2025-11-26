namespace BackendPTDetecta.Application.DTOs
{
    public class TipoSeguroDTO
    {
        public int IdTipoSeguro { get; set; }
        public string NombreSeguro { get; set; } = string.Empty;
        public string TipoCobertura { get; set; } = string.Empty;
        public string CoPago { get; set; } = string.Empty;
    }
}