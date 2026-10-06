using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopLab.Domain
{
    public class Student : Person, ISkill
    {
        public int Course { get; private set; }
        public string StudentId { get; }      // обов'язковий
        public string Gender { get; }        // обов'язковий
        public string Residence { get; set; } // місце проживання
        public string RecordBook { get; }    // залікова книжка, обов'язкова

        public Student(string firstName, string lastName, int course,
            string studentId, string gender, string residence, string recordBook)
            : base(firstName, lastName)
        {
            Course = course;
            StudentId = studentId;
            Gender = gender;
            Residence = residence;
            RecordBook = recordBook;
        }

        // Змінює стан об'єкта (переведення на наступний курс), нічого не друкує.
        public void Study() => Course++;

        public bool IsFifthCourseKyivFemale()
            => Course == 5 && Gender == "жінка" && Residence == "Київ";

        public string MultiplyBigNumbers(string a, string b) => BigMath.Multiply(a, b);

        public override string GetDescription()
            => $"Student {FirstName} {LastName}, курс {Course}, квиток {StudentId}, залікова {RecordBook}, проживає: {Residence}";
    }
}
