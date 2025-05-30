using System.Collections.Generic;
using System.Threading.Tasks;
using PatientRegistration.API.Models;

namespace PatientRegistration.API.Repositories
{
    public interface IPacienteRepository
    {
        Task<IEnumerable<Paciente>> GetAllAsync();
        Task<Paciente?> GetByIdAsync(int id);
        Task AddAsync(Paciente paciente);
        Task UpdateAsync(Paciente paciente);
        Task<bool> ExistsByCpfAsync(string cpf);
        Task SaveChangesAsync();
    }
}
