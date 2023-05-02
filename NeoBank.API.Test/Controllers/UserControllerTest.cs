using Autofac;
using Autofac.Extras.Moq;
using BusinessDomain.User;
using BusinessModel.Common;
using BusinessModel.User;
using DataAccess.User;
using Microsoft.AspNetCore.Mvc;
using NeoBank.API.Controllers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBank.API.Test.Controllers
{
    public class UserControllerTest
    {
        private readonly IContainer _container;

        public UserControllerTest()
        {
            _container = new ContainerResolver().Container;
        }

        [Test]
        public void CreateUserDetails_ItShouldReturnstatus()
        {
            UserModel request = new UserModel()
            {
                shopname = "Test",
                title = "testtitle",
                firstName = "TestFirstname",
                middleName = "Middle",
                lastName = "Last",
                gender = "Male",
                mobileNo = "9874563210",
                emailAddress = "test@gmail.com"
            };

            StatusResponseModel responses = new StatusResponseModel();


            using (var mock = AutoMock.GetStrict())
            {
                // Arrange - configure the mock
                mock.Mock<IUserDA>().Setup(x => x.CreateUserDetails(request)).Returns(responses);
                var userService = mock.Create<UserBusiness>();

                // Act
                UserController controller = new UserController(userService);
                var actual = controller.CreateUserDetails(request);
                var okResult = actual as OkObjectResult;

                // Assert - assert on the mock
                mock.Mock<IUserDA>().Verify(x => x.CreateUserDetails(request));
                Assert.NotNull(okResult);
            }
        }

        [Test]
        public void GetUserDetailsById_ItShouldReturnUserDetailsById()
        {
            int id = 1;

            List<UserModel> responses = new List<UserModel>()
            {
                new UserModel()
            };

            using (var mock = AutoMock.GetStrict())
            {
                // Arrange - configure the mock
                mock.Mock<IUserDA>().Setup(x => x.GetUserDetailsById(id)).Returns(responses);
                var userService = mock.Create<UserBusiness>();

                // Act
                UserController controller = new UserController(userService);
                var actual = controller.GetUserDetailsById(id);
                var okResult = actual as OkObjectResult;

                // Assert - assert on the mock
                mock.Mock<IUserDA>().Verify(x => x.GetUserDetailsById(id));
                Assert.NotNull(okResult);
            }
        }
    }
}
