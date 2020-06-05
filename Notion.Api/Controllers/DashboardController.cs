using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Notion.Comman.Dtos;
using Notion.Services.Abstract;

namespace Notion.Api.Controllers
{
    /// <summary>
    /// Dashboard controller to handle endpoints about dashboard
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        /// <summary>
        /// Public Controller for dependency injection.
        /// </summary>
        /// <param name="dashboardService"></param>
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        /// <summary>
        /// Request to get total request type card information.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<TotalRequestDto>> GetTotalRequestCard()
        {
            return await _dashboardService.GetTotalRequestCard();
        }

        /// <summary>
        /// Returns weekly request
        /// </summary>
        /// <returns></returns>
        [HttpGet("weekly-request")]
        public async Task<List<WeeklyRequest>> GetWeeklyRequestsAsync()
        {
            return await _dashboardService.GetWeeklyRequests();
        }
    }
}