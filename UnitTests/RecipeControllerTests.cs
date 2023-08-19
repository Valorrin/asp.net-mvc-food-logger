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
    public class RecipeControllerTests
    {
        private Mock<IRecipeRepository> _recipeRepositoryMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private Mock<IFoodRepository> _foodRepositoryMock;
        private Mock<IDiaryRepository> _diaryRepositoryMock;
        private RecipeController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _recipeRepositoryMock = new Mock<IRecipeRepository>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _foodRepositoryMock = new Mock<IFoodRepository>();
            _diaryRepositoryMock = new Mock<IDiaryRepository>();

            _controller = new RecipeController(
                _recipeRepositoryMock.Object,
                _httpContextAccessorMock.Object,
                _foodRepositoryMock.Object,
                _diaryRepositoryMock.Object);
        }

        [TestMethod]
        public void Create_InvalidRecipe_ReturnsViewWithError()
        {
            // Arrange
            var invalidRecipeViewModel = new CreateRecipeViewModel { Name = "" };
            _controller.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = _controller.Create(invalidRecipeViewModel) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.ViewData.ModelState.IsValid);
            Assert.AreEqual(invalidRecipeViewModel, result.Model);
        }

        [TestMethod]
        public async Task Delete_RecipeWithoutDiaryEntries_SuccessfullyDeletesRecipeAndRedirectsToDashboard()
        {
            // Arrange
            var recipeId = 1;
            var recipe = new Recipe { Id = recipeId };
            _recipeRepositoryMock.Setup(repo => repo.GetById(recipeId)).ReturnsAsync(recipe);
            _diaryRepositoryMock.Setup(repo => repo.GetAllEntriesByRecipeId(recipeId)).Returns(new List<DiaryEntry>());

            // Act
            var result = await _controller.Delete(recipeId) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("Dashboard", result.ControllerName);

            // Verify that Delete method was called on the repository with the correct recipe object
            _recipeRepositoryMock.Verify(repo => repo.Delete(It.IsAny<Recipe>()), Times.Once);
        }

    }
}
