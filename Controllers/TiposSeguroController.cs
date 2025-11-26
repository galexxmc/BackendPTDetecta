using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendPTDetecta.Infrastructure.Data;
using BackendPTDetecta.Domain.Entities;
using BackendPTDetecta.Application.DTOs; // <--- 1. IMPORTANTE: Agregar este using

namespace BackendPTDetecta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposSeguroController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TiposSeguroController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TiposSeguro
        // 2. Cambiamos el tipo de retorno a TipoSeguroDTO
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoSeguroDTO>>> GetSeguros()
        {
            var seguros = await _context.TiposSeguro
                .Where(s => s.EstadoRegistro == 1)
                // 3. Proyectamos hacia el DTO explícitamente
                .Select(s => new TipoSeguroDTO 
                {
                    IdTipoSeguro = s.IdTipoSeguro,
                    NombreSeguro = s.NombreSeguro,
                    TipoCobertura = s.TipoCobertura,
                    CoPago = s.CoPago
                })
                .ToListAsync();

            return Ok(seguros);
        }
    }
}