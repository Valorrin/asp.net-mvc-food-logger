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

        public List<DiaryEntry> GetDiaryEntriesForDate(DateTime? date)
        {
            var diaryEntries = context.DiaryEntries
                .Include(de => de.Food)
                .Include(de => de.Recipe)
                    .ThenInclude(r => r.Foods)
                .Where(de => de.Diary.Date == date)
                .ToList();

            return diaryEntries;
        }

        public bool AddDiary(Diary diary)
        {
            context.Diaries.Add(diary);
            return Save();
        }

        public bool AddDiaryEntry(DiaryEntry diaryEntry) 
        {
            context.DiaryEntries.Add(diaryEntry);
            return Save();
        }

        public Diary GetDiaryByDate(string userId, DateTime date)
        {
            return context.Diaries
                .FirstOrDefault(d => d.AppUserId == userId && d.Date == date);
        }
        public int GetDiaryId(string appUserId, DateTime date)
        {
            return context.Diaries.FirstOrDefault(d => d.AppUserId == appUserId && d.Date == date).Id;
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}
