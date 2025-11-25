using System.ComponentModel.DataAnnotations;
using Universidade.Application.Validations; // <-- Importante!

namespace Universidade.Application.ViewModels
{
    public class DisciplinaViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nome da Disciplina")]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
        [PrimeiraLetraMaiuscula] // <-- Validação Personalizada 1
        public string Nome { get; set; }

        [Display(Name = "Carga Horária")]
        [Range(1, 200, ErrorMessage = "A carga horária deve ser entre 1 e 200 horas.")]
        [MultiploDe(5)] // <-- Validação Personalizada 2 (Deve ser múltiplo de 5)
        public int CargaHoraria { get; set; }

        [Display(Name = "Curso")]
        [Required(ErrorMessage = "Selecione um curso.")]
        public int CursoId { get; set; }

        [Display(Name = "Curso")]
        public string? CursoNome { get; set; }
    }
}