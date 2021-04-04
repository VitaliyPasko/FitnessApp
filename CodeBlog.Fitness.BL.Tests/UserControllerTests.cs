using System;
using CodeBlogFitness.BL.Controller;
using Xunit;

namespace CodeBlog.Fitness.BL.Tests
{
    public class UserControllerTests
    {
        [Fact]
        public void SaveTest()
        {
            //Arrange
            var userName = Guid.NewGuid().ToString();
            //Act
            var controller = new UserController(userName);
            //Assert
            Assert.Equal(userName,controller.CurrentUser.Name);
        }
        [Fact]
        public void SetNewUserDataTest()
        {
            //Arrange
            var userName = Guid.NewGuid().ToString();
            var birthDate = DateTime.Parse("12.06.1987");
            var weight = 90;
            var gender = "man";
            var height = 190;
            var age = 33;
            //Act
            
            var controller = new UserController(userName);
            controller.SetNewUserData(gender, birthDate, weight, height);
            var controller2 = new UserController(userName);
            
            //Assert
            Assert.Equal(userName, controller2.CurrentUser.Name);
            Assert.Equal(birthDate.ToShortDateString(), controller2.CurrentUser.BirthDate.ToShortDateString());
            Assert.Equal(weight, controller2.CurrentUser.Weight);
            Assert.Equal(height, controller2.CurrentUser.Height);
            Assert.Equal(gender, controller2.CurrentUser.Gender.Name);
            Assert.Equal(age, controller2.CurrentUser.Age);


            Console.ReadLine();
            

        }
    }
}