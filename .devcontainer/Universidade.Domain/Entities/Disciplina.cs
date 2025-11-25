using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Universidade.Domain.Entities
{
    public class Disciplina
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da disciplina é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
        public required string Nome { get; set; }

        [Range(1, 200, ErrorMessage = "A carga horária deve ser entre 1 e 200 horas.")]
        public int CargaHoraria { get; set; }

        // Chave Estrangeira (Foreign Key) para Curso
        // O Required garante que toda disciplina pertença a um curso
        [Required(ErrorMessage = "É obrigatório selecionar um Curso.")]
        public int CursoId { get; set; }

        // Propriedade de Navegação
        // Permite acessar os dados do Curso a partir da Disciplina (ex: disciplina.Curso.Nome)
        [ForeignKey("CursoId")]
        public virtual Curso? Curso { get; set; }
    }
}