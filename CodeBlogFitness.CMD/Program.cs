using System;
using CodeBlogFitness.BL.Controller;
using CodeBlogFitness.BL.Model;

namespace CodeBlogFitness.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Вас приветствует приложение CodeBlogFitness");
            Console.WriteLine("Введите имя пользователя");
            var name = Console.ReadLine();
            var userController = new UserController(name);
            Console.WriteLine(userController.CurrentUser);
            if (userController.IsNewUser)
            {
                string genderName = GetGenderName();
                DateTime birthDate = ParseToDate();
                // birthDate = birthDate.AddHours(5);
                // birthDate = birthDate.AddMinutes(5);
                // birthDate = birthDate.AddSeconds(5);
                Console.WriteLine(birthDate);
                double weight = ParseToDouble("вес");
                double height = ParseToDouble("рост");
                userController.SetNewUserData(genderName, birthDate, height, weight);
                Console.WriteLine(userController.CurrentUser.Age);
            }
        }

        private static string GetGenderName()
        {
            Console.WriteLine("Введите ваш пол:");
            while (true)
            {
                string genderName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(genderName))
                    return genderName;
                Console.WriteLine("Некорректные данные. Введите снова:");
            }
        }

        private static DateTime ParseToDate()
        {
            Console.WriteLine("Введите дату рождения в формате (dd.mm.yyyy):");
            while (true)
            {
                bool isCorrectDate = DateTime.TryParse(Console.ReadLine(), out var birthDate);
                if (isCorrectDate)
                    return birthDate;
                Console.WriteLine("Некорректная дата, введите снова:");
            }
        }

        private static double ParseToDouble(string value)
        {
            Console.WriteLine($"Введите {value}");
            while (true)
            {
                bool isNumber = double.TryParse(Console.ReadLine(), out double input);
                if (isNumber)
                    return input;
                Console.WriteLine("Некорректные данные, введите свой вес снова:");
            }
        }
    }
}