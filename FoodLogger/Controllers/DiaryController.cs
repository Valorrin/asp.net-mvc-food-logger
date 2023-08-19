using FoodLogger.Data.Models;
using FoodLogger.Interfaces;
using FoodLogger.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Authorize]
public class DiaryController : Controller
{
    private readonly IDiaryService diaryService;

    public DiaryController(IDiaryService diaryService)
    {
        this.diaryService = diaryService;
    }

    [HttpGet]
    public IActionResult Index(DateTime? selectedDate)
    {
        if (selectedDate == null)
        {
            selectedDate = DateTime.Today;
        }

        var appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var diaryViewModel = diaryService.GetDiaryViewModel(selectedDate.Value, appUserId);

        return View(diaryViewModel);
    }

    [HttpGet]
    public IActionResult LoadDiary(DateTime selectedDate)
    {
        var appUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var diaryViewModel = diaryService.GetDiaryViewModel(selectedDate, appUserId);

        return PartialView("_DiaryEntriesPartial", diaryViewModel);
    }

}