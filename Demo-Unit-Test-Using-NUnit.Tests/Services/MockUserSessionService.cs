using Demo_Unit_Test_Using_NUnit.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Demo_Unit_Test_Using_NUnit.Services.UserSessionService;

namespace Demo_Unit_Test_Using_NUnit.Tests.Services
{
    public class MockUserSessionService : IUserSessionService
    {
        private UserSession? _userSession;

        public void CreateSession(int id, string email)
        {
            _userSession = new UserSession { Id = id, Email = email };
        }

        public UserSession GetSession()
        {
            return _userSession;
        }

        public void ClearSession()
        {
            _userSession = null;
        }
    }
}
