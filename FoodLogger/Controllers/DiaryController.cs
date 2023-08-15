using FoodLogger.Interfaces;
using FoodLogger.Models;
using FoodLogger.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodLogger.Controllers
{
    public class DiaryController : Controller
    {
        private readonly IDiaryRepository diaryRepository;

        public DiaryController(IDiaryRepository diaryRepository)
        {
            this.diaryRepository = diaryRepository;
        }

        public IActionResult Index(DateTime selectedDate)
        {
            var diaryEntries = diaryRepository.GetDiaryEntriesForDate(selectedDate);

            var diaryViewModel = new DiaryViewModel
            {
                SelectedDate = selectedDate,
                DiaryEntries = diaryEntries
            };

            return View(diaryViewModel);
        }
    }
}
