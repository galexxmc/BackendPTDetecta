using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendPTDetecta.Domain.Entities
{
    [Table("TIPOS_SEGURO")]
    public class TipoSeguro : EntidadAuditable
    {
        [Key]
        [Column("NU_ID_TIPO_SEGURO")]
        public int IdTipoSeguro { get; set; }

        [Required]
        [Column("TX_NOMBRE_SEGURO")]
        [MaxLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [Column("TX_DESCRIPCION")]
        [MaxLength(200)]
        public string? Descripcion { get; set; }
    }
}