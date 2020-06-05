using System.Collections.Generic;
using System.Threading.Tasks;
using Notion.Comman.Dtos;

namespace Notion.Services.Abstract
{
    public interface IDashboardService
    {
        public Task<List<TotalRequestDto>> GetTotalRequestCard();
        public Task<List<WeeklyRequest>> GetWeeklyRequests();
    }
}