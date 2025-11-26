using System.ComponentModel.DataAnnotations.Schema;

namespace BackendPTDetecta.Domain.Entities
{
    public abstract class EntidadAuditable
    {
        [Column("TX_ID_USU_REG")]
        public string UsuarioRegistro { get; set; } = string.Empty;

        [Column("FE_REG")]
        public DateTime FechaRegistro { get; set; }

        [Column("TX_ID_USU_MOD")]
        public string? UsuarioModificacion { get; set; }

        [Column("FE_MOD")]
        public DateTime? FechaModificacion { get; set; }

        [Column("TX_ID_USU_ELI")]
        public string? UsuarioEliminacion { get; set; }

        [Column("FE_ELI")]
        public DateTime? FechaEliminacion { get; set; }

        [Column("TX_MOTIVO_ELI")]
        public string? MotivoEliminacion { get; set; }

        [Column("NU_ESTADO_REGISTRO")]
        public int EstadoRegistro { get; set; } = 1;
    }
}