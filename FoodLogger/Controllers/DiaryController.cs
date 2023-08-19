using FoodLogger.Data.Models;
using FoodLogger.Interfaces;
using FoodLogger.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Authorize]
public class DiaryController : Controller
{
    private readonly IDiaryRepository diaryRepository;

    public DiaryController(IDiaryRepository diaryRepository)
    {
        this.diaryRepository = diaryRepository;
    }

    private DiaryViewModel GetDiaryViewModel(DateTime selectedDate)
    {
        var appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var existingDiary = diaryRepository.GetDiaryByDate(appUserId, selectedDate);

        if (existingDiary == null)
        {
            var newDiary = new Diary
            {
                AppUserId = appUserId,
                Date = selectedDate
            };
            diaryRepository.AddDiary(newDiary);
            existingDiary = newDiary;
        }

        var diaryEntries = diaryRepository.GetDiaryEntriesForDate(selectedDate, appUserId);

        var totalCalories = diaryEntries.Sum(de => de.Calories);
        var totalProtein = diaryEntries.Sum(de => de.Protein);
        var totalCarbs = diaryEntries.Sum(de => de.Carbs);
        var totalFats = diaryEntries.Sum(de => de.Fats);

        var diaryViewModel = new DiaryViewModel
        {
            DiaryId = existingDiary.Id,
            SelectedDate = selectedDate,
            DiaryEntries = diaryEntries,
            TotalCalories = totalCalories,
            TotalProtein = totalProtein,
            TotalCarbs = totalCarbs,
            TotalFats = totalFats
        };

        return diaryViewModel;
    }

    [HttpGet]
    public IActionResult Index(DateTime? selectedDate)
    {
        if (selectedDate == null)
        {
            selectedDate = DateTime.Today;
        }

        var diaryViewModel = GetDiaryViewModel(selectedDate.Value);

        return View(diaryViewModel);
    }

    [HttpGet]
    public IActionResult LoadDiary(DateTime selectedDate)
    {
        var appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var existingDiary = diaryRepository.GetDiaryByDate(appUserId, selectedDate);

        if (existingDiary == null)
        {
            var newDiary = new Diary
            {
                AppUserId = appUserId,
                Date = selectedDate
            };
            diaryRepository.AddDiary(newDiary);
        }

        var diaryViewModel = GetDiaryViewModel(selectedDate);

        return PartialView("_DiaryEntriesPartial", diaryViewModel);
    }

}