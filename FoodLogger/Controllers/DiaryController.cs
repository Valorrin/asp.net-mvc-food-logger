using FoodLogger.Data.Models;
using FoodLogger.Interfaces;
using FoodLogger.Models;
using FoodLogger.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FoodLogger.Controllers
{
    public class DiaryController : Controller
    {
        private readonly IDiaryRepository diaryRepository;

        public DiaryController(IDiaryRepository diaryRepository)
        {
            this.diaryRepository = diaryRepository;
        }

        public IActionResult Index(DateTime? selectedDate)
        {
            var appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (selectedDate == null)
            {
                selectedDate = DateTime.Today;
            }

            var existingDiary = diaryRepository.GetDiaryByDate(appUserId, selectedDate.Value);

            if (existingDiary == null)
            {
                var newDiary = new Diary
                {
                    AppUserId = appUserId,
                    Date = selectedDate.Value
                };
                diaryRepository.AddDiary(newDiary);
                existingDiary = newDiary;
            }

            var diaryEntries = diaryRepository.GetDiaryEntriesForDate(selectedDate);

            var diaryViewModel = new DiaryViewModel
            {
                DiaryId = existingDiary.Id,
                SelectedDate = selectedDate.Value,
                DiaryEntries = diaryEntries
            };

            return View(diaryViewModel);
        }

        [HttpGet]
        public IActionResult LoadDiary(DateTime? selectedDate)
        {
            var appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (selectedDate == null)
            {
                selectedDate = DateTime.Today;
            }
            var existingDiary = diaryRepository.GetDiaryByDate(appUserId, selectedDate.Value);

            if (existingDiary == null)
            {
                var newDiary = new Diary
                {
                    AppUserId = appUserId,
                    Date = selectedDate.Value
                };
                diaryRepository.AddDiary(newDiary);
            }

            var diaryEntries = diaryRepository.GetDiaryEntriesForDate(selectedDate);

            var diaryViewModel = new DiaryViewModel
            {
                SelectedDate = selectedDate,
                DiaryEntries = diaryEntries
            };

            return PartialView("_DiaryEntriesPartial", diaryViewModel);
        }

    }
}
