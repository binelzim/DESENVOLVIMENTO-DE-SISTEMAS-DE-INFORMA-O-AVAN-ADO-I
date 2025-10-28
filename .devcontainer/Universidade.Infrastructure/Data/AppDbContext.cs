// Importações necessárias
using Microsoft.EntityFrameworkCore;
using Universidade.Domain.Entities; // <-- Precisamos saber sobre a entidade Curso

namespace Universidade.Infrastructure.Data
{
    // Nossa classe AppDbContext herda da classe DbContext do Entity Framework
    public class AppDbContext : DbContext
    {
        // Este construtor é essencial. Ele permite que a Injeção de Dependência
        // (que faremos na Fase 5) passe as configurações de conexão 
        // (como a Connection String) para o DbContext.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Esta linha diz ao EF Core: "Eu quero que você gerencie a entidade 'Curso'".
        // O EF Core irá, por padrão, criar uma tabela chamada 'Cursos' 
        // baseada nas propriedades da classe Curso.
        public DbSet<Curso> Cursos { get; set; }

        // (Opcional) Poderíamos configurar mais coisas aqui, 
        // mas para este projeto, o básico é suficiente.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}