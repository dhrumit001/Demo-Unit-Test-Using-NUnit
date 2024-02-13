using Demo_Unit_Test_Using_NUnit.Core.Domain.Users;
using Demo_Unit_Test_Using_NUnit.Core.Filters;
using Demo_Unit_Test_Using_NUnit.Data;
using Demo_Unit_Test_Using_NUnit.Models;
using Demo_Unit_Test_Using_NUnit.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;

namespace Demo_Unit_Test_Using_NUnit.Controllers
{
    [ServiceFilter(typeof(AuthenticationFilter))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepository<User> _userRepository;
        private readonly IUserSessionService _userSessionService;
        private readonly TaskService _taskService;

        public HomeController(ILogger<HomeController> logger,
            IRepository<User> userRepository,
            IUserSessionService userSessionService,
            UserService userService,
            TaskService taskService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _userSessionService = userSessionService;
            _taskService = taskService;
        }

        public async Task<IActionResult> Index()
        {
            var todayTasks = await _taskService.GetAllTasks(_userSessionService.GetSession().Id, DateTime.Now);
            return View(todayTasks);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}