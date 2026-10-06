using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopLab.Domain
{
    public class Photographer : Person, ISkill
    {
        public string CameraModel { get; }
        public int PhotosTaken { get; private set; }

        public Photographer(string firstName, string lastName, string cameraModel)
            : base(firstName, lastName)
        {
            CameraModel = cameraModel;
        }

        public void TakePhoto() => PhotosTaken++; // зміна стану

        public string MultiplyBigNumbers(string a, string b) => BigMath.Multiply(a, b);

        public override string GetDescription()
            => $"Photographer {FirstName} {LastName}, камера: {CameraModel}, фото: {PhotosTaken}";
    }
}
