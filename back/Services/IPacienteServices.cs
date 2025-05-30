using System.Collections.Generic;
using System.Threading.Tasks;
using PatientRegistration.API.DTOs;
using PatientRegistration.API.Models;

namespace PatientRegistration.API.Services
{
    public interface IPacienteService
    {
        Task<IEnumerable<PacienteDTO>> ListarPacientesAsync();
        Task<Paciente?> BuscarPorIdAsync(int id);
        Task CadastrarPacienteAsync(Paciente paciente);
        Task EditarPacienteAsync(Paciente paciente);
    }
}
