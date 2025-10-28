using Microsoft.AspNetCore.Mvc;
using Universidade.Application.Interfaces; // <-- A interface da Aplicação
using Universidade.Application.ViewModels; // <-- O ViewModel

namespace Universidade.Web.Controllers
{
    public class CursosController : Controller
    {
        // O controller só conhece a ABSTRAÇÃO (Interface)
        private readonly ICursoAppService _cursoAppService;

        // O sistema de Injeção de Dependência (que configuramos no Program.cs)
        // irá injetar a implementação (CursoAppService) aqui.
        public CursosController(ICursoAppService cursoAppService)
        {
            _cursoAppService = cursoAppService;
        }

        // GET: /Cursos ou /Cursos/Index
        public async Task<IActionResult> Index()
        {
            // Busca todos os cursos usando o serviço
            var lista = await _cursoAppService.ObterTodos();
            return View(lista);
        }

        // GET: /Cursos/Create
        public IActionResult Create()
        {
            // Apenas exibe o formulário de criação vazio
            return View();
        }

        // POST: /Cursos/Create
        [HttpPost]
        [ValidateAntiForgeryToken] // Proteção contra ataques CSRF
        public async Task<IActionResult> Create(CursoViewModel cursoViewModel)
        {
            // Verifica se os Data Annotations (ex: [Required]) são válidos
            if (ModelState.IsValid)
            {
                await _cursoAppService.Adicionar(cursoViewModel);
                return RedirectToAction(nameof(Index)); // Volta para a lista
            }
            // Se inválido, retorna para a View com os dados preenchidos
            // e exibe as mensagens de erro.
            return View(cursoViewModel);
        }

        // GET: /Cursos/Edit/5 (ex: id = 5)
        public async Task<IActionResult> Edit(int id)
        {
            var curso = await _cursoAppService.ObterPorId(id);
            if (curso == null)
            {
                return NotFound();
            }
            // Exibe o formulário de edição preenchido
            return View(curso);
        }

        // POST: /Cursos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CursoViewModel cursoViewModel)
        {
            if (id != cursoViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _cursoAppService.Atualizar(cursoViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(cursoViewModel);
        }

        // GET: /Cursos/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var curso = await _cursoAppService.ObterPorId(id);
            if (curso == null)
            {
                return NotFound();
            }
            // Exibe a tela de confirmação de exclusão
            return View(curso);
        }

        // POST: /Cursos/Delete/5
        [HttpPost, ActionName("Delete")] // Confirmação do POST
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _cursoAppService.Remover(id);
            return RedirectToAction(nameof(Index));
        }
    }
}