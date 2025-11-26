namespace BackendPTDetecta.Application.DTOs
{
    public class HistorialClinicoDTO
    {
        public int IdHistorialClinico { get; set; }
        public string CodigoHistoria { get; set; } = string.Empty;
        public DateTime FechaApertura { get; set; }
        
        // Datos médicos
        public string GrupoSanguineo { get; set; } = string.Empty;
        public string AlergiasPrincipales { get; set; } = string.Empty;
        public string EnfermedadesCronicas { get; set; } = string.Empty;
        public string EstadoPacienteActual { get; set; } = string.Empty;
    }
}