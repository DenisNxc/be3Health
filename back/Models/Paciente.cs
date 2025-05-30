using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientRegistration.API.Models
{
    public class Paciente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        [Required]
        public string Sobrenome { get; set; } = string.Empty;

        // DataNascimento não obrigatório
        public DateTime? DataNascimento { get; set; }

        // Genero não obrigatório
        public string? Genero { get; set; }

        // CPF ou RG obrigatório (validação feita no controller)
        public string? CPF { get; set; }
        public string? RG { get; set; }

        // UfRg não obrigatório
        public string? UfRg { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        // Celular ou TelefoneFixo obrigatório (validação feita no controller)
        public string? Celular { get; set; }
        public string? TelefoneFixo { get; set; }

        public bool Ativo { get; set; } = true;

        // Relação com Convênio (não obrigatório)
        public int? ConvenioId { get; set; }

        [ForeignKey("ConvenioId")]
        public Convenio? Convenio { get; set; }

        public string? NumeroCarteirinha { get; set; }
        public string? ValidadeCarteirinha { get; set; } // mm/yyyy como texto
    }
}
