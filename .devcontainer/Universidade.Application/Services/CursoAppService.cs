using Mapster; // <-- Importante: Namespace do Mapster
using Universidade.Application.Interfaces;
using Universidade.Application.ViewModels;
using Universidade.Domain.Entities;
using Universidade.Domain.Interfaces;

namespace Universidade.Application.Services
{
    public class CursoAppService : ICursoAppService
    {
        private readonly ICursoRepository _cursoRepository;

        public CursoAppService(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        public async Task Adicionar(CursoViewModel cursoViewModel)
        {
            // O Mapster faz a conversão automática baseada nos nomes das propriedades
            var curso = cursoViewModel.Adapt<Curso>();
            
            await _cursoRepository.AddAsync(curso);
            await _cursoRepository.SaveChangesAsync();
        }

        public async Task Atualizar(CursoViewModel cursoViewModel)
        {
            // Mapeia os dados do ViewModel para a Entidade
            var curso = cursoViewModel.Adapt<Curso>();

            // O EF Core precisa saber que este objeto existe. 
            // Como estamos passando um objeto novo com o ID preenchido, o Update funciona.
            _cursoRepository.Update(curso);
            await _cursoRepository.SaveChangesAsync();
        }

        public async Task<CursoViewModel> ObterPorId(int id)
        {
            var curso = await _cursoRepository.GetByIdAsync(id);
            // Converte de Entidade -> ViewModel
            return curso.Adapt<CursoViewModel>();
        }

        public async Task<IEnumerable<CursoViewModel>> ObterTodos()
        {
            var cursos = await _cursoRepository.GetAllAsync();
            // Converte Lista de Entidades -> Lista de ViewModels
            return cursos.Adapt<IEnumerable<CursoViewModel>>();
        }

        public async Task Remover(int id)
        {
            var curso = await _cursoRepository.GetByIdAsync(id);
            if (curso != null)
            {
                _cursoRepository.Delete(curso);
                await _cursoRepository.SaveChangesAsync();
            }
        }
    }
}