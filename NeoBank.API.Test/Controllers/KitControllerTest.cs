using Autofac;
using Autofac.Extras.Moq;
using BusinessDomain.Kit;
using BusinessModel.Common;
using BusinessModel.Kit;
using DataAccess.Kit;
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
    public class KitControllerTest
    {
        private readonly IContainer _container;

        public KitControllerTest()
        {
            _container = new ContainerResolver().Container;
        }


        [Test]
        public void CreateKitDetails_ItShouldReturnStatus()
        {
            KitModel request = new KitModel()
            {
                KitReferenceNumber = "Kit1",
                CompanyCode = "Comp001",
                CompanyAdminCode = "CompAdmin001",
            };

            StatusResponseModel responses = new StatusResponseModel();


            using (var mock = AutoMock.GetStrict())
            {
                // Arrange - configure the mock
                mock.Mock<IKitDA>().Setup(x => x.CreateKitDetails(request)).Returns(responses);
                var kitService = mock.Create<KitBusiness>();

                // Act
                KitController controller = new KitController(kitService);
                var actual = controller.CreateKitDetails(request);
                var okResult = actual as OkObjectResult;

                // Assert - assert on the mock
                mock.Mock<IKitDA>().Verify(x => x.CreateKitDetails(request));
                Assert.NotNull(okResult);
            }
        }

        [Test]
        public void UpdateKitDetails_ItShouldReturnStatus()
        {
            KitModel request = new KitModel()
            {
                KitId=1,
                KitReferenceNumber = "Kit1",
                CompanyCode = "Comp001",
                CompanyAdminCode = "CompAdmin001",
            };

            StatusResponseModel responses = new StatusResponseModel();


            using (var mock = AutoMock.GetStrict())
            {
                // Arrange - configure the mock
                mock.Mock<IKitDA>().Setup(x => x.UpdateKitDetails(request)).Returns(responses);
                var kitService = mock.Create<KitBusiness>();

                // Act
                KitController controller = new KitController(kitService);
                var actual = controller.UpdateKitDetails(request);
                var okResult = actual as OkObjectResult;

                // Assert - assert on the mock
                mock.Mock<IKitDA>().Verify(x => x.UpdateKitDetails(request));
                Assert.NotNull(okResult);
            }
        }

        [Test]
        public void GetKitDetailsById_ItShouldReturnKitDetailsById()
        {
            int id = 1;

            List<KitModel> responses = new List<KitModel>()
            {
                new KitModel()
            };

            using (var mock = AutoMock.GetStrict())
            {
                // Arrange - configure the mock
                mock.Mock<IKitDA>().Setup(x => x.GetKitDetailsById(id)).Returns(responses);
                var kitService = mock.Create<KitBusiness>();

                // Act
                KitController controller = new KitController(kitService);
                var actual = controller.GetKitDetailsById(id);
                var okResult = actual as OkObjectResult;

                // Assert - assert on the mock
                mock.Mock<IKitDA>().Verify(x => x.GetKitDetailsById(id));
                Assert.NotNull(okResult);
            }
        }
    }
}
