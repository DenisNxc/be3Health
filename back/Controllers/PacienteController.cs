using Microsoft.AspNetCore.Mvc;
using PatientRegistration.API.Models;
using PatientRegistration.API.Services;
using System;
using System.Threading.Tasks;
using PatientRegistration.API.DTOs;
using Microsoft.EntityFrameworkCore;
using PatientRegistration.API.Data;

namespace PatientRegistration.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _service;
        private readonly ApplicationDbContext _context;

        public PacienteController(IPacienteService service, ApplicationDbContext context)
        {
            _service = service;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pacientes = await _service.ListarPacientesAsync();
            return Ok(pacientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var paciente = await _service.BuscarPorIdAsync(id);
            if (paciente == null)
                return NotFound();

            var dto = new PacienteDTO
            {
                Id = paciente.Id,
                Nome = paciente.Nome,
                Sobrenome = paciente.Sobrenome,
                DataNascimento = paciente.DataNascimento.HasValue ? paciente.DataNascimento.Value.ToString("yyyy-MM-dd") : null,
                Genero = paciente.Genero,
                CPF = paciente.CPF,
                RG = paciente.RG,
                UfRg = paciente.UfRg,
                Email = paciente.Email,
                Celular = paciente.Celular,
                TelefoneFixo = paciente.TelefoneFixo,
                Ativo = paciente.Ativo,
                ConvenioNome = paciente.Convenio?.Nome,
                NumeroCarteirinha = paciente.NumeroCarteirinha,
                ValidadeCarteirinha = paciente.ValidadeCarteirinha
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PacienteDTO pacienteDto)
        {
            if (pacienteDto == null)
                return BadRequest("O corpo da requisição está vazio ou mal formatado. Envie um JSON válido.");

            // Validação dos campos obrigatórios (complementar ao DTO)
            if (string.IsNullOrWhiteSpace(pacienteDto.Nome))
                return BadRequest("O campo Nome é obrigatório.");
            if (string.IsNullOrWhiteSpace(pacienteDto.Sobrenome))
                return BadRequest("O campo Sobrenome é obrigatório.");
            if (string.IsNullOrWhiteSpace(pacienteDto.CPF))
                return BadRequest("O campo CPF é obrigatório.");
            if (string.IsNullOrWhiteSpace(pacienteDto.Email))
                return BadRequest("O campo Email é obrigatório.");
            if (string.IsNullOrWhiteSpace(pacienteDto.DataNascimento))
                return BadRequest("O campo Data de Nascimento é obrigatório.");
            else if (!DateTime.TryParse(pacienteDto.DataNascimento, out _))
                return BadRequest("O campo DataNascimento está em formato inválido (use yyyy-MM-dd).");

            // Remover validação de Celular obrigatório
            // if (string.IsNullOrWhiteSpace(pacienteDto.Celular))
            //     return BadRequest("O campo Celular é obrigatório.");

            try
            {
                int? convenioId = null;
                if (!string.IsNullOrEmpty(pacienteDto.ConvenioNome))
                {
                    var convenio = await _context.Convenios.FirstOrDefaultAsync(c => c.Nome == pacienteDto.ConvenioNome);
                    if (convenio == null)
                        return BadRequest($"Convênio 	'{pacienteDto.ConvenioNome}	' não encontrado.");
                    convenioId = convenio.Id;
                }

                var paciente = new Paciente
                {
                    Nome = pacienteDto.Nome ?? string.Empty,
                    Sobrenome = pacienteDto.Sobrenome ?? string.Empty,
                    DataNascimento = DateTime.Parse(pacienteDto.DataNascimento), // Já validamos que não é nulo e é data válida
                    Genero = pacienteDto.Genero,
                    CPF = pacienteDto.CPF,
                    RG = pacienteDto.RG,
                    UfRg = pacienteDto.UfRg,
                    Email = pacienteDto.Email ?? string.Empty,
                    Celular = pacienteDto.Celular,
                    TelefoneFixo = pacienteDto.TelefoneFixo,
                    Ativo = pacienteDto.Ativo,
                    NumeroCarteirinha = pacienteDto.NumeroCarteirinha,
                    ValidadeCarteirinha = pacienteDto.ValidadeCarteirinha,
                    ConvenioId = convenioId
                };

                await _service.CadastrarPacienteAsync(paciente);

                var dto = new PacienteDTO
                {
                    Id = paciente.Id,
                    Nome = paciente.Nome,
                    Sobrenome = paciente.Sobrenome,
                    DataNascimento = paciente.DataNascimento.Value.ToString("yyyy-MM-dd"),
                    Genero = paciente.Genero,
                    CPF = paciente.CPF,
                    RG = paciente.RG,
                    UfRg = paciente.UfRg,
                    Email = paciente.Email,
                    Celular = paciente.Celular,
                    TelefoneFixo = paciente.TelefoneFixo,
                    Ativo = paciente.Ativo,
                    ConvenioNome = pacienteDto.ConvenioNome,
                    NumeroCarteirinha = paciente.NumeroCarteirinha,
                    ValidadeCarteirinha = paciente.ValidadeCarteirinha
                };
                return CreatedAtAction(nameof(GetById), new { id = paciente.Id }, dto);
            }
            catch (Exception ex)
            {
                // Considerar logar a exceção completa para depuração
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PacienteDTO pacienteDto)
        {
            if (pacienteDto == null)
                return BadRequest("O corpo da requisição está vazio ou mal formatado. Envie um JSON válido.");

            if (id != pacienteDto.Id)
                return BadRequest("O ID na URL não coincide com o ID no corpo da requisição.");

            // Validação dos campos obrigatórios (complementar ao DTO)
            if (string.IsNullOrWhiteSpace(pacienteDto.Nome))
                return BadRequest("O campo Nome é obrigatório.");
            if (string.IsNullOrWhiteSpace(pacienteDto.Sobrenome))
                return BadRequest("O campo Sobrenome é obrigatório.");
            if (string.IsNullOrWhiteSpace(pacienteDto.CPF))
                return BadRequest("O campo CPF é obrigatório.");
            if (string.IsNullOrWhiteSpace(pacienteDto.Email))
                return BadRequest("O campo Email é obrigatório.");
            if (string.IsNullOrWhiteSpace(pacienteDto.DataNascimento))
                return BadRequest("O campo Data de Nascimento é obrigatório.");
            else if (!DateTime.TryParse(pacienteDto.DataNascimento, out _))
                 return BadRequest("O campo DataNascimento está em formato inválido (use yyyy-MM-dd).");

            // Remover validação de Celular obrigatório
            // if (string.IsNullOrWhiteSpace(pacienteDto.Celular))
            //     return BadRequest("O campo Celular é obrigatório.");

            try
            {
                var paciente = await _service.BuscarPorIdAsync(id);
                if (paciente == null)
                    return NotFound($"Paciente com ID {id} não encontrado."); // Mensagem mais clara

                // Atualizar propriedades básicas
                paciente.Nome = pacienteDto.Nome ?? string.Empty;
                paciente.Sobrenome = pacienteDto.Sobrenome ?? string.Empty;
                // Parse da data já validada
                paciente.DataNascimento = DateTime.Parse(pacienteDto.DataNascimento);
                paciente.Genero = pacienteDto.Genero;
                paciente.CPF = pacienteDto.CPF;
                paciente.RG = pacienteDto.RG;
                paciente.UfRg = pacienteDto.UfRg;
                paciente.Email = pacienteDto.Email ?? string.Empty;
                paciente.Celular = pacienteDto.Celular;
                paciente.TelefoneFixo = pacienteDto.TelefoneFixo;
                paciente.Ativo = pacienteDto.Ativo;
                paciente.NumeroCarteirinha = pacienteDto.NumeroCarteirinha;
                paciente.ValidadeCarteirinha = pacienteDto.ValidadeCarteirinha;

                // Atualizar o convênio pelo nome
                if (!string.IsNullOrEmpty(pacienteDto.ConvenioNome))
                {
                    var convenio = await _context.Convenios.FirstOrDefaultAsync(c => c.Nome == pacienteDto.ConvenioNome);
                    if (convenio == null)
                        return BadRequest($"Convênio 	'{pacienteDto.ConvenioNome}	' não encontrado.");
                    paciente.ConvenioId = convenio.Id;
                }
                else
                {
                    paciente.ConvenioId = null; // Permite remover o convênio
                }

                await _service.EditarPacienteAsync(paciente);
                return NoContent(); // Retorno padrão para PUT bem-sucedido sem conteúdo
            }
            catch (DbUpdateConcurrencyException) // Capturar exceção específica para concorrência
            {
                // Logar a exceção
                return StatusCode(StatusCodes.Status409Conflict, new { mensagem = "Conflito de concorrência ao atualizar o paciente." });
            }
            catch (Exception ex) // Capturar outras exceções
            {
                // Logar a exceção completa para depuração
                return BadRequest(new { mensagem = $"Erro ao atualizar paciente: {ex.Message}" });
            }
        }
    }
}

