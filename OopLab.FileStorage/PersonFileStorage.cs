using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using OopLab.Domain;

namespace OopLab.FileStorage
{
    public class PersonFileStorage
    {
        // Запис поелементно у форматі методички.
        public void SaveAll(string path, Person[] persons)
        {
            using var writer = new StreamWriter(path, false);
            foreach (Person p in persons)
            {
                writer.WriteLine(ToRecord(p));
            }
        }

        // Читання поелементно: кожен блок "Тип Назва { ... };" → окремий об'єкт.
        public Person[] LoadAll(string path)
        {
            Person[] persons = new Person[0];
            using var reader = new StreamReader(path);
            string? line;
            string buffer = "";
            while ((line = reader.ReadLine()) != null)
            {
                string trimmed = line.Trim();
                if (trimmed.Length == 0) continue;
                buffer += " " + trimmed;
                if (buffer.Contains("};"))
                {
                    Person? p = ParseBlock(buffer);
                    if (p != null)
                    {
                        Array.Resize(ref persons, persons.Length + 1);
                        persons[persons.Length - 1] = p;
                    }
                    buffer = "";
                }
            }
            return persons;
        }

        private static string ToRecord(Person p)
        {
            return p switch
            {
                Student s => $"Student {s.FirstName}{s.LastName}\r\n{{ \"firstname\": \"{s.FirstName}\", \"lastname\": \"{s.LastName}\", \"course\": \"{s.Course}\", \"studentId\": \"{s.StudentId}\", \"gender\": \"{s.Gender}\", \"residence\": \"{s.Residence}\", \"recordBook\": \"{s.RecordBook}\" }};",
                Joiner j => $"Joiner {j.FirstName}{j.LastName}\r\n{{ \"firstname\": \"{j.FirstName}\", \"lastname\": \"{j.LastName}\", \"tool\": \"{j.Tool}\" }};",
                Photographer ph => $"Photographer {ph.FirstName}{ph.LastName}\r\n{{ \"firstname\": \"{ph.FirstName}\", \"lastname\": \"{ph.LastName}\", \"cameraModel\": \"{ph.CameraModel}\" }};",
                _ => ""
            };
        }

        private static Person? ParseBlock(string block)
        {
            Match header = Regex.Match(block.Trim(), @"^(Student|Joiner|Photographer)\s");


            var attrs = new System.Collections.Generic.Dictionary<string, string>();
            foreach (Match m in Regex.Matches(block, @"""(\w+)"":\s*""([^""]*)"""))
                attrs[m.Groups[1].Value] = m.Groups[2].Value;

            string fn = attrs.GetValueOrDefault("firstname", "");
            string ln = attrs.GetValueOrDefault("lastname", "");

            switch (header.Groups[1].Value)
            {
                case "Student":
                    int.TryParse(attrs.GetValueOrDefault("course", "0"), out int course);
                    return new Student(fn, ln, course,
                        attrs.GetValueOrDefault("studentId", ""),
                        attrs.GetValueOrDefault("gender", ""),
                        attrs.GetValueOrDefault("residence", ""),
                        attrs.GetValueOrDefault("recordBook", ""));
                case "Joiner":
                    return new Joiner(fn, ln, attrs.GetValueOrDefault("tool", ""));
                case "Photographer":
                    return new Photographer(fn, ln, attrs.GetValueOrDefault("cameraModel", ""));
            }
            return null;
        }
    }
}
