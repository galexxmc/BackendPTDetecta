using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendPTDetecta.Domain.Entities
{
    [Table("PACIENTES")]
    public class Paciente : EntidadAuditable
    {
        [Key]
        [Column("NU_ID_PACIENTE")]
        public int IdPaciente { get; set; }

        [Column("TX_CODIGO_PACIENTE")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)] 
        public string Codigo { get; set; } = string.Empty;

        [Required]
        [Column("NU_DNI_PACIEN")]
        [MaxLength(20)]
        public string Dni { get; set; } = string.Empty;

        [Required]
        [Column("TX_NOM_PACIEN")]
        [MaxLength(100)]
        public string Nombres { get; set; } = string.Empty;

        [Required]
        [Column("TX_APE_PACIEN")]
        [MaxLength(100)]
        public string Apellidos { get; set; } = string.Empty;

        [Column("TX_SEXO_PACIEN")]
        [MaxLength(1)]
        public string Sexo { get; set; } = string.Empty;

        [Column("TX_FE_NAC_PACIEN", TypeName = "date")]
        public DateTime FechaNacimiento { get; set; }

        [Column("TX_DIR_PACIEN")]
        [MaxLength(200)]
        public string Direccion { get; set; } = string.Empty;

        [Column("NU_TEL_PACIEN")]
        [MaxLength(20)]
        public string Telefono { get; set; } = string.Empty;

        [Column("TX_EMAIL_PACIEN")]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        
        public virtual HistorialClinico? HistorialClinico { get; set; }
        
        [Column("NU_ID_TIPO_SEGURO")]
        public int? IdTipoSeguro { get; set; }
        
        [ForeignKey("IdTipoSeguro")]
        public virtual TipoSeguro? TipoSeguro { get; set; }
    }
}