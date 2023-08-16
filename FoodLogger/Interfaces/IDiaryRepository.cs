using FoodLogger.Data.Models;

namespace FoodLogger.Interfaces
{
    public interface IDiaryRepository
    {
        List<DiaryEntry> GetDiaryEntriesForDate(DateTime? date);
        public bool AddDiary(Diary diary);
        public bool AddDiaryEntry(DiaryEntry diaryEntry);

        public int GetDiaryId(string userId, DateTime date);
        Diary GetDiaryByDate(string userId, DateTime date);
    }
}
