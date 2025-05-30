using System.Collections.Generic;
using System.Threading.Tasks;
using PatientRegistration.API.Models;
using PatientRegistration.API.Repositories;
using PatientRegistration.API.DTOs;
using System.Linq;


namespace PatientRegistration.API.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _repo;

        public PacienteService(IPacienteRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<PacienteDTO>> ListarPacientesAsync()
        {
            var pacientes = await _repo.GetAllAsync();

            return pacientes.Select(p => new PacienteDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Sobrenome = p.Sobrenome,
                DataNascimento = p.DataNascimento.HasValue ? p.DataNascimento.Value.ToString("yyyy-MM-dd") : null,
                Genero = p.Genero,
                CPF = p.CPF,
                RG = p.RG,
                UfRg = p.UfRg,
                Email = p.Email,
                Celular = p.Celular,
                TelefoneFixo = p.TelefoneFixo,
                Ativo = p.Ativo,
                ConvenioNome = p.Convenio?.Nome,
                NumeroCarteirinha = p.NumeroCarteirinha,
                ValidadeCarteirinha = p.ValidadeCarteirinha
            });
        }


        public async Task<Paciente?> BuscarPorIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task CadastrarPacienteAsync(Paciente paciente)
        {
            if (!string.IsNullOrEmpty(paciente.CPF))
            {
                if (await _repo.ExistsByCpfAsync(paciente.CPF))
                    throw new System.Exception("CPF já cadastrado.");
            }

            await _repo.AddAsync(paciente);
            await _repo.SaveChangesAsync();
        }

        public async Task EditarPacienteAsync(Paciente paciente)
        {
            await _repo.UpdateAsync(paciente);
            await _repo.SaveChangesAsync();
        }
    }
}
