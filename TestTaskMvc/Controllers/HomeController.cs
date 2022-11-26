using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TestTaskMvc.Data;
using TestTaskMvc.Models;
using TestTaskMvc.Models.ViewModel;
using TestTaskMvc.Services.Interface;

namespace TestTaskMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IOrderFilterService _filterService;
        public HomeController(ApplicationDbContext context, IOrderFilterService filterService)
        {
            _context = context;
            _filterService = filterService;
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                Order = _context.Order.Include(o => o.Provider),
                OrderItem = _context.OrderItem,
                FilterSelectList = _filterService.GetFilterSelectListView()
            };

            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Filter(RequestFilter request)
        {
            var result = _filterService.Filter(request);
            ViewData["MessageFilter"] = "Фильтры применены";
            return View(nameof(Index), result);
        }
    }
}   