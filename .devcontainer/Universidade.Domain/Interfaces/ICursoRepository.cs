using Universidade.Domain.Entities; // Precisamos disso para saber o que é a classe 'Curso'.

namespace Universidade.Domain.Interfaces
{
    // Esta interface herda todas as promessas de IRepository<T>,
    // mas já especifica que o 'T' será 'Curso'.
    public interface ICursoRepository : IRepository<Curso>
    {
        // Este espaço é para métodos que SÓ fazem sentido para Cursos.
        // Por exemplo:
        // Task<IEnumerable<Curso>> ObterCursosPorAreaDeConhecimento(string area);
        //
        // Para o seu CRUD básico, não precisamos de nenhum método extra aqui.
        // A interface pode ficar vazia, pois ela já herdou tudo que precisa.
    }
}