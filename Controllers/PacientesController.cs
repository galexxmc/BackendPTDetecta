using Microsoft.AspNetCore.Mvc;
using BackendPTDetecta.Domain.Entities;
using BackendPTDetecta.Application.DTOs; // Asegúrate de tener tus DTOs aquí
using BackendPTDetecta.Application.Interfaces;

namespace BackendPTDetecta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        // 1. SOLO inyectamos la interfaz (Abstracción)
        private readonly IPacienteRepository _repository;

        public PacientesController(IPacienteRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Pacientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PacienteDetalleDTO>>> GetPacientes()
        {
            // El trabajo sucio lo hace el repositorio
            var pacientes = await _repository.ObtenerTodosConDetalleAsync();

            // Mapeo a DTO (Idealmente usarías AutoMapper, pero manual es válido y explícito)
            var resultado = pacientes.Select(p => MapToDetalleDTO(p));
            return Ok(resultado);
        }

        // GET: api/Pacientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PacienteDetalleDTO>> GetPaciente(int id)
        {
            var p = await _repository.ObtenerPorIdConDetalleAsync(id);
            if (p == null) return NotFound();

            return Ok(MapToDetalleDTO(p));
        }

        // POST: api/Pacientes
        [HttpPost]
        public async Task<ActionResult<PacienteDetalleDTO>> PostPaciente(PacienteCrearDTO datos)
        {
            // 1. Validación de Negocio (Delegada al repo)
            if (await _repository.ExisteDniAsync(datos.Dni))
            {
                return BadRequest(new { mensaje = "El DNI ya está registrado en el sistema." });
            }

            // 2. Mapeo DTO -> Entidad
            var nuevoPaciente = new Paciente
            {
                Dni = datos.Dni,
                Nombres = datos.Nombres,
                Apellidos = datos.Apellidos,
                Edad = datos.Edad,
                Sexo = datos.Sexo,
                FechaNacimiento = datos.FechaNacimiento,
                Direccion = datos.Direccion,
                Telefono = datos.Telefono,
                Email = datos.Email,
                IdTipoSeguro = datos.IdTipoSeguro,
                // Auditoría
                UsuarioRegistro = datos.UsuarioRegistro ?? "WebUser", // Fallback
                FechaRegistro = DateTime.UtcNow,
                EstadoRegistro = 1
            };

            // 3. Persistencia (El repositorio se encargará de crear el Historial Clínico también)
            var creado = await _repository.CrearAsync(nuevoPaciente);

            // 4. Retornar 201 Created
            return CreatedAtAction(nameof(GetPaciente), new { id = creado.IdPaciente }, MapToDetalleDTO(creado));
        }

        // PUT: api/Pacientes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaciente(int id, PacienteCrearDTO datos)
        {
            // OJO: Aquí deberías recibir un DTO de Actualización, no PacienteCrearDTO, pero lo reusamos por ahora.
            
            var pacienteExistente = await _repository.ObtenerPorIdConDetalleAsync(id);
            if (pacienteExistente == null) return NotFound();

            // Actualizamos campos
            pacienteExistente.Dni = datos.Dni;
            pacienteExistente.Nombres = datos.Nombres;
            pacienteExistente.Apellidos = datos.Apellidos;
            pacienteExistente.Sexo = datos.Sexo;
            pacienteExistente.FechaNacimiento = datos.FechaNacimiento;                  
            pacienteExistente.Edad = datos.Edad;
            pacienteExistente.Direccion = datos.Direccion;
            pacienteExistente.Telefono = datos.Telefono;
            pacienteExistente.Email = datos.Email;
            pacienteExistente.IdTipoSeguro = datos.IdTipoSeguro;
            pacienteExistente.UsuarioModificacion = datos.UsuarioRegistro; // O un campo específico usuarioModificacion
            pacienteExistente.FechaModificacion = DateTime.UtcNow;

            await _repository.ActualizarAsync(pacienteExistente);

            return NoContent();
        }

        // PUT: api/Pacientes/eliminar/5
        [HttpPut("eliminar/{id}")]
        public async Task<IActionResult> DeleteLogico(int id, [FromBody] EliminarDTO data) // Usamos DTO, no dynamic
        {
            var exito = await _repository.EliminarLogicoAsync(id, data.UsuarioEliminacion, data.MotivoEliminacion);
            if (!exito) return NotFound();

            return NoContent();
        }

        // GET: api/Pacientes/buscar-eliminado/12345678
        [HttpGet("buscar-eliminado/{dni}")]
        public async Task<ActionResult<PacienteDetalleDTO>> BuscarEliminado(string dni)
        {
            var paciente = await _repository.BuscarEliminadoPorDniAsync(dni);
            if (paciente == null) return NotFound(new { mensaje = "No se encontró un paciente eliminado con este DNI." });
            
            return Ok(MapToDetalleDTO(paciente));
        }

        // PUT: api/Pacientes/habilitar/5
        [HttpPut("habilitar/{id}")]
        public async Task<IActionResult> Habilitar(int id)
        {
            var exito = await _repository.HabilitarPacienteAsync(id);
            if (!exito) return NotFound();
            return Ok(new { mensaje = "Paciente habilitado correctamente" });
        }

        // --- HELPER: Mapeo Manual (Para mantener el controller limpio) ---
        // En un proyecto real, esto iría en una capa de Mappers o usarías AutoMapper
        private static PacienteDetalleDTO MapToDetalleDTO(Paciente p)
        {
            return new PacienteDetalleDTO
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
                    EstadoPacienteActual = p.HistorialClinico.EstadoPacienteActual
                } : null
            };
        }
    }

    // DTO auxiliar para la eliminación (Ponlo en su propio archivo idealmente)
    public class EliminarDTO
    {
        public string UsuarioEliminacion { get; set; } = "Sistema";
        public string MotivoEliminacion { get; set; } = "Sin motivo";
    }
}