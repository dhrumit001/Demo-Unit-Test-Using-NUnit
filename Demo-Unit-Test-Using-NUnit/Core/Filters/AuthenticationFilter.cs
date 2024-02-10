using Demo_Unit_Test_Using_NUnit.Controllers;
using Demo_Unit_Test_Using_NUnit.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Demo_Unit_Test_Using_NUnit.Core.Filters
{
    public class AuthenticationFilter : IActionFilter
    {
        private readonly IUserSessionService _userSessionService;

        public AuthenticationFilter(IUserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var currentUser = _userSessionService.GetSession();
            if (currentUser == null)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
 (new { action = "Login", controller = "Account" }));
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
