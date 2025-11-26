using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendPTDetecta.Domain.Entities
{
    [Table("HISTORIAS_CLINICAS")]
    public class HistoriaClinica : EntidadAuditable
    {
        [Key]
        [Column("NU_ID_HIS_CLINICA")]
        public int IdHistoriaClinica { get; set; }

        [Required]
        [Column("TX_CODIGO_HISTORIA")]
        [MaxLength(20)]
        public string CodigoHistoria { get; set; } = string.Empty;

        [Column("FE_APERTURA")]
        public DateTime FechaApertura { get; set; }
    }
}