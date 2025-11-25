using Microsoft.EntityFrameworkCore;
using Universidade.Domain.Entities;

namespace Universidade.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Curso> Cursos { get; set; }
        // Adicionamos a tabela de Disciplinas
        public DbSet<Disciplina> Disciplinas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração Explícita do Relacionamento 1:N
            modelBuilder.Entity<Disciplina>()
                .HasOne(d => d.Curso)           // Uma Disciplina tem um Curso
                .WithMany(c => c.Disciplinas)   // Um Curso tem muitas Disciplinas
                .HasForeignKey(d => d.CursoId)  // A chave estrangeira é CursoId
                .OnDelete(DeleteBehavior.Cascade); // Se deletar o Curso, deleta as Disciplinas dele
        }
    }
}