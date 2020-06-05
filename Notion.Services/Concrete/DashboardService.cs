using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notion.Comman.Dtos;
using Notion.DAL.Context;
using Notion.Services.Abstract;
using Notion.Services.Helper;

namespace Notion.Services.Concrete
{
    public class DashboardService : IDashboardService
    {
        private readonly AppDataContext _context;

        public DashboardService(AppDataContext context)
        {
            _context = context;
        }

        public async Task<List<TotalRequestDto>> GetTotalRequestCard()
        {
            var requests = await  _context.RequestStreams.GroupBy(x => x.MethodType).Select(g => new TotalRequestDto
            {
                Method = g.Key,
                Count = g.Count()
            }).ToListAsync();

            return requests;
        }

        public async Task<List<WeeklyRequest>> GetWeeklyRequests()
        {
            const string methodType = "OPTIONS";
            var lastWeek = DateTime.UtcNow.AddDays(-7);
            var weeklyRequest = await _context.RequestStreams
                .OrderByDescending(x => x.CreatedAtUTC)
                .Where(x => x.CreatedAtUTC >= lastWeek && x.MethodType != methodType)
                .GroupBy(g => g.CreatedAtUTC.Date)
                .Select(x => new WeeklyRequest
                {
                    WeekDay = x.Key,
                    Count = x.Count()
                }).ToListAsync();

            return weeklyRequest;
        }
    }
}