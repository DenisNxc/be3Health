using Microsoft.EntityFrameworkCore;
using PatientRegistration.API.Data;
using PatientRegistration.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientRegistration.API.Repositories
{
    public class PacienteRepository : IPacienteRepository
    {
        private readonly ApplicationDbContext _context;

        public PacienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Paciente>> GetAllAsync()
        {
            return await _context.Pacientes
                .Include(p => p.Convenio)
                .ToListAsync();
        }

        public async Task<Paciente?> GetByIdAsync(int id)
        {
            return await _context.Pacientes
                .Include(p => p.Convenio)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Paciente paciente)
        {
            await _context.Pacientes.AddAsync(paciente);
        }

        public Task UpdateAsync(Paciente paciente)
        {
            _context.Pacientes.Update(paciente);
            return Task.CompletedTask;
        }

        public async Task<bool> ExistsByCpfAsync(string cpf)
        {
            return await _context.Pacientes.AnyAsync(p => p.CPF == cpf);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
