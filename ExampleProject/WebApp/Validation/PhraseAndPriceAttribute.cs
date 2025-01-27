using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.Validation
{
    public class PhraseAndPriceAttribute : ValidationAttribute
    {
        public string? Phrase { get; set; }
        public string? PriceTarget { get; set; }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null && Phrase != null && PriceTarget != null)
            {
                Product? product = value as Product;

                if (product != null && product.Name.StartsWith(Phrase, StringComparison.OrdinalIgnoreCase)
                    && product.Price > decimal.Parse(PriceTarget))
                {
                    return new ValidationResult(ErrorMessage ?? $"{Phrase} products cannot cost more than ${PriceTarget}");
                }
            }

            return ValidationResult.Success;
        }
    }
}