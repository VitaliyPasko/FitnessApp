using System;
using Microsoft.VisualBasic.CompilerServices;

namespace CodeBlogFitness.BL.Model
{
    /// <summary>
    /// Пользователь
    /// </summary>
    [Serializable]
    public class User
    {
        #region Свойства

        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Пол.
        /// </summary>
        public Gender Gender { get; }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public DateTime BirthDate { get; }

        /// <summary>
        /// Вес пользователя.
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Рост пользователя.
        /// </summary>
        public double Height { get; set; }

        #endregion

        /// <summary>
        /// Создать нового пользователя.
        /// </summary>
        /// <param name="name">Имя пользователя.</param>
        /// <param name="gender">Пол пользователя.</param>
        /// <param name="birthDate">Дата рождения пользователя.</param>
        /// <param name="weight">Вес пользователя.</param>
        /// <param name="height">Рост пользователя.</param>
        /// <exception cref="ArgumentNullException">Выброс исключения при некорректных данных в параметрах.</exception>
        public User(string name,
            Gender gender,
            DateTime birthDate,
            double weight,
            double height)
        {
            #region Условия проверки

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Имя не может быть пустым.", nameof(name));
            if (birthDate < DateTime.Parse("01.01.1900") || birthDate >= DateTime.Now)
                throw new ArgumentNullException("Некорректная дата рождения.", nameof(birthDate));
            if (weight <= 0)
                throw new ArgumentNullException("Вес не может быть меньше или равен 0.", nameof(weight));
            if (height <= 0)
                throw new ArgumentNullException("Рост не может быть меньше или равен 0.", nameof(height));

            #endregion

            Name = name;
            Gender = gender ?? throw new ArgumentNullException("Пол не может быть null.", nameof(gender));
            BirthDate = birthDate;
            Weight = weight;
            Height = height;
        }

        public override string ToString()
        {
            return $"{Name} {Gender.Name} {BirthDate} {Weight} {Height}";
        }
    }
}