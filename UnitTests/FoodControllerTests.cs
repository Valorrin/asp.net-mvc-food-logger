using FoodLogger.Controllers;
using FoodLogger.Data.Models;
using FoodLogger.Interfaces;
using FoodLogger.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace FoodLogger.Tests
{
    [TestClass]
    public class FoodControllerTests
    {
        private Mock<IFoodRepository> foodRepositoryMock;
        private Mock<IHttpContextAccessor> httpContextAccessorMock;
        private Mock<IDiaryRepository> diaryRepositoryMock;
        private FoodController controller;

        [TestInitialize]
        public void Initialize()
        {
            foodRepositoryMock = new Mock<IFoodRepository>();
            httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            diaryRepositoryMock = new Mock<IDiaryRepository>();
            controller = new FoodController(foodRepositoryMock.Object, httpContextAccessorMock.Object, diaryRepositoryMock.Object);
        }

        [TestMethod]
        public void Create_ValidFood_RedirectsToDashboard()
        {
            var foodVM = new FoodViewModel
            {
                Name = "Orange",
                Grams = 120,
                Calories = 50,
                Protein = 1,
                Carbs = 10,
                Fat = 0,
                AppUserId = "testUserId"
            };

            var result = controller.Create(foodVM) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("Dashboard", result.ControllerName);
        }

        [TestMethod]
        public void Create_InvalidFood_ReturnsViewWithError()
        {
            var invalidFoodVM = new FoodViewModel { Name = "InvalidFood" };
            controller.ModelState.AddModelError("Calories", "Calories is required");

            var result = controller.Create(invalidFoodVM) as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(invalidFoodVM, result.Model);
            Assert.IsFalse(result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void Edit_ValidFood_RedirectsToDashboard()
        {
            var foodVM = new EditFoodViewModel
            {
                Id = 1,
                Name = "Updated Apple",
                Grams = 100,
                Calories = 60,
                Protein = 2,
                Carbs = 12,
                Fat = 0.5
            };

            var result = controller.Edit(foodVM) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("Dashboard", result.ControllerName);
        }

        [TestMethod]
        public void Delete_FoodWithoutDiaryEntries_RedirectsToDashboard()
        {
            var foodId = 1;
            var food = new Food { Id = foodId };
            foodRepositoryMock.Setup(repo => repo.GetById(foodId)).Returns(food);
            diaryRepositoryMock.Setup(repo => repo.GetAllEntriesByFoodId(foodId)).Returns(new List<DiaryEntry>());

            var result = controller.Delete(foodId) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("Dashboard", result.ControllerName);
        }

        [TestMethod]
        public async Task Index_ReturnsViewWithFoods()
        {
            var foods = new List<Food> { new Food(), new Food() };
            foodRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(foods);

            var result = await controller.Index() as ViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(foods, result.Model);
        }
    }
}
