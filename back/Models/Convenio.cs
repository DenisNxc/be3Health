using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PatientRegistration.API.Models
{
    public class Convenio
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; } = string.Empty;

        public ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();
    }
}
