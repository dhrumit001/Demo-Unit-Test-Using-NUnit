using static Demo_Unit_Test_Using_NUnit.Services.UserSessionService;

namespace Demo_Unit_Test_Using_NUnit.Services
{
    public interface IUserSessionService
    {
        void CreateSession(int id, string email);

        UserSession GetSession();

        void ClearSession();
       
    }
}
