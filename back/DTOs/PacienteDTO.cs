using System.ComponentModel.DataAnnotations;

namespace PatientRegistration.API.DTOs
{
    public class PacienteDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo Sobrenome é obrigatório.")]
        public string? Sobrenome { get; set; }

        [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório.")]
        // Manter como string para validação flexível no DTO, converter no mapeamento
        public string? DataNascimento { get; set; }

        public string? Genero { get; set; }

        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        public string? CPF { get; set; }

        public string? RG { get; set; }
        public string? UfRg { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo Email não é um endereço de email válido.")]
        public string? Email { get; set; }

        public string? Celular { get; set; }
        public string? TelefoneFixo { get; set; }
        public bool Ativo { get; set; } = true; // Default to true for new entries if not provided
        public string? ConvenioNome { get; set; }
        public string? NumeroCarteirinha { get; set; }
        public string? ValidadeCarteirinha { get; set; }
    }
}

