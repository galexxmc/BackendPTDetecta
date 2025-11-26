using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendPTDetecta.Infrastructure.Data;
using BackendPTDetecta.Domain.Entities;

namespace BackendPTDetecta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialClinicoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HistorialClinicoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/HistorialClinico/paciente/1
        [HttpGet("paciente/{idPaciente}")]
        public async Task<ActionResult<HistorialClinico>> GetPorPaciente(int idPaciente)
        {
            var historial = await _context.HistorialesClinicos
                .FirstOrDefaultAsync(h => h.IdPaciente == idPaciente && h.EstadoRegistro == 1);

            if (historial == null) return NotFound(new { mensaje = "No se encontró historial activo para este paciente." });
            
            return Ok(historial);
        }

        // PUT: api/HistorialClinico/paciente/1
        [HttpPut("paciente/{idPaciente}")]
        public async Task<IActionResult> ActualizarHistorial(int idPaciente, HistorialClinico datos)
        {
            var historial = await _context.HistorialesClinicos
                .FirstOrDefaultAsync(h => h.IdPaciente == idPaciente);

            if (historial == null) return NotFound("Historial no encontrado.");

            // Actualizamos datos médicos
            historial.GrupoSanguineo = datos.GrupoSanguineo;
            historial.AlergiasPrincipales = datos.AlergiasPrincipales;
            historial.AntecedentesHereditarios = datos.AntecedentesHereditarios;
            historial.EnfermedadesCronicas = datos.EnfermedadesCronicas;
            historial.EstadoPacienteActual = datos.EstadoPacienteActual;

            // Auditoría
            historial.UsuarioModificacion = datos.UsuarioModificacion;
            
            await _context.SaveChangesAsync();
            return Ok(historial);
        }
    }
}