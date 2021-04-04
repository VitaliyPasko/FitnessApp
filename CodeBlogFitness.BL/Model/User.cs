using System;

namespace CodeBlogFitness.BL.Model
{

    #region Описание модели 
    
    /// <summary>
    /// Пользователь
    /// </summary>
    
    #endregion
    [Serializable]
    public class User
    {
        #region Свойства

        /// <summary>
        /// Имя.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Пол.
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Дата рождения.
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Вес пользователя.
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Рост пользователя.
        /// </summary>
        public double Height { get; set; }

        private DateTime _nowDate = DateTime.Today;
        private int _age;
        
        public int Age { get; set; }

        #endregion

        #region Описание конструктора

        /// <summary>
        /// Создать нового пользователя.
        /// </summary>
        /// <param name="name">Имя пользователя.</param>
        /// <param name="gender">Пол пользователя.</param>
        /// <param name="birthDate">Дата рождения пользователя.</param>
        /// <param name="weight">Вес пользователя.</param>
        /// <param name="height">Рост пользователя.</param>
        /// <param name="age">Полных лет.</param>
        /// <exception cref="ArgumentNullException">Выброс исключения при некорректных данных в параметрах.</exception>

        #endregion
        public User(string name,
            string gender,
            DateTime birthDate,
            double weight,
            double height,
            int age)
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
            if (string.IsNullOrWhiteSpace(gender))
                throw new ArgumentNullException("Пол не может быть null.", nameof(gender));
            if (age <= 5 || age > 100)
                throw new ArgumentNullException("Пользователю не может быть меньше 5 лет.", nameof(age));
            
            #endregion
            
            
            Name = name;
            Gender = new Gender(gender);
            BirthDate = birthDate;
            Weight = weight;
            Height = height;
            Age = age;
        }

        #region Описание перегрузки конструктора

        /// <summary>
        /// Перегрузка конструктора.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <exception cref="ArgumentNullException">Если userName не содержит данных.</exception>

        #endregion
        public User(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentNullException("Имя не может быть пустым.", nameof(userName));
            Name = userName;
        }

        #region Описание метода

        /// <summary>
        /// Перегрузка метода Tostring().
        /// </summary>
        /// <returns>Строка: имя прльзователя</returns>

        #endregion
        public override string ToString()
        {
            return $"{Name}";
        }
    }
}