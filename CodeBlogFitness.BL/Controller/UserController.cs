using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using CodeBlogFitness.BL.Model;

namespace CodeBlogFitness.BL.Controller
{
    #region Описание класса

    /// <summary>
    /// Контроллер пользователя.
    /// </summary>

    #endregion
    public class UserController
    {
        #region Описаине поля

        /// <summary>
        /// Путь до файла со списком пользователей
        /// </summary>

        #endregion
        private const string UserPath = "users.dat";

        #region Описание поля

        /// <summary>
        /// Текущий пользователь.
        /// </summary>

        #endregion
        public User CurrentUser { get; }

        #region Описание поля

        /// <summary>
        /// Список пользователей.
        /// </summary>

        #endregion
        public List<User> Users { get; }

        #region Описание поля

        /// <summary>
        /// Контроллер для проверки на валидность данных пользователя
        /// </summary>
        #endregion
        private readonly ValidationController _validationController;

        public bool IsNewUser { get; set; } = true;

        #region Описание контроллера
        /// <summary>
        /// Создание нового контроллера пользователя.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        #endregion
        public UserController(string userName)
        {
            _validationController = new ValidationController();
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException("имя пользователя не может быть пустым");
            Users = GetUserData();
            if (Users.Count != 0)
            {
                CurrentUser = Users.SingleOrDefault(u => u.Name == userName) ?? new User(userName);
                if (CurrentUser != null)
                {
                    IsNewUser = _validationController.CheckUser(CurrentUser);
                    
                    if (!IsNewUser)
                    {
                        Console.WriteLine($"{CurrentUser.Name} {CurrentUser.Gender} {CurrentUser.Height} {CurrentUser.Weight} {CurrentUser.BirthDate.ToShortDateString()} {CurrentUser.Age}");
                        File.WriteAllText(UserPath, "");
                        Save();
                        return;
                    }
                }
            }
            CurrentUser = new User(userName);
        }

        #region Описаине метода
        /// <summary>
        /// Сохранить данные пользователя в файл.
        /// </summary>
        #endregion
        private void Save()
        {
            using BinaryWriter writer = new BinaryWriter(File.Open(UserPath, FileMode.Create));
            if (Users.Count != 0)
            {
                foreach (var user in Users)
                {
                    writer.Write(user.Name);
                    writer.Write(user.Gender.Name);
                    writer.Write(user.BirthDate.ToString("dd.mm.yyyy"));
                    writer.Write(user.Weight);
                    writer.Write(user.Height);
                    writer.Write(user.Age);
                }
            }
        }
        #region Описание метода
        /// <summary>
        /// Получить список пользователей из файла.
        /// </summary>
        /// <returns>Список пользователей.</returns>
        /// <exception cref="FileLoadException">Если файла users.dat нет.</exception>
        #endregion
        private List<User> GetUserData()
        {
            try
            {
                List<User> users = new List<User>();
                if (File.Exists(UserPath))
                {
                    string data = File.ReadAllText(UserPath);
                    if (!string.IsNullOrWhiteSpace(data))
                    {
                        using BinaryReader reader = new BinaryReader(File.Open(UserPath, FileMode.Open));
                        while (reader.PeekChar() > -1)
                        {
                            users.Add(new User(
                                reader.ReadString(),
                                reader.ReadString(),
                                 DateTime.ParseExact(reader.ReadString(), "dd.mm.yyyy", CultureInfo.InvariantCulture).Date,
                                reader.ReadDouble(),
                                reader.ReadDouble(),
                                reader.ReadInt32()));
                        }
                    }
                }
                return users;
            }
            catch (FileLoadException e)
            {
                Console.WriteLine("Не удалось взять пользователей из файла.");
                return new List<User>();
            }
        }
        #region Описание метода
        
        /// <summary>
        /// Инициализация нового пользователя.
        /// Сохранение его в файл.
        /// </summary>
        /// <param name="genderName">Пол пользователя</param>
        /// <param name="birthDate">Дата рождения</param>
        /// <param name="weight">Вес пользователя</param>
        /// <param name="height">Рост пользователя</param>
        
        #endregion
        public void SetNewUserData(string genderName, DateTime birthDate, double weight = 1, double height = 1)
        {
            #region Проверка
            if (string.IsNullOrWhiteSpace(genderName))
                new ArgumentNullException(nameof(genderName), "Пол не может быть пустым.");
            if (birthDate == DateTime.Parse("01.01.0001"))
                new ArgumentNullException(nameof(birthDate), "Дата не может быть пустой.");
            if (weight < 15 || weight > 200)
                new ArgumentNullException(nameof(weight), "Вес меньше 15 килограмм и больше 200 килограм недопустим.");
            if (height < 40 || height > 250)
                new ArgumentNullException(nameof(height), "Рост меньше 40см или больше 250см недопустим.");
            #endregion
            CurrentUser.Gender = new Gender(genderName);
            CurrentUser.BirthDate = birthDate;
            CurrentUser.Weight = weight;
            CurrentUser.Height = height;
            CurrentUser.Age = GetAge(birthDate);
            Users.Add(CurrentUser);
            File.WriteAllText(UserPath, "");
            Save();
        }

        #region Описание метода

        /// <summary>
        /// Из введенной пользователем даты рождения получаем количество полных лет.
        /// </summary>
        /// <param name="birthDate">Введенная пользователем дата рождения</param>
        /// <returns>Количество полных лет</returns>

        #endregion
        private int GetAge(DateTime birthDate)
        {
            DateTime nowDate = DateTime.Today;
            int age = nowDate.Year - birthDate.Year;
            if (birthDate > nowDate.AddYears(-age)) age--;
            return age;
        }
    }
}