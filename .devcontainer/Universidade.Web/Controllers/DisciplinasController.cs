using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Universidade.Application.Interfaces;
using Universidade.Application.ViewModels;

namespace Universidade.Web.Controllers
{
    public class DisciplinasController : Controller
    {
        private readonly IDisciplinaAppService _disciplinaAppService;
        private readonly ICursoAppService _cursoAppService; // Precisamos disso para preencher o Dropdown

        public DisciplinasController(IDisciplinaAppService disciplinaAppService, ICursoAppService cursoAppService)
        {
            _disciplinaAppService = disciplinaAppService;
            _cursoAppService = cursoAppService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _disciplinaAppService.ObterTodos());
        }

        public async Task<IActionResult> Create()
        {
            // Carrega a lista de cursos para o Dropdown (Select)
            await PreencherViewBagCursos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DisciplinaViewModel disciplinaViewModel)
        {
            if (ModelState.IsValid)
            {
                await _disciplinaAppService.Adicionar(disciplinaViewModel);
                return RedirectToAction(nameof(Index));
            }
            // Se der erro, recarrega o dropdown
            await PreencherViewBagCursos();
            return View(disciplinaViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var disciplina = await _disciplinaAppService.ObterPorId(id);
            if (disciplina == null) return NotFound();

            await PreencherViewBagCursos();
            return View(disciplina);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DisciplinaViewModel disciplinaViewModel)
        {
            if (id != disciplinaViewModel.Id) return NotFound();

            if (ModelState.IsValid)
            {
                await _disciplinaAppService.Atualizar(disciplinaViewModel);
                return RedirectToAction(nameof(Index));
            }
            await PreencherViewBagCursos();
            return View(disciplinaViewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var disciplina = await _disciplinaAppService.ObterPorId(id);
            if (disciplina == null) return NotFound();

            return View(disciplina);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _disciplinaAppService.Remover(id);
            return RedirectToAction(nameof(Index));
        }

        // Método auxiliar para preencher o Dropdown
        private async Task PreencherViewBagCursos()
        {
            var cursos = await _cursoAppService.ObterTodos();
            ViewData["CursoId"] = new SelectList(cursos, "Id", "Nome");
        }

        // GET: Disciplinas/Filtrar?termo=Matematica
        public async Task<IActionResult> Filtrar(string termo)
        {
            // Se o termo for vazio, traz tudo. Se tiver texto, pesquisa.
            var resultado = string.IsNullOrEmpty(termo) 
                ? await _disciplinaAppService.ObterTodos()
                : await _disciplinaAppService.Pesquisar(termo);

            // Retorna apenas o HTML das linhas da tabela (_TabelaDisciplinas.cshtml)
            return PartialView("_TabelaDisciplinas", resultado);
        }
    }
}