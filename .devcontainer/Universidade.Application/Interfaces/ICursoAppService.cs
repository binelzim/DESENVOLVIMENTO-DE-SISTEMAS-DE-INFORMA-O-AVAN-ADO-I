using Universidade.Application.ViewModels;

namespace Universidade.Application.Interfaces
{
    // Esta é a interface que o Controller irá injetar e usar.
    // Ela define todas as operações que a View pode realizar.
    public interface ICursoAppService
    {
        // Retorna uma lista de Cursos para a View
        Task<IEnumerable<CursoViewModel>> ObterTodos();

        // Retorna um Curso específico para a View de Edição/Detalhes
        Task<CursoViewModel> ObterPorId(int id);

        // Recebe dados da View para criar um novo Curso
        Task Adicionar(CursoViewModel cursoViewModel);

        // Recebe dados da View para atualizar um Curso existente
        Task Atualizar(CursoViewModel cursoViewModel);

        // Recebe um ID da View para remover um Curso
        Task Remover(int id);
    }
}