using Autofac;
using Autofac.Extras.Moq;
using BusinessDomain.Company;
using BusinessModel.Common;
using BusinessModel.Company;
using DataAccess.Company;
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
    public class CompanyControllerTest
    {
        private readonly IContainer _container;

        public CompanyControllerTest()
        {
            _container = new ContainerResolver().Container;
        }

        [Test]
        public void CreateCompanyDetails_ItShouldReturnStatus()
        {
            CompanyModel request = new CompanyModel()
            {
                Name = "AbcCompany",
                Ownername = "TestOwnerName",
                Email = "Test@gmail.com",
                Mobile = "9874563210",
                Websiteurl = "abc.com"
            };

            StatusModel responses = new StatusModel();


            using (var mock = AutoMock.GetStrict())
            {
                // Arrange - configure the mock
                mock.Mock<ICompanyDA>().Setup(x => x.CreateCompanyDetails(request)).Returns(responses);
                var companyService = mock.Create<CompanyBusiness>();

                // Act
                CompanyController controller = new CompanyController(companyService);
                var actual = controller.CreateCompanyDetails(request);
                var okResult = actual as OkObjectResult;

                // Assert - assert on the mock
                mock.Mock<ICompanyDA>().Verify(x => x.CreateCompanyDetails(request));
                Assert.NotNull(okResult);
            }
        }

        [Test]
        public void GetCompanyDetailsById_ItShouldReturnCompanyDetailsById()
        {
            int id = 1;

            List<CompanyModel> responses = new List<CompanyModel>()
            {
                new CompanyModel()
            };

            using (var mock = AutoMock.GetStrict())
            {
                // Arrange - configure the mock
                mock.Mock<ICompanyDA>().Setup(x => x.GetCompanyDetailsById(id)).Returns(responses);
                var companyService = mock.Create<CompanyBusiness>();

                // Act
                CompanyController controller = new CompanyController(companyService);
                var actual = controller.GetCompanyDetailsById(id);
                var okResult = actual as OkObjectResult;

                // Assert - assert on the mock
                mock.Mock<ICompanyDA>().Verify(x => x.GetCompanyDetailsById(id));
                Assert.NotNull(okResult);
            }
        }

        [Test]
        public void UpdateCompanyDetails_ItShouldReturnStatus()
        {
            CompanyModel request = new CompanyModel()
            {
                Id=1,
                Name = "AbcCompany",
                Ownername = "TestOwnerName",
                Email = "Test@gmail.com",
                Mobile = "9874563210",
                Websiteurl = "abc.com"
            };

            StatusModel responses = new StatusModel();


            using (var mock = AutoMock.GetStrict())
            {
                // Arrange - configure the mock
                mock.Mock<ICompanyDA>().Setup(x => x.UpdateCompanyDetails(request)).Returns(responses);
                var companyService = mock.Create<CompanyBusiness>();

                // Act
                CompanyController controller = new CompanyController(companyService);
                var actual = controller.UpdateCompanyDetails(request);
                var okResult = actual as OkObjectResult;

                // Assert - assert on the mock
                mock.Mock<ICompanyDA>().Verify(x => x.UpdateCompanyDetails(request));
                Assert.NotNull(okResult);
            }
        }

        [Test]
        public void DeleteCompanyDetailsById_ItShouldReturnStatus()
        {
            int id = 1;

            StatusModel responses = new StatusModel();

            using (var mock = AutoMock.GetStrict())
            {
                // Arrange - configure the mock
                mock.Mock<ICompanyDA>().Setup(x => x.DeleteCompanyDetailsById(id)).Returns(responses);
                var companyService = mock.Create<CompanyBusiness>();

                // Act
                CompanyController controller = new CompanyController(companyService);
                var actual = controller.DeleteCompanyDetailsById(id);
                var okResult = actual as OkObjectResult;

                // Assert - assert on the mock
                mock.Mock<ICompanyDA>().Verify(x => x.DeleteCompanyDetailsById(id));
                Assert.NotNull(okResult);
            }
        }
    }
}
