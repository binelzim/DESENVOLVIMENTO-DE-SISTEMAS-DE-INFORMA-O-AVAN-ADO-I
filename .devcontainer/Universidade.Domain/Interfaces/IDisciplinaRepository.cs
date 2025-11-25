using Universidade.Domain.Entities;

namespace Universidade.Domain.Interfaces
{
    public interface IDisciplinaRepository : IRepository<Disciplina>
    {
        Task<IEnumerable<Disciplina>> ObterTodasComCurso();
        // Novo método de pesquisa
        Task<IEnumerable<Disciplina>> Pesquisar(string termo);
    }
}