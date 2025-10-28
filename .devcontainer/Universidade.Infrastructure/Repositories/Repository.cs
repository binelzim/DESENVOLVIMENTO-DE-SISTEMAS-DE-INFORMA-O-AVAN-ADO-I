using Microsoft.EntityFrameworkCore;
using Universidade.Domain.Interfaces; // <-- A interface que estamos implementando
using Universidade.Infrastructure.Data; // <-- O DbContext que vamos usar

namespace Universidade.Infrastructure.Repositories
{
    // Esta classe implementa o IRepository<T> genérico
    // 'abstract' significa que ela não pode ser usada diretamente, apenas herdada.
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        // O 'protected' permite que as classes filhas (como CursoRepository)
        // também acessem o _context, se necessário.
        protected readonly AppDbContext _context;

        // Injetamos o DbContext para que possamos interagir com o banco
        public Repository(AppDbContext context)
        {
            _context = context;
        }

        // A implementação de fato dos métodos do contrato
        // Usamos o _context.Set<T>() para acessar a tabela genérica (ex: Cursos, Alunos)

        public async Task AddAsync(T entity)
        {
            // 'AddAsync' apenas começa a "rastrear" a entidade.
            await _context.Set<T>().AddAsync(entity);
        }

        public void Delete(T entity)
        {
            // 'Remove' apenas marca a entidade como "deletada".
            _context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            // 'ToListAsync' executa a consulta no banco.
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            // 'FindAsync' busca pela Chave Primária.
            return await _context.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            // 'Update' apenas marca a entidade como "modificada".
            _context.Set<T>().Update(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            // É O 'SAVECHANGES' QUE REALMENTE EXECUTA O SQL
            // (INSERT, UPDATE, DELETE) no banco de dados.
            return await _context.SaveChangesAsync();
        }
    }
}