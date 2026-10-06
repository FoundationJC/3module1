using System.Text.RegularExpressions;

namespace OopLab.FileStorage
{
    // Перевірка коректності даних регулярними виразами.
    public static class Validators
    {
        // Студентський квиток: 2 літери + 6 цифр, напр. KB123456
        public static bool IsValidStudentId(string id) =>
            Regex.IsMatch(id ?? "", @"^[A-ZА-ЯІЇЄ]{2}\d{6}$");

        // Залікова книжка: ЗК + 6 цифр
        public static bool IsValidRecordBook(string rb) =>
            Regex.IsMatch(rb ?? "", @"^ЗК\d{6}$");

        // Ім'я/прізвище/місто: тільки літери, апостроф, дефіс, пробіл
        public static bool IsValidNameOrPlace(string s) =>
            Regex.IsMatch(s ?? "", @"^[A-Za-zА-Яа-яІіЇїЄєҐґ' \-]+$");

        // Курс: 1–6
        public static bool IsValidCourse(int course) => course >= 1 && course <= 6;

        // Стать: чоловік / жінка
        public static bool IsValidGender(string g) =>
            g == "чоловік" || g == "жінка";
    }
}
