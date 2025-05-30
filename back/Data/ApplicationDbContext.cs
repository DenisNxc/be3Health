using Microsoft.EntityFrameworkCore;
using PatientRegistration.API.Models;

namespace PatientRegistration.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Convenio> Convenios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Paciente>()
                .HasIndex(p => p.CPF)
                .IsUnique()
                .HasFilter("[CPF] IS NOT NULL");

            modelBuilder.Entity<Convenio>().HasData(
                new Convenio { Id = 1, Nome = "Convênio A" },
                new Convenio { Id = 2, Nome = "Convênio B" },
                new Convenio { Id = 3, Nome = "Convênio C" },
                new Convenio { Id = 4, Nome = "Convênio D" },
                new Convenio { Id = 5, Nome = "Convênio E" }
            );
        }
    }
}
