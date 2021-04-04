using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using CodeBlogFitness.BL.Model;

namespace CodeBlogFitness.BL.Controller
{
    /// <summary>
    /// Контроллер пользователя.
    /// </summary>
    public class UserController
    {
        /// <summary>
        /// Путь до файла со списком пользователей
        /// </summary>
        private const string UserPath = "users.dat";
        /// <summary>
        /// Текущий пользователь.
        /// </summary>
        public User CurrentUser { get; }
        /// <summary>
        /// Список пользователей.
        /// </summary>
        public List<User> Users { get; }
        /// <summary>
        /// Контроллер для проверки на валидность данных пользователя
        /// </summary>
        private readonly ValidationController _validationController;

        public bool IsNewUser { get; set; } = true;
        /// <summary>
        /// Создание нового контроллера пользователя.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        public UserController(string userName)
        {
            _validationController = new ValidationController();
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException("имя пользователя не может быть пустым");
            Users = GetUserData();
            if (Users.Count != 0)
            {
                CurrentUser = Users.SingleOrDefault(u => u.Name == userName) ?? new User(userName);
                IsNewUser = _validationController.CheckUser(CurrentUser);
                if (!IsNewUser)
                {
                    Users.Add(CurrentUser);
                    Save();
                }
            }
        }
      
        /// <summary>
        /// Сохранить данные пользователя в файл.
        /// </summary>
        private void Save()
        {
            using BinaryWriter writer = new BinaryWriter(File.Open(UserPath, FileMode.Create));
            if (Users.Count != 0)
            {
                foreach (var user in Users)
                {
                    writer.Write(user.Name);
                    writer.Write(user.Gender.Name);
                    writer.Write(user.BirthDate.Date.ToString(CultureInfo.InvariantCulture));
                    writer.Write(user.Weight);
                    writer.Write(user.Height);
                }
            }
        }
        /// <summary>
        /// Получить список пользователей из файла.
        /// </summary>
        /// <returns>Список пользователей.</returns>
        /// <exception cref="FileLoadException">Если файла users.dat нет.</exception>
        private List<User> GetUserData()
        {
            try
            {
                List<User> users = new List<User>();
                if (File.Exists(UserPath))
                {
                    string data = File.ReadAllText(UserPath);
                    if (data.Trim() != "")
                    {
                        using BinaryReader reader = new BinaryReader(File.Open(UserPath, FileMode.Open));
                        while (reader.PeekChar() > -1)
                        {
                            users.Add(new User(
                                reader.ReadString(),
                                new Gender(reader.ReadString()),
                                DateTime.Parse(reader.ReadString()),
                                reader.ReadDouble(),
                                reader.ReadDouble()));
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

        public void SetNewUserData(string genderName, DateTime birthDate, double weight = 1, double height = 1)
        {
            //TODO: проверка
            CurrentUser.Gender = new Gender(genderName);
            CurrentUser.BirthDate = birthDate;
            CurrentUser.Weight = weight;
            CurrentUser.Height = height;
            Users.Add(CurrentUser);
            Save();
        }
    }
}