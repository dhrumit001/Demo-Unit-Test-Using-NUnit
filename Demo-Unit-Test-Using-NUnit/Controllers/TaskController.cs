using Demo_Unit_Test_Using_NUnit.Core.Domain.Tasks;
using Demo_Unit_Test_Using_NUnit.Core.Domain.Users;
using Demo_Unit_Test_Using_NUnit.Core.Filters;
using Demo_Unit_Test_Using_NUnit.Data;
using Demo_Unit_Test_Using_NUnit.Models.Task;
using Demo_Unit_Test_Using_NUnit.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo_Unit_Test_Using_NUnit.Controllers
{
    [ServiceFilter(typeof(AuthenticationFilter))]
    public class TaskController : Controller
    {
        private readonly TaskService _taskService;
        private readonly IRepository<Core.Domain.Tasks.Task> _taskRepository;
        private readonly IUserSessionService _userSessionService;

        public TaskController(
            IRepository<Core.Domain.Tasks.Task> taskRepository,
            TaskService taskService,
            IUserSessionService userSessionService)
        {
            _taskRepository = taskRepository;
            _taskService = taskService;
            _userSessionService = userSessionService;
        }

        public async Task<IActionResult> Index()
        {
            var loginUser = _userSessionService.GetSession();
            var todayTasks = await _taskService.GetAllTasks(loginUser.Id);

            return View(todayTasks);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TaskModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _taskService.CreateTask(new Core.Domain.Tasks.Task
                    {
                        Title = model.Title,
                        UserId = _userSessionService.GetSession().Id,
                        Description = model.Description,
                        Date = model.Date,
                    });

                    return RedirectToAction(nameof(Index));
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(nameof(model.Date), ex.Message);
                return View(model);
            }
        }

        public IActionResult Delete(int Id)
        {
            var task = _taskRepository.GetById(Id);
            if (task is null)
                return NotFound();

            _taskRepository.Delete(task);

            return RedirectToAction(nameof(Index));
        }

    }
}
