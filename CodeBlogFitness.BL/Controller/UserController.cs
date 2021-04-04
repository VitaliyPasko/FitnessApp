using System;
using System.Globalization;
using System.IO;
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
        /// Создание нового контроллера пользователя.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="genderName">Пол пользователя.</param>
        /// <param name="birthDay">Дата рождения пользователя.</param>
        /// <param name="weight">Вес пользователя.</param>
        /// <param name="height">Рост пользователя</param>
        public UserController(string userName, string genderName, DateTime birthDay, double weight, double height)
        {
            //TODO: проверка
            User = new User(userName, new Gender(genderName), birthDay, weight, height);
        }
        /// <summary>
        /// Пользователь приложения.
        /// </summary>
        private const string UserPath = "users.dat";
        public User User { get; }
        /// <summary>
        /// Сохранить данные пользователя в файл.
        /// </summary>
        public void Save()
        {
            using BinaryWriter writer = new BinaryWriter(File.Open(UserPath, FileMode.Create));
            writer.Write(User.Name);
            writer.Write(User.Gender.Name);
            writer.Write(User.BirthDate.Date.ToString(CultureInfo.InvariantCulture));
            writer.Write(User.Weight);
            writer.Write(User.Height);
        }
        /// <summary>
        /// Загрузить пользователя из файла.
        /// </summary>
        /// <returns>Пользователь из файла.</returns>
        /// <exception cref="FileLoadException">Если файла users.dat нет.</exception>
        public UserController()
        {
            if (File.Exists(UserPath))
            {
                using BinaryReader reader = new BinaryReader(File.Open(UserPath, FileMode.Open));
                User = new User(
                    reader.ReadString(),
                    new Gender(reader.ReadString()),
                    DateTime.Parse(reader.ReadString()),
                    reader.ReadDouble(),
                    reader.ReadDouble());
            }
            else
                throw new FileLoadException("Не удалось получить пользователя из файла",  UserPath);
            
            //TODO: что делать, если не прочитали пользователя?
        }
    }
}