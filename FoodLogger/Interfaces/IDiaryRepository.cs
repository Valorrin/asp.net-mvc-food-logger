using FoodLogger.Data.Models;

namespace FoodLogger.Interfaces
{
    public interface IDiaryRepository
    {
        public bool AddDiary(Diary diary);

        Diary GetDiaryByDate(string userId, DateTime date);

        public int GetDiaryId(string userId, DateTime date);



        public bool AddDiaryEntry(DiaryEntry diaryEntry);

        List<DiaryEntry> GetDiaryEntriesForDate(DateTime? date, string userId);

        DiaryEntry GetDiaryEntryById(int id);

        List<DiaryEntry> GetAllEntriesByFoodId(int id);

        List<DiaryEntry> GetAllEntriesByRecipeId(int id);

        public bool Update(DiaryEntry entry);

        public bool DeleteEntry(DiaryEntry entry);

    }
}
