using FoodLogger.Data.Models;
using FoodLogger.Interfaces;
using FoodLogger.Models;
using FoodLogger.Repository;
using System.Security.Claims;

namespace FoodLogger.Services
{
    public class DiaryService : IDiaryService
    {
        private readonly IDiaryRepository diaryRepository;

        public DiaryService(IDiaryRepository diaryRepository)
        {
            this.diaryRepository = diaryRepository;
        }
        public DiaryViewModel GetDiaryViewModel(DateTime selectedDate, string appUserId)
        {
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
    }
}
