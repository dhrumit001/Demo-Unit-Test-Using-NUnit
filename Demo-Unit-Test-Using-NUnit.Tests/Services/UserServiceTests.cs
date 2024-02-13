using Demo_Unit_Test_Using_NUnit.Core.Domain.Users;
using Demo_Unit_Test_Using_NUnit.Data;
using Demo_Unit_Test_Using_NUnit.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Unit_Test_Using_NUnit.Tests.Services
{
    public class UserServiceTests
    {
        public readonly DbContextOptions<ObjectContext> dbContextOptions;

        public UserServiceTests()
        {
            // Build DbContextOptions
            dbContextOptions = new DbContextOptionsBuilder<ObjectContext>()
                .UseInMemoryDatabase(databaseName: "MyTestDb")
                .Options;
        }

        #region Test ValidateUser

        [TestCase("dhrumit.patel@ics-global.in", "Test@124")]
        public async Task ValidateUser_Test_Valid_User(string email, string password)
        {
            //Arrange
            var blogContext = new ObjectContext(dbContextOptions);
            var userRepo = new EfRepository<User>(blogContext);
            userRepo.Insert(new User() { Email = "dhrumit.patel@ics-global.in", Password = "Test@124", Active = true });
            userRepo.Insert(new User() { Email = "test.user@ics-global.in", Password = "Test@124", Active = true });
            var service = new UserService(userRepo);

            //Act
            var result = await service.ValidateUser(email, password);

            //Asset
            Assert.That(result, Is.EqualTo(true));

        }

        [TestCase("dhrumit.patel@ics-global.in", "Test@125")]
        [TestCase("test.user@ics-global.in", "Test@124")]
        public async Task ValidateUser_Test_Invalid_User(string email, string password)
        {
            //Arrange
            var blogContext = new ObjectContext(dbContextOptions);
            var userRepo = new EfRepository<User>(blogContext);
            userRepo.Insert(new User() { Email = "dhrumit.patel@ics-global.in", Password = "Test@124", Active = true });
            userRepo.Insert(new User() { Email = "test.user@ics-global.in", Password = "Test@124", Active = false });

            var service = new UserService(userRepo);

            //Act
            var result = await service.ValidateUser(email, password);

            //Asset
            Assert.That(result, Is.EqualTo(false));

        }

        #endregion

        #region Test CreateUser

        [TestCase("dhrumit.patel277@ics-global.in", "Test@124")]
        public async Task CreateUser_Test_With_Not_Exist_User(string email, string password)
        {
            //Arrange
            var blogContext = new ObjectContext(dbContextOptions);
            var userRepo = new EfRepository<User>(blogContext);
            var service = new UserService(userRepo);

            //Act
            await service.CreateUser(email, password);

            var user = await userRepo.Table
                .FirstOrDefaultAsync(u => u.Email == email);

            //Asset
            Assert.That(user.Email, Is.EqualTo(email));
        }

        [TestCase("dhrumit.patel@ics-global.in", "Test@124")]
        public void CreateUser_Test_With_Exist_User(string email, string password)
        {
            //Arrange
            var blogContext = new ObjectContext(dbContextOptions);
            var userRepo = new EfRepository<User>(blogContext);
            userRepo.Insert(new User() { Email = "dhrumit.patel@ics-global.in", Password = "Test@124", Active = true });

            var service = new UserService(userRepo);

            //Act Asset
            Assert.ThrowsAsync(Is.TypeOf<Exception>().And.Message.EqualTo("User already Exists"), () => service.CreateUser(email, password));
        }

        #endregion
    }
}
