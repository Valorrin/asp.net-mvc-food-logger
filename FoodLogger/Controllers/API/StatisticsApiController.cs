using FoodLogger.Interfaces;
using FoodLogger.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodLogger.Controllers.API
{
    [Route("api/statistics")]
    [ApiController]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IStatisticsRepository statistics;

        public StatisticsApiController(IStatisticsRepository statistics)
        {
            this.statistics = statistics;
        }

        [HttpGet]
        public StatisticsRepositoryModel GetStatistics() 
        {
            return this.statistics.Total();
        }

    }
}
