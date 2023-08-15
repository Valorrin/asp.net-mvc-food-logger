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

        public List<DiaryEntry> GetDiaryEntriesForDate(DateTime date)
        {
            var diaryEntries = context.DiaryEntries
            .Include(de => de.Recipe)
            .Include(de => de.Food)
            .Where(de => de.Diary.Date == date)
            .ToList();

            return diaryEntries;
        }
    }
}
