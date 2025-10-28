using Universidade.Application.Interfaces;
using Universidade.Application.ViewModels;
using Universidade.Domain.Entities; // <-- Precisa da Entidade do Domínio
using Universidade.Domain.Interfaces; // <-- Precisa da Interface do Domínio

namespace Universidade.Application.Services
{
    public class CursoAppService : ICursoAppService
    {
        // 1. Dependência da Interface do Domínio (Repositório)
        // Esta classe NÃO sabe como o banco de dados funciona.
        // Ela apenas conhece o contrato ICursoRepository.
        private readonly ICursoRepository _cursoRepository;

        // 2. Injeção de Dependência (DI)
        // O sistema (no Program.cs) será responsável por "injetar"
        // uma implementação concreta de ICursoRepository aqui.
        public CursoAppService(ICursoRepository cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        // --- Implementação dos Métodos ---

        public async Task Adicionar(CursoViewModel cursoViewModel)
        {
            // 3. Mapeamento (ViewModel -> Entidade)
            // Converte os dados da tela para uma entidade de negócio.
            // (Em projetos reais, usamos uma ferramenta como o AutoMapper para isso)
            var curso = new Curso
            {
                Nome = cursoViewModel.Nome,
                Descricao = cursoViewModel.Descricao,
                CargaHoraria = cursoViewModel.CargaHoraria
            };

            // 4. Chama o Repositório do Domínio
            await _cursoRepository.AddAsync(curso);
            await _cursoRepository.SaveChangesAsync(); // Confirma a transação
        }

        public async Task Atualizar(CursoViewModel cursoViewModel)
        {
            // Busca a entidade original no banco
            var curso = await _cursoRepository.GetByIdAsync(cursoViewModel.Id);

            if (curso != null)
            {
                // Mapeia as mudanças do ViewModel para a Entidade
                curso.Nome = cursoViewModel.Nome;
                curso.Descricao = cursoViewModel.Descricao;
                curso.CargaHoraria = cursoViewModel.CargaHoraria;

                // 4. Chama o Repositório do Domínio
                _cursoRepository.Update(curso);
                await _cursoRepository.SaveChangesAsync(); // Confirma a transação
            }
        }

        public async Task<CursoViewModel> ObterPorId(int id)
        {
            var curso = await _cursoRepository.GetByIdAsync(id);
            if (curso == null) return null;

            // 3. Mapeamento (Entidade -> ViewModel)
            // Converte a entidade do banco para um ViewModel que a tela entende.
            return new CursoViewModel
            {
                Id = curso.Id,
                Nome = curso.Nome,
                Descricao = curso.Descricao,
                CargaHoraria = curso.CargaHoraria
            };
        }

        public async Task<IEnumerable<CursoViewModel>> ObterTodos()
        {
            var cursos = await _cursoRepository.GetAllAsync();

            // 3. Mapeamento (Lista de Entidades -> Lista de ViewModels)
            return cursos.Select(curso => new CursoViewModel
            {
                Id = curso.Id,
                Nome = curso.Nome,
                Descricao = curso.Descricao,
                CargaHoraria = curso.CargaHoraria
            });
        }

        public async Task Remover(int id)
        {
            var curso = await _cursoRepository.GetByIdAsync(id);
            if (curso != null)
            {
                // 4. Chama o Repositório do Domínio
                _cursoRepository.Delete(curso);
                await _cursoRepository.SaveChangesAsync(); // Confirma a transação
            }
        }
    }
}