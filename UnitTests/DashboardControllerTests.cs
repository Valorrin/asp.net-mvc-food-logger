using FoodLogger.Controllers;
using FoodLogger.Data.Models;
using FoodLogger.Interfaces;
using FoodLogger.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodLogger.Tests
{
    [TestClass]
    public class DashboardControllerTests
    {
        private Mock<IDashboardRepository> _dashboardRepositoryMock;
        private DashboardController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _dashboardRepositoryMock = new Mock<IDashboardRepository>();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "testUserId") // Replace with the actual user ID
            };

            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuthentication"));

            _controller = new DashboardController(_dashboardRepositoryMock.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = userPrincipal
                    }
                }
            };
        }

    }
}
