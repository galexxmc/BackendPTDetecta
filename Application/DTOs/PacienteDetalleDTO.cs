namespace BackendPTDetecta.Application.DTOs
{
    public class PacienteDetalleDTO
    {
        public int IdPaciente { get; set; }
        public string CodigoPaciente { get; set; } = string.Empty;
        public string Dni { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public int Edad
        {
            get
            {
                var hoy = DateTime.Today;
                var edad = hoy.Year - FechaNacimiento.Year;
                if (FechaNacimiento.Date > hoy.AddYears(-edad))
                    edad--;
                return edad;
            }
        }
        public string Sexo { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;

        public string Direccion { get; set; } = string.Empty;

        public DateTime FechaNacimiento { get; set; }

        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public TipoSeguroDTO? Seguro { get; set; }
        public HistorialClinicoDTO? Historial { get; set; }
    }
}