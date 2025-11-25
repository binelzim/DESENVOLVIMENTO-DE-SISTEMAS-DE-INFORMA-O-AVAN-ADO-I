using Microsoft.EntityFrameworkCore;
using Universidade.Domain.Entities;
using Universidade.Domain.Interfaces;
using Universidade.Infrastructure.Data;

namespace Universidade.Infrastructure.Repositories
{
    public class DisciplinaRepository : Repository<Disciplina>, IDisciplinaRepository
    {
        public DisciplinaRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Disciplina>> ObterTodasComCurso()
        {
            return await _context.Disciplinas
                                 .Include(d => d.Curso)
                                 .ToListAsync();
        }

        // Implementação da pesquisa
        public async Task<IEnumerable<Disciplina>> Pesquisar(string termo)
        {
            return await _context.Disciplinas
                                 .Include(d => d.Curso)
                                 .Where(d => d.Nome.Contains(termo)) // Filtra pelo nome
                                 .ToListAsync();
        }
    }
}