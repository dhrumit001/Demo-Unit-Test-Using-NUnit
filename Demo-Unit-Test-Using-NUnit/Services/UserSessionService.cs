using System.Text.Json;
using System.Text.Json.Serialization;

namespace Demo_Unit_Test_Using_NUnit.Services
{
    public class UserSessionService : IUserSessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserSessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void CreateSession(int id, string email)
        {
            _httpContextAccessor.HttpContext
                .Session.SetString("UserData", JsonSerializer.Serialize(new UserSession
                {
                    Id = id,
                    Email = email
                }));
        }

        public UserSession GetSession()
        {
            var userData = _httpContextAccessor.HttpContext.Session.GetString("UserData");
            if (string.IsNullOrWhiteSpace(userData))
                return null;

            return JsonSerializer.Deserialize<UserSession>(userData);

        }

        public void ClearSession()
        {
            _httpContextAccessor.HttpContext.Session.Remove("UserData");
        }

        public class UserSession
        {
            public int Id { get; set; }
            public string Email { get; set; }
        }

    }
}
