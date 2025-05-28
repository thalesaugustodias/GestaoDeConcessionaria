using FluentValidation;

namespace GestaoDeConcessionaria.Application.Extensions
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> Cpf<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(cpf => IsValidCpf(cpf)).WithMessage("CPF inválido.");
        }

        private static bool IsValidCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf)) return false;

            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11 || !long.TryParse(cpf, out _)) return false;

            var digits = cpf.Select(c => int.Parse(c.ToString())).ToArray();
            var firstCheck = CalculateCheckDigit(digits.Take(9).ToArray());
            var secondCheck = CalculateCheckDigit(digits.Take(10).ToArray());

            return digits[9] == firstCheck && digits[10] == secondCheck;
        }

        private static int CalculateCheckDigit(int[] digits)
        {
            var sum = 0;
            for (int i = 0; i < digits.Length; i++)
            {
                sum += digits[i] * (digits.Length + 1 - i);
            }
            var remainder = sum % 11;
            return remainder < 2 ? 0 : 11 - remainder;
        }
    }
}
