using System.ComponentModel.DataAnnotations;

namespace Universidade.Domain.Entities
{
    public class Curso
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do curso é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public required string Descricao { get; set; }

        [Display(Name = "Carga Horária")]
        [Range(1, 1000, ErrorMessage = "A carga horária deve ser um valor válido entre 1 e 1000.")]
        public int CargaHoraria { get; set; }

        // Relacionamento 1:N - Um curso tem várias disciplinas
        // O 'virtual' é importante para o Entity Framework trabalhar corretamente
        public virtual ICollection<Disciplina> Disciplinas { get; set; } = new List<Disciplina>();
    }
}