using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopLab.Domain
{
    // Довге множення чисел, що не вміщаються у long — через рядки.
    public static class BigMath
    {
        public static string Multiply(string a, string b)
        {
            if (string.IsNullOrWhiteSpace(a) || string.IsNullOrWhiteSpace(b)

        || !a.All(char.IsDigit) || !b.All(char.IsDigit))
                throw new FormatException("Числа мають містити лише цифри.");



            int[] result = new int[a.Length + b.Length];
            for (int i = a.Length - 1; i >= 0; i--)
                for (int j = b.Length - 1; j >= 0; j--)
                    result[i + j + 1] += (a[i] - '0') * (b[j] - '0');

            for (int k = result.Length - 1; k > 0; k--)
            {
                result[k - 1] += result[k] / 10;
                result[k] %= 10;
            }

            var sb = new StringBuilder();
            bool leading = true;
            foreach (int digit in result)
            {
                if (leading && digit == 0) continue;
                leading = false;
                sb.Append(digit);
            }
            return sb.Length == 0 ? "0" : sb.ToString();



        }
    }
}
