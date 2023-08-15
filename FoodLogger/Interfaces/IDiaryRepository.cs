using FoodLogger.Data.Models;

namespace FoodLogger.Interfaces
{
    public interface IDiaryRepository
    {
        List<DiaryEntry> GetDiaryEntriesForDate(DateTime date);
    }
}
