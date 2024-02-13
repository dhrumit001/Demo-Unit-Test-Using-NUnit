using Demo_Unit_Test_Using_NUnit.Core.Domain.Users;
using Demo_Unit_Test_Using_NUnit.Data;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Demo_Unit_Test_Using_NUnit.Services
{
    public class UserService
    {
        private readonly IRepository<User> _userRepository;
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> ValidateUser(string email, string password)
        {
            var user = await _userRepository.Table
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user is null || user.Active is not true)
                return false;

            var isValidPassword = user.Password.Equals(password);
            return isValidPassword;
        }

        public async Task CreateUser(string email, string password)
        {
            var user = await _userRepository.Table
                .FirstOrDefaultAsync(u => u.Email == email);

            if (user is not null)
                throw new Exception("User already Exists");

            _userRepository.Insert(new User
            {
                Email = email,
                Password = password,
                Active = true
            });
        }
    }
}
