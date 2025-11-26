using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendPTDetecta.Domain.Entities
{
    [Table("HISTORIAS_CLINICAS")]
    public class HistorialClinico : EntidadAuditable
    {
        [Key]
        [Column("NU_ID_HIS_CLINICA")]
        public int IdHistorialClinico { get; set; }

        // --- ESTOS FALTABAN ---
        [Required]
        [Column("TX_CODIGO_HISTORIA")]
        [MaxLength(20)]
        public string CodigoHistoria { get; set; } = string.Empty;

        [Column("FE_APERTURA")]
        public DateTime FechaApertura { get; set; }

        [Column("TX_GRUP_SANG")]
        [MaxLength(10)]
        public string GrupoSanguineo { get; set; } = string.Empty;

        [Column("TX_ALER_PRIN")]
        public string AlergiasPrincipales { get; set; } = string.Empty;

        [Column("TX_ENFER_CRONI")]
        public string EnfermedadesCronicas { get; set; } = string.Empty;

        [Column("TX_ANTE_HEREDI")]
        public string AntecedentesHereditarios { get; set; } = string.Empty;

        [Column("TX_ESTADO_PACIEN")]
        public string EstadoPacienteActual { get; set; } = string.Empty;
        // ----------------------

        // FK hacia Paciente (Relación 1 a 1 inversa)
        [Column("NU_ID_PACIENTE")]
        public int IdPaciente { get; set; }
        
        [ForeignKey("IdPaciente")]
        public virtual Paciente? Paciente { get; set; }
    }
}