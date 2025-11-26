using Microsoft.AspNetCore.Mvc;
using BackendPTDetecta.Application.Interfaces;
using BackendPTDetecta.Domain.Entities;

namespace BackendPTDetecta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteRepository _repo;

        // Inyección de Dependencias: Pedimos la Interfaz (Application), no la BD directa
        public PacientesController(IPacienteRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Pacientes
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var lista = await _repo.ObtenerTodosAsync();
            return Ok(lista);
        }

        // GET: api/Pacientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var item = await _repo.ObtenerPorIdAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/Pacientes
        [HttpPost]
        public async Task<ActionResult> Post(Paciente paciente)
        {
            var creado = await _repo.CrearAsync(paciente);
            return CreatedAtAction(nameof(GetById), new { id = creado.IdPaciente }, creado);
        }

        // PUT: api/Pacientes/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Paciente paciente)
        {
            if (id != paciente.IdPaciente) return BadRequest();
            await _repo.ActualizarAsync(paciente);
            return NoContent();
        }

        // PUT: api/Pacientes/eliminar/5 (Eliminado Lógico)
        [HttpPut("eliminar/{id}")]
        public async Task<ActionResult> DeleteLogico(int id, [FromBody] dynamic data)
        {
            // Leemos el JSON dinámico para sacar auditoría
            // Postman Body: { "usuarioEliminacion": "Admin", "motivoEliminacion": "Error" }
            string usuario = data.GetProperty("usuarioEliminacion").GetString();
            string motivo = data.GetProperty("motivoEliminacion").GetString();

            var exito = await _repo.EliminarLogicoAsync(id, usuario, motivo);
            if (!exito) return NotFound();
            return NoContent();
        }
    }
}