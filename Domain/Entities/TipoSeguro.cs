using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendPTDetecta.Domain.Entities
{
    [Table("TIPO_SEGUROS")] // Nombre plural según tu requisito
    public class TipoSeguro : EntidadAuditable
    {
        [Key]
        [Column("NU_ID_TIPO_SEGURO")]
        public int IdTipoSeguro { get; set; }

        [Required]
        [Column("TX_NOM_SEG")]
        [MaxLength(100)]
        public string NombreSeguro { get; set; } = string.Empty;

        [Required]
        [Column("NU_RUC_EMPRESA")]
        [MaxLength(11)] // RUC Perú tiene 11 dígitos
        public string RucEmpresa { get; set; } = string.Empty;

        [Column("TX_TIP_COBER")]
        [MaxLength(100)]
        public string TipoCobertura { get; set; } = string.Empty;

        [Column("TX_CO_PAGO")]
        [MaxLength(50)]
        public string CoPago { get; set; } = string.Empty; // Ej: "10%" o "S/ 50.00"
    }
}