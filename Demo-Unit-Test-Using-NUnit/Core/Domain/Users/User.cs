namespace Demo_Unit_Test_Using_NUnit.Core.Domain.Users
{
    public class User : BaseEntity
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool Active { get; set; }
    }
}
