using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientRegistration.API.Data;

namespace PatientRegistration.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConvenioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ConvenioController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var convenios = await _context.Convenios
                .Include(c => c.Pacientes)
                .Select(c => new {
                    c.Id,
                    c.Nome,
                    Pacientes = c.Pacientes.Select(p => new {
                        p.Id,
                        p.Nome,
                        p.Sobrenome,
                        p.CPF
                    })
                })
                .ToListAsync();
            return Ok(convenios);
        }
    }
}
