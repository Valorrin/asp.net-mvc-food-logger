using FoodLogger.Controllers;
using FoodLogger.Data.Models;
using FoodLogger.Interfaces;
using FoodLogger.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FoodLogger.Tests
{
    [TestClass]
    public class DiaryEntryControllerTests
    {
        private Mock<IFoodRepository> _foodRepositoryMock;
        private Mock<IRecipeRepository> _recipeRepositoryMock;
        private Mock<IDiaryRepository> _diaryRepositoryMock;
        private Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private DiaryEntryController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _foodRepositoryMock = new Mock<IFoodRepository>();
            _recipeRepositoryMock = new Mock<IRecipeRepository>();
            _diaryRepositoryMock = new Mock<IDiaryRepository>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            _controller = new DiaryEntryController(
                _foodRepositoryMock.Object,
                _recipeRepositoryMock.Object,
                _diaryRepositoryMock.Object,
                _httpContextAccessorMock.Object);
        }

        [TestMethod]
        public void AddRecipeEntry_ValidData_SuccessfullyAddsEntryAndRedirects()
        {
            // Arrange
            var entryViewModel = new DiaryEntryViewModel
            {
                SelectedRecipeId = 1,
                Quantity = 1,
                DiaryId = 1,
                DiaryDate = DateTime.Today
            };

            // Act
            var result = _controller.AddRecipeEntry(entryViewModel) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("Diary", result.ControllerName);

            // Verify that AddDiaryEntry was called with the correct diary entry
            _diaryRepositoryMock.Verify(repo => repo.AddDiaryEntry(It.IsAny<DiaryEntry>()), Times.Once);
        }

        [TestMethod]
        public void Edit_ExistingEntry_ReturnsViewWithModel()
        {
            var entryId = 1;
            var existingEntry = new DiaryEntry
            {
                Id = entryId,
                FoodId = 1,
                Quantity = 200,
                Calories = 100,
                Protein = 10,
                Carbs = 20,
                Fats = 5
            };
            _diaryRepositoryMock.Setup(repo => repo.GetDiaryEntryById(entryId))
                              .Returns(existingEntry);

            var result = _controller.Edit(entryId) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(EditEntryViewModel));
            var model = result.Model as EditEntryViewModel;
            Assert.AreEqual(entryId, model.Id);
            Assert.AreEqual(existingEntry.Quantity, model.Quantity);
        }

        [TestMethod]
        public void Edit_InvalidModelState_ReturnsViewWithModel()
        {
            var model = new EditEntryViewModel
            {
                Id = 1,
                Quantity = -100 
            };
            _controller.ModelState.AddModelError("Quantity", "Quantity must be a positive value");

            var result = _controller.Edit(model) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(EditEntryViewModel));
            var returnedModel = result.Model as EditEntryViewModel;
            Assert.AreEqual(model.Id, returnedModel.Id);
            Assert.AreEqual(model.Quantity, returnedModel.Quantity);
        }

        [TestMethod]
        public void Delete_ExistingEntry_SuccessfullyDeletesEntryAndRedirectsToIndex()
        {
            var entryId = 1;
            var selectedDate = DateTime.Today;

            var entryToDelete = new DiaryEntry
            {
                Id = entryId,
                DiaryId = 1,
                FoodId = 1,
                Quantity = 100,
                Calories = 50,
                Protein = 2,
                Carbs = 10,
                Fats = 1
            };

            _diaryRepositoryMock.Setup(repo => repo.GetDiaryEntryById(entryId)).Returns(entryToDelete);

            var result = _controller.Delete(entryId, selectedDate) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("Diary", result.ControllerName);

            _diaryRepositoryMock.Verify(repo => repo.DeleteEntry(entryToDelete), Times.Once);
        }
    }
}
