using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopLab.Domain
{
    // Спільна поведінка: її може виконувати і студент, і тесляр, і фотограф.
    public interface ISkill
    {
        string MultiplyBigNumbers(string a, string b);
    }
}
