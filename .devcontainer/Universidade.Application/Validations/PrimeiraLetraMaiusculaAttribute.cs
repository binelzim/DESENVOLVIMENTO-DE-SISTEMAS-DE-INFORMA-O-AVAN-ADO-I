using System.ComponentModel.DataAnnotations;

namespace Universidade.Application.Validations
{
    public class PrimeiraLetraMaiusculaAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                // Se for nulo, deixamos passar (o [Required] que cuida disso)
                return ValidationResult.Success;
            }

            var texto = value.ToString();
            if (texto!.Length > 0 && char.IsUpper(texto[0]))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("A primeira letra deve ser maiúscula.");
        }
    }
}