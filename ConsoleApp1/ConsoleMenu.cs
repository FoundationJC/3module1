using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OopLab.Domain;
using OopLab.FileStorage;
using System.Numerics;

namespace OopLab.ConsoleApp
{
    // Увесь консольний ввод-вивід — тільки тут, поза сутностями та файловими класами.
    public class ConsoleMenu
    {
        private Person[] _persons = new Person[0];
        private readonly PersonFileStorage _storage = new();
        private const string FilePath = "persons.txt";

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\n=== МЕНЮ ===");
                Console.WriteLine("1 - Додати особу");
                Console.WriteLine("2 - Показати всіх");
                Console.WriteLine("3 - Пошук за прізвищем");
                Console.WriteLine("4 - Пошук за студентським квитком");
                Console.WriteLine("5 - Студентки 5 курсу, що проживають у Києві");
                Console.WriteLine("6 - Зберегти у файл");
                Console.WriteLine("7 - Завантажити з файлу");
                Console.WriteLine("8 - Видалити особу");
                Console.WriteLine("9 - Множення великих чисел");
                Console.WriteLine("0 - Вихід");
                Console.Write("Ваш вибір: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddPerson(); break;
                    case "2": ShowAll(); break;
                    case "3": SearchByLastName(); break;
                    case "4": SearchByStudentId(); break;
                    case "5": CountKyivFifthCourseGirls(); break;
                    case "6": _storage.SaveAll(FilePath, _persons); Console.WriteLine("Збережено."); break;
                    case "7": _persons = _storage.LoadAll(FilePath); Console.WriteLine($"Завантажено {_persons.Length} осіб."); break;
                    case "8": RemovePerson(); break;
                    case "9": MultiplyDemo(); break;
                    case "0": return;
                    default: Console.WriteLine("Невірний пункт."); break;
                }
            }
        }

        private void AddPerson()
        {
            Console.Write("Тип (Student/Joiner/Photographer): ");
            string? type = Console.ReadLine();
            Console.Write("Ім'я: ");
            string fn = Console.ReadLine() ?? "";
            Console.Write("Прізвище: ");
            string ln = Console.ReadLine() ?? "";
            if (!Validators.IsValidNameOrPlace(fn) || !Validators.IsValidNameOrPlace(ln))
            {
                Console.WriteLine("Помилка: ім'я та прізвище мають містити лише літери.");
                return;
            }

            Person p = type switch
            {
                "Student" => ReadStudent(fn, ln),
                "Joiner" => new Joiner(fn, ln, ReadValidated("Інструмент: ", Validators.IsValidNameOrPlace, "Лише літери.")),
                "Photographer" => new Photographer(fn, ln, ReadValidated("Модель камери: ", s => !string.IsNullOrWhiteSpace(s), "Не може бути порожнім.")),
                _ => throw new ArgumentException("Невідомий тип")
            };

            Array.Resize(ref _persons, _persons.Length + 1);
            _persons[_persons.Length - 1] = p;
            Console.WriteLine("Додано.");
        }

        private Student ReadStudent(string fn, string ln)
        {
            int course = int.Parse(ReadValidated("Курс (1-6): ",
                s => int.TryParse(s, out int c) && Validators.IsValidCourse(c), "Курс 1-6."));
            string studentId = ReadValidated("Студентський квиток (напр. KB123456): ",
                Validators.IsValidStudentId, "Формат: 2 літери + 6 цифр.");
            string gender = ReadValidated("Стать (чоловік/жінка): ",
                Validators.IsValidGender, "Лише чоловік або жінка.");
            string residence = ReadValidated("Місце проживання: ",
                Validators.IsValidNameOrPlace, "Лише літери.");
            string recordBook = ReadValidated("Залікова книжка (напр. ЗК123456): ",
                Validators.IsValidRecordBook, "Формат: ЗК + 6 цифр.");
            return new Student(fn, ln, course, studentId, gender, residence, recordBook);
        }

        private string ReadValidated(string prompt, Func<string, bool> check, string error)
        {
            while (true)
            {
                Console.Write(prompt);
                string? s = Console.ReadLine() ?? "";
                if (check(s)) return s;
                Console.WriteLine("Помилка: " + error);
            }
        }

        private void ShowAll()
        {
            if (_persons.Length == 0) { Console.WriteLine("База порожня."); return; }
            foreach (Person p in _persons) Console.WriteLine(p.GetDescription());
        }

        private void SearchByLastName()
        {
            Console.Write("Прізвище: ");
            string? ln = Console.ReadLine();
            bool found = false;
            foreach (Person p in _persons)
                if (p.LastName.Equals(ln, StringComparison.OrdinalIgnoreCase))
                { Console.WriteLine(p.GetDescription()); found = true; }
            if (!found) Console.WriteLine("Не знайдено.");
        }

        private void SearchByStudentId()
        {
            Console.Write("Студентський квиток: ");
            string? id = Console.ReadLine();
            bool found = false;
            foreach (Person p in _persons)
                if (p is Student s && s.StudentId == id)
                { Console.WriteLine(s.GetDescription()); found = true; }
            if (!found) Console.WriteLine("Не знайдено.");
        }

        private void CountKyivFifthCourseGirls()
        {
            int count = 0;
            foreach (Person p in _persons)
                if (p is Student s && s.IsFifthCourseKyivFemale())
                { Console.WriteLine(s.GetDescription()); count++; }
            Console.WriteLine($"Кількість: {count}");
        }

        private void RemovePerson()
        {
            Console.Write("Прізвище особи для видалення: ");
            string? ln = Console.ReadLine();
            int index = Array.FindIndex(_persons, p => p.LastName.Equals(ln, StringComparison.OrdinalIgnoreCase));
            if (index < 0) { Console.WriteLine("Не знайдено."); return; }
            for (int i = index; i < _persons.Length - 1; i++) _persons[i] = _persons[i + 1];
            Array.Resize(ref _persons, _persons.Length - 1);
            Console.WriteLine("Видалено.");
        }

        private void MultiplyDemo()
        {
            Console.Write("Перше число: ");
            string? a = Console.ReadLine();
            Console.Write("Друге число: ");
            string? b = Console.ReadLine();

            if (!BigInteger.TryParse(a, out BigInteger x) || !BigInteger.TryParse(b, out BigInteger y))
            {
                Console.WriteLine("Некоректні числа.");
                return;
            }

            foreach (Person p in _persons)
                if (p is ISkill)
                {
                    try
                    {
                        string product = BigMath.Multiply(a!, b!);
                        Console.WriteLine($"{p.FirstName} {p.LastName}: {a} × {b} = {product}");
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }


                    Console.WriteLine($"{p.FirstName} {p.LastName}: {x} × {y} = {x * y}");
                    return;



                }
            Console.WriteLine("Немає осіб із таким вмінням.");


        }


    }

}
