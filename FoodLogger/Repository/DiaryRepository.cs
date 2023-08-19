using FoodLogger.Data;
using FoodLogger.Data.Models;
using FoodLogger.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodLogger.Repository
{
    public class DiaryRepository : IDiaryRepository
    {
        private readonly ApplicationDbContext context;

        public DiaryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

   

        public bool AddDiary(Diary diary)
        {
            context.Diaries.Add(diary);
            return Save();
        }

        public Diary GetDiaryByDate(string userId, DateTime date)
        {
            return context.Diaries.FirstOrDefault(d => d.AppUserId == userId && d.Date == date);
        }
 
        public int GetDiaryId(string appUserId, DateTime date)
        {
            return context.Diaries.FirstOrDefault(d => d.AppUserId == appUserId && d.Date == date).Id;
        }


        public bool AddDiaryEntry(DiaryEntry diaryEntry)
        {
            context.DiaryEntries.Add(diaryEntry);
            return Save();
        }

        public bool Update(DiaryEntry entry)
        {
            context.DiaryEntries.Update(entry);
            return Save();
        }

        public bool DeleteEntry(DiaryEntry entry)
        {
            context.DiaryEntries.Remove(entry);
            return Save();
        }

        public DiaryEntry GetDiaryEntryById(int id)
        {
            return context.DiaryEntries.FirstOrDefault(d => d.Id == id);
        }

        public List<DiaryEntry> GetAllEntriesByFoodId(int id)
        {
            var diaryEntries = context.DiaryEntries
                .Include(d => d.Food)
                .Where(f=>f.FoodId == id)
                .ToList();

            return diaryEntries;
        }

        public List<DiaryEntry> GetAllEntriesByRecipeId(int id)
        {
            var diaryEntries = context.DiaryEntries
                .Include(d => d.Recipe)
                .Where(f => f.RecipeId == id)
                .ToList();

            return diaryEntries;
        }

        public List<DiaryEntry> GetDiaryEntriesForDate(DateTime? date, string userId)
        {
            var diaryEntries = context.DiaryEntries
                .Include(de => de.Food)
                .Include(de => de.Recipe)
                    .ThenInclude(r => r.Foods)
                .Where(de => de.Diary.Date == date && de.Diary.AppUserId == userId)
                .ToList();

            return diaryEntries;
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
