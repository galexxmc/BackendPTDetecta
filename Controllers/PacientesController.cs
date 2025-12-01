using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendPTDetecta.Infrastructure.Data;
using BackendPTDetecta.Domain.Entities;
using BackendPTDetecta.Application.DTOs;
using BackendPTDetecta.Application.Interfaces;


namespace BackendPTDetecta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPacienteRepository _repository;

        public PacientesController(ApplicationDbContext context,IPacienteRepository repository)
        {
            _context = context;
            _repository = repository; 
        }

        // GET: api/Pacientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PacienteDetalleDTO>>> GetPacientes()
        {
            // 1. Traemos la data con sus relaciones (JOINs)
            var pacientes = await _context.Pacientes
                .Include(p => p.TipoSeguro)
                .Include(p => p.HistorialClinico)
                .Where(p => p.EstadoRegistro == 1) // Solo activos
                .OrderByDescending(p => p.FechaRegistro)
                .ToListAsync();

            // 2. Mapeamos a DTO para romper el ciclo infinito y limpiar la respuesta
            var resultado = pacientes.Select(p => new PacienteDetalleDTO
            {
                IdPaciente = p.IdPaciente,
                Dni = p.Dni,
                CodigoPaciente = p.Codigo,
                Nombres = p.Nombres,
                Apellidos = p.Apellidos,
                Edad = p.Edad,
                Sexo = p.Sexo,
                Direccion = p.Direccion,
                FechaNacimiento = p.FechaNacimiento,
                Telefono = p.Telefono,
                Email = p.Email,
                
                // Objeto Seguro Limpio
                Seguro = p.TipoSeguro != null ? new TipoSeguroDTO
                {
                    IdTipoSeguro = p.TipoSeguro.IdTipoSeguro,
                    NombreSeguro = p.TipoSeguro.NombreSeguro,
                    TipoCobertura = p.TipoSeguro.TipoCobertura,
                    CoPago = p.TipoSeguro.CoPago
                } : null,

                // Objeto Historial Limpio
                Historial = p.HistorialClinico != null ? new HistorialClinicoDTO
                {
                    IdHistorialClinico = p.HistorialClinico.IdHistorialClinico,
                    CodigoHistoria = p.HistorialClinico.CodigoHistoria,
                    FechaApertura = p.HistorialClinico.FechaApertura,
                    GrupoSanguineo = p.HistorialClinico.GrupoSanguineo,
                    AlergiasPrincipales = p.HistorialClinico.AlergiasPrincipales,
                    EnfermedadesCronicas = p.HistorialClinico.EnfermedadesCronicas,
                    EstadoPacienteActual = p.HistorialClinico.EstadoPacienteActual
                } : null
            }).ToList();

            return Ok(resultado);
        }

        // GET: api/Pacientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteDetalleDTO>> GetPaciente(int id)
        {
            var p = await _context.Pacientes
                .Include(p => p.TipoSeguro)
                .Include(p => p.HistorialClinico)
                .FirstOrDefaultAsync(x => x.IdPaciente == id);

            if (p == null) return NotFound();

            // Mapeo Individual
            var dto = new PacienteDetalleDTO
            {
                IdPaciente = p.IdPaciente,
                CodigoPaciente = p.Codigo,
                Dni = p.Dni,
                Nombres = p.Nombres,
                Apellidos = p.Apellidos,
                Edad = p.Edad,
                Sexo = p.Sexo,
                Direccion = p.Direccion,
                FechaNacimiento = p.FechaNacimiento,
                Telefono = p.Telefono,
                Email = p.Email,
                
                Seguro = p.TipoSeguro != null ? new TipoSeguroDTO
                {
                    IdTipoSeguro = p.TipoSeguro.IdTipoSeguro,
                    NombreSeguro = p.TipoSeguro.NombreSeguro,
                    TipoCobertura = p.TipoSeguro.TipoCobertura,
                    CoPago = p.TipoSeguro.CoPago
                } : null,

                Historial = p.HistorialClinico != null ? new HistorialClinicoDTO
                {
                    IdHistorialClinico = p.HistorialClinico.IdHistorialClinico,
                    CodigoHistoria = p.HistorialClinico.CodigoHistoria,
                    FechaApertura = p.HistorialClinico.FechaApertura,
                    GrupoSanguineo = p.HistorialClinico.GrupoSanguineo,
                    AlergiasPrincipales = p.HistorialClinico.AlergiasPrincipales,
                    EnfermedadesCronicas = p.HistorialClinico.EnfermedadesCronicas,
                    EstadoPacienteActual = p.HistorialClinico.EstadoPacienteActual
                } : null
            };

            return Ok(dto);
        }

        // POST: api/Pacientes
        [HttpPost]
        public async Task<ActionResult<Paciente>> PostPaciente(Paciente paciente)
        {
            // Validación de DNI duplicado
            bool existeDni = await _context.Pacientes.AnyAsync(x => x.Dni == paciente.Dni && x.EstadoRegistro == 1);
            if (existeDni) return BadRequest(new { mensaje = "El DNI ya está registrado en el sistema." });

            // 1. Guardar Paciente
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();

            // 2. Crear Historial Clínico Vacío Automáticamente
            var historial = new HistorialClinico
            {
                IdPaciente = paciente.IdPaciente,
                CodigoHistoria = "HC-" + DateTime.Now.Year + "-" + paciente.IdPaciente.ToString("D4"),
                FechaApertura = DateTime.UtcNow,
                UsuarioRegistro = paciente.UsuarioRegistro,
                EstadoPacienteActual = "Ingresado"
            };
            _context.HistorialesClinicos.Add(historial);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPaciente), new { id = paciente.IdPaciente }, paciente);
        }

        // PUT: api/Pacientes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaciente(int id, Paciente datos)
        {
            if (id != datos.IdPaciente) return BadRequest("ID URL no coincide con ID Cuerpo");

            var existente = await _context.Pacientes.FindAsync(id);
            if (existente == null) return NotFound();

            // Actualizamos campos
            existente.Dni = datos.Dni;
            existente.Nombres = datos.Nombres;
            existente.Apellidos = datos.Apellidos;
            existente.Sexo = datos.Sexo;
            existente.FechaNacimiento = datos.FechaNacimiento;
            existente.Edad = datos.Edad;
            existente.Direccion = datos.Direccion;
            existente.Telefono = datos.Telefono;
            existente.Email = datos.Email;
            existente.IdTipoSeguro = datos.IdTipoSeguro;

            // Auditoría
            existente.UsuarioModificacion = datos.UsuarioModificacion;
            // Fechas se llenan solas en DbContext

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Pacientes.Any(e => e.IdPaciente == id)) return NotFound();
                throw;
            }

            return NoContent();
        }

        // PUT: api/Pacientes/eliminar/5
        [HttpPut("eliminar/{id}")]
        public async Task<IActionResult> DeleteLogico(int id, [FromBody] dynamic data)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null) return NotFound();

            // Extraer datos del JSON dinámico
            string usuario = "Sistema";
            string motivo = "Sin motivo";

            try 
            { 
                usuario = data.GetProperty("usuarioEliminacion").GetString(); 
                motivo = data.GetProperty("motivoEliminacion").GetString();
            } 
            catch { /* Si no envían datos, usamos defaults */ }

            // Soft Delete
            paciente.EstadoRegistro = 0;
            paciente.UsuarioEliminacion = usuario;
            paciente.MotivoEliminacion = motivo;
            paciente.FechaEliminacion = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // GET: api/Pacientes/buscar-eliminado/12345678
        [HttpGet("buscar-eliminado/{dni}")]
        public async Task<ActionResult<Paciente>> BuscarEliminado(string dni)
        {
            var paciente = await _repository.BuscarEliminadoPorDniAsync(dni);
            if (paciente == null) return NotFound("No se encontró un paciente eliminado con este DNI.");
            return Ok(paciente);
        }

        // PUT: api/Pacientes/habilitar/5
        [HttpPut("habilitar/{id}")]
        public async Task<IActionResult> Habilitar(int id)
        {
            var exito = await _repository.HabilitarPacienteAsync(id);
            if (!exito) return NotFound();
            return Ok(new { mensaje = "Paciente habilitado correctamente" });
        }
    }
}