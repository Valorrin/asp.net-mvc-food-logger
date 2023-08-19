using FoodLogger.Models;

namespace FoodLogger.Interfaces
{
    public interface IDiaryService
    {
        DiaryViewModel GetDiaryViewModel(DateTime selectedDate, string appUserId);
    }
}
