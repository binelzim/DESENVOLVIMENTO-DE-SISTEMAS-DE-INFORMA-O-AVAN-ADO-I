using Mapster;
using Universidade.Application.Interfaces;
using Universidade.Application.ViewModels;
using Universidade.Domain.Entities;
using Universidade.Domain.Interfaces;

namespace Universidade.Application.Services
{
    public class DisciplinaAppService : IDisciplinaAppService
    {
        private readonly IDisciplinaRepository _disciplinaRepository;

        public DisciplinaAppService(IDisciplinaRepository disciplinaRepository)
        {
            _disciplinaRepository = disciplinaRepository;
        }

        public async Task Adicionar(DisciplinaViewModel disciplinaViewModel)
        {
            var disciplina = disciplinaViewModel.Adapt<Disciplina>();
            await _disciplinaRepository.AddAsync(disciplina);
            await _disciplinaRepository.SaveChangesAsync();
        }

        public async Task Atualizar(DisciplinaViewModel disciplinaViewModel)
        {
            var disciplina = disciplinaViewModel.Adapt<Disciplina>();
            _disciplinaRepository.Update(disciplina);
            await _disciplinaRepository.SaveChangesAsync();
        }

        public async Task<DisciplinaViewModel> ObterPorId(int id)
        {
            var disciplina = await _disciplinaRepository.GetByIdAsync(id);
            return disciplina.Adapt<DisciplinaViewModel>();
        }

        public async Task<IEnumerable<DisciplinaViewModel>> ObterTodos()
        {
            // Usamos o método específico que carrega o Curso junto
            var disciplinas = await _disciplinaRepository.ObterTodasComCurso();
            
            // O Mapster é inteligente: ele mapeia automaticamente 
            // disciplina.Curso.Nome (Entidade) -> disciplinaViewModel.CursoNome (ViewModel)
            return disciplinas.Adapt<IEnumerable<DisciplinaViewModel>>();
        }

        public async Task Remover(int id)
        {
            var disciplina = await _disciplinaRepository.GetByIdAsync(id);
            if (disciplina != null)
            {
                _disciplinaRepository.Delete(disciplina);
                await _disciplinaRepository.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<DisciplinaViewModel>> Pesquisar(string termo)
        {
            var disciplinas = await _disciplinaRepository.Pesquisar(termo);
            return disciplinas.Adapt<IEnumerable<DisciplinaViewModel>>();
        }
    }
}