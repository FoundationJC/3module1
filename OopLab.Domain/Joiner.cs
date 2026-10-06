using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopLab.Domain
{
    public class Joiner : Person, ISkill
    {
        public string Tool { get; }
        public int ProductsMade { get; private set; }

        public Joiner(string firstName, string lastName, string tool)
            : base(firstName, lastName)
        {
            Tool = tool;
        }

        public void MakeProduct() => ProductsMade++; // зміна стану

        public string MultiplyBigNumbers(string a, string b) => BigMath.Multiply(a, b);

        public override string GetDescription()
            => $"Joiner {FirstName} {LastName}, інструмент: {Tool}, виробів: {ProductsMade}";
    }
}
