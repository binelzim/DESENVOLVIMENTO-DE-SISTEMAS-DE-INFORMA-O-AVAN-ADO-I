using System.ComponentModel.DataAnnotations;

namespace Universidade.Application.Validations
{
    public class MultiploDeAttribute : ValidationAttribute
    {
        private readonly int _numero;

        public MultiploDeAttribute(int numero)
        {
            _numero = numero;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            if (int.TryParse(value.ToString(), out int valorInteiro))
            {
                if (valorInteiro % _numero == 0)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult($"O valor deve ser um múltiplo de {_numero}.");
        }
    }
}