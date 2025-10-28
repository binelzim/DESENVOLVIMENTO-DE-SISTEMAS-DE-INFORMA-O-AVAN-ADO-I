using System.ComponentModel.DataAnnotations;

namespace Universidade.Application.ViewModels
{
    // Esta classe representa os dados que a View (tela)
    // vai usar para exibir e criar um Curso.
    public class CursoViewModel
    {
        // A View não precisa saber que isso é uma [Key]
        public int Id { get; set; }

        // Usamos [Display] para formatar o nome do campo na tela.
        [Display(Name = "Nome do Curso")]
        [Required(ErrorMessage = "O nome do curso é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Descricao { get; set; }

        [Display(Name = "Carga Horária (em horas)")]
        [Required(ErrorMessage = "A carga horária é obrigatória.")]
        [Range(1, 1000, ErrorMessage = "A carga horária deve ser um valor válido entre 1 e 1000.")]
        public int CargaHoraria { get; set; }
    }
}