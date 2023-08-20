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
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoodLogger.Tests
{
    [TestClass]
    public class DiaryControllerTests
    {
        private Mock<IDiaryRepository> _diaryRepositoryMock;
        private DiaryController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _diaryRepositoryMock = new Mock<IDiaryRepository>();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "testUserId") 
            };

            var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "TestAuthentication"));

            _controller = new DiaryController(_diaryRepositoryMock.Object)
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
        public void LoadDiary_ExistingDiary_ReturnsPartialViewWithViewModel()
        {
            var selectedDate = DateTime.Today;
            var diaryViewModel = new DiaryViewModel
            {
                SelectedDate = selectedDate,
                DiaryEntries = new List<DiaryEntry>(),
                TotalCalories = 0,
                TotalProtein = 0,
                TotalCarbs = 0,
                TotalFats = 0
            };

            _diaryRepositoryMock.Setup(repo => repo.GetDiaryByDate(It.IsAny<string>(), selectedDate))
                .Returns(new Diary { Id = 1, AppUserId = "testUserId", Date = selectedDate });
            _diaryRepositoryMock.Setup(repo => repo.GetDiaryEntriesForDate(selectedDate, It.IsAny<string>()))
                .Returns(new List<DiaryEntry>());

            var result =  _controller.LoadDiary(selectedDate) as PartialViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("_DiaryEntriesPartial", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(DiaryViewModel));
            var model = result.Model as DiaryViewModel;
            Assert.AreEqual(diaryViewModel.SelectedDate, model.SelectedDate);
            Assert.AreEqual(diaryViewModel.DiaryEntries.Count, model.DiaryEntries.Count);
            Assert.AreEqual(diaryViewModel.TotalCalories, model.TotalCalories);
            Assert.AreEqual(diaryViewModel.TotalProtein, model.TotalProtein);
            Assert.AreEqual(diaryViewModel.TotalCarbs, model.TotalCarbs);
            Assert.AreEqual(diaryViewModel.TotalFats, model.TotalFats);
        }

        [TestMethod]
        public void  LoadDiary_NonExistentDiary_CreatesNewDiaryAndReturnsPartialViewWithViewModel()
        {
            var selectedDate = DateTime.Today;
            var diaryViewModel = new DiaryViewModel
            {
                SelectedDate = selectedDate,
                DiaryEntries = new List<DiaryEntry>(),
                TotalCalories = 0,
                TotalProtein = 0,
                TotalCarbs = 0,
                TotalFats = 0
            };

            _diaryRepositoryMock.Setup(repo => repo.GetDiaryByDate(It.IsAny<string>(), selectedDate))
                .Returns((Diary)null);
            _diaryRepositoryMock.Setup(repo => repo.AddDiary(It.IsAny<Diary>()))
                .Callback<Diary>(diary => diary.Id = 1);
            _diaryRepositoryMock.Setup(repo => repo.GetDiaryEntriesForDate(selectedDate, It.IsAny<string>()))
                .Returns(new List<DiaryEntry>());

            var result =  _controller.LoadDiary(selectedDate) as PartialViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("_DiaryEntriesPartial", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(DiaryViewModel));
            var model = result.Model as DiaryViewModel;
            Assert.AreEqual(diaryViewModel.SelectedDate, model.SelectedDate);
            Assert.AreEqual(diaryViewModel.DiaryEntries.Count, model.DiaryEntries.Count);
            Assert.AreEqual(diaryViewModel.TotalCalories, model.TotalCalories);
            Assert.AreEqual(diaryViewModel.TotalProtein, model.TotalProtein);
            Assert.AreEqual(diaryViewModel.TotalCarbs, model.TotalCarbs);
            Assert.AreEqual(diaryViewModel.TotalFats, model.TotalFats);
        }

        [TestMethod]
        public void LoadDiary_ExistingDiaryWithEntries_ReturnsPartialViewWithViewModel()
        {
            var selectedDate = DateTime.Today;
            var diaryEntries = new List<DiaryEntry>
        {
            new DiaryEntry { Calories = 100, Protein = 10, Carbs = 20, Fats = 5 },
            new DiaryEntry { Calories = 150, Protein = 15, Carbs = 30, Fats = 8 }
        };

            _diaryRepositoryMock.Setup(repo => repo.GetDiaryByDate(It.IsAny<string>(), selectedDate))
                .Returns(new Diary { Id = 1, AppUserId = "testUserId", Date = selectedDate });
            _diaryRepositoryMock.Setup(repo => repo.GetDiaryEntriesForDate(selectedDate, It.IsAny<string>()))
                .Returns(diaryEntries);

            var expectedTotalCalories = diaryEntries.Sum(entry => entry.Calories);
            var expectedTotalProtein = diaryEntries.Sum(entry => entry.Protein);
            var expectedTotalCarbs = diaryEntries.Sum(entry => entry.Carbs);
            var expectedTotalFats = diaryEntries.Sum(entry => entry.Fats);

            var result = _controller.LoadDiary(selectedDate) as PartialViewResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("_DiaryEntriesPartial", result.ViewName);
            Assert.IsInstanceOfType(result.Model, typeof(DiaryViewModel));
            var model = result.Model as DiaryViewModel;
            Assert.AreEqual(selectedDate, model.SelectedDate);
            Assert.AreEqual(diaryEntries.Count, model.DiaryEntries.Count);
            Assert.AreEqual(expectedTotalCalories, model.TotalCalories);
            Assert.AreEqual(expectedTotalProtein, model.TotalProtein);
            Assert.AreEqual(expectedTotalCarbs, model.TotalCarbs);
            Assert.AreEqual(expectedTotalFats, model.TotalFats);
        }


    }
}
