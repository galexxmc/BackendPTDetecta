namespace BackendPTDetecta.Application.DTOs
{
    public class PacienteDetalleDTO
    {
        public int IdPaciente { get; set; }
        public string Dni { get; set; } = string.Empty;

        // --- CAMPOS AGREGADOS PARA CORREGIR EL ERROR ---
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public int Edad { get; set; }
        public string Sexo { get; set; } = string.Empty;
        // -----------------------------------------------

        public string NombreCompleto { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Objetos Anidados
        public TipoSeguroDTO? Seguro { get; set; }
        public HistorialClinicoDTO? Historial { get; set; }
    }
}