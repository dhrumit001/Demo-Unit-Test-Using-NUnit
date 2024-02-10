using Demo_Unit_Test_Using_NUnit.Core.Domain.Users;
using Demo_Unit_Test_Using_NUnit.Data;
using Demo_Unit_Test_Using_NUnit.Models.Account;
using Demo_Unit_Test_Using_NUnit.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo_Unit_Test_Using_NUnit.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;
        private readonly IUserSessionService _userSessionService;
        private readonly IRepository<User> _userRepository;

        public AccountController(UserService userService,
            IUserSessionService userSessionService,
            IRepository<User> userRepository)
        {
            _userService = userService;
            _userSessionService = userSessionService;
            _userRepository = userRepository;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ValidateUser(loginModel.Email, loginModel.Password);
                if (result)
                {
                    var user = await _userRepository.Table.
                        FirstOrDefaultAsync(e => e.Email == loginModel.Email);

                    _userSessionService.CreateSession(user.Id, user.Email);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(nameof(loginModel.Password), "Invalid Credencials.");
            }

            return View(loginModel);
        }

        public IActionResult Logout()
        {
            _userSessionService.ClearSession();
            return RedirectToAction("Index", "Home");
        }
    }
}
