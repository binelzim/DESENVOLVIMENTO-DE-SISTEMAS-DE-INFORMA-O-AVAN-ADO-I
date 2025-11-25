using Universidade.Application.ViewModels;

namespace Universidade.Application.Interfaces
{
    public interface IDisciplinaAppService
    {
        Task Adicionar(DisciplinaViewModel disciplinaViewModel);
        Task Atualizar(DisciplinaViewModel disciplinaViewModel);
        Task Remover(int id);
        Task<DisciplinaViewModel> ObterPorId(int id);
        Task<IEnumerable<DisciplinaViewModel>> ObterTodos();

        // Novo método
        Task<IEnumerable<DisciplinaViewModel>> Pesquisar(string termo);
    }
}