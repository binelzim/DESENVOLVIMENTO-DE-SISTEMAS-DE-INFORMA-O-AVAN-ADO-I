using System.ComponentModel.DataAnnotations;

namespace Universidade.Domain.Entities
{
    public class Curso
    {
        [Key] // Isso informa ao Entity Framework que 'Id' é a chave primária.
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do curso é obrigatório.")] // Garante que o nome não pode ser nulo ou vazio. A mensagem de erro será usada na tela de cadastro.
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")] // Define um tamanho máximo e mínimo.
        public required string Nome { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public required string Descricao { get; set; }

        [Display(Name = "Carga Horária")] // Muda como o nome do campo aparece na tela (ex: em um formulário).
        [Range(1, 1000, ErrorMessage = "A carga horária deve ser um valor válido entre 1 e 1000.")] // Garante que o valor esteja dentro de um intervalo.
        public int CargaHoraria { get; set; }
    }
}