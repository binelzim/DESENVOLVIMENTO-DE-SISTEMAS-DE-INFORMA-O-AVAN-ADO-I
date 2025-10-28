using Universidade.Domain.Entities; // <-- A entidade Curso
using Universidade.Domain.Interfaces; // <-- A interface ICursoRepository
using Universidade.Infrastructure.Data; // <-- O DbContext

namespace Universidade.Infrastructure.Repositories
{
    // Esta é a classe que vamos injetar na Fase 5.
    // 1. Ela herda toda a lógica pronta do Repository<Curso>
    // 2. Ela cumpre o contrato do ICursoRepository
    public class CursoRepository : Repository<Curso>, ICursoRepository
    {
        // O construtor apenas recebe o DbContext...
        public CursoRepository(AppDbContext context) : base(context)
        {
            // ...e o repassa para o construtor da classe base (Repository<T>)
            // É o 'base(context)' que faz a mágica.
        }

        // Como não temos métodos extras no ICursoRepository,
        // o corpo da classe fica vazio! Toda a lógica
        // já foi implementada na classe 'Repository<Curso>'.
    }
}