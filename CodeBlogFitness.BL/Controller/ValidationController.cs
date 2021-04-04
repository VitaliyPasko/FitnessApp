using System;
using CodeBlogFitness.BL.Model;

namespace CodeBlogFitness.BL.Controller
{
    public class ValidationController
    {
        public bool CheckUser(User user)
        {
            bool isCorrectData = string.IsNullOrWhiteSpace(user.Name)
                                 || user.Gender == null
                                 || string.IsNullOrWhiteSpace(user.Gender.Name)
                                 || user.Age == 0
                                 || user.Weight == 0
                                 || user.Height == 0
                                 || user.BirthDate == DateTime.Parse("01.01.0001");
            return isCorrectData;
        }
    }
}