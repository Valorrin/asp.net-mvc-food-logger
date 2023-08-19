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

        [TestMethod]
        public async Task IndexReturnsViewWithDashboardViewModel()
        {
            // Arrange
            var userFoods = new List<Food> { new Food(), new Food() };
            var userRecipes = new List<Recipe> { new Recipe(), new Recipe() };

            _dashboardRepositoryMock.Setup(repo => repo.GetAllUserFoods("testUserId"))
                .ReturnsAsync(userFoods);
            _dashboardRepositoryMock.Setup(repo => repo.GetAllUserRecipes("testUserId"))
                .ReturnsAsync(userRecipes);

            // Act
            var result = await _controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(DashboardViewModel));
            var model = result.Model as DashboardViewModel;
            Assert.AreEqual(userFoods.Count, model.Foods.Count);
            Assert.AreEqual(userRecipes.Count, model.Recipes.Count);
        }
        [TestMethod]
        public async Task Index_ReturnsViewWithDashboardViewModel()
        {
            // Arrange
            var userFoods = new List<Food> { new Food(), new Food() };
            var userRecipes = new List<Recipe> { new Recipe(), new Recipe() };

            _dashboardRepositoryMock.Setup(repo => repo.GetAllUserFoods("testUserId"))
                .ReturnsAsync(userFoods);
            _dashboardRepositoryMock.Setup(repo => repo.GetAllUserRecipes("testUserId"))
                .ReturnsAsync(userRecipes);

            // Act
            var result = await _controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(DashboardViewModel));
            var model = result.Model as DashboardViewModel;
            Assert.AreEqual(userFoods.Count, model.Foods.Count);
            Assert.AreEqual(userRecipes.Count, model.Recipes.Count);
        }
        // Additional test methods can be added here for other scenarios and actions

    }
}
