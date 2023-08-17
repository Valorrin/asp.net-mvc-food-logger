using FoodLogger.Data.Models;
using FoodLogger.Interfaces;
using FoodLogger.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

        var diaryEntries = diaryRepository.GetDiaryEntriesForDate(selectedDate);

        var totalCalories = diaryEntries.Sum(de => de.Food?.Calories ?? de.Recipe?.CalculateCalories ?? 0);
        var totalProtein = diaryEntries.Sum(de => de.Food?.Protein ?? de.Recipe?.CalculateProtein ?? 0);
        var totalCarbs = diaryEntries.Sum(de => de.Food?.Carbs ?? de.Recipe?.CalculateCarbs ?? 0);
        var totalFats = diaryEntries.Sum(de => de.Food?.Fat ?? de.Recipe?.CalculateFats ?? 0);

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
    public IActionResult LoadDiary(DateTime? selectedDate)
    {
        if (selectedDate == null)
        {
            selectedDate = DateTime.Today;
        }

        var diaryViewModel = GetDiaryViewModel(selectedDate.Value);

        return PartialView("_DiaryEntriesPartial", diaryViewModel);
    }
}