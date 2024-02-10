using Demo_Unit_Test_Using_NUnit.Controllers;
using Demo_Unit_Test_Using_NUnit.Core.Domain.Users;
using Demo_Unit_Test_Using_NUnit.Data;
using Demo_Unit_Test_Using_NUnit.Models.Account;
using Demo_Unit_Test_Using_NUnit.Services;
using Demo_Unit_Test_Using_NUnit.Tests.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Unit_Test_Using_NUnit.Tests.Controllers
{
    public class AccountControllerTests
    {
        public readonly DbContextOptions<ObjectContext> dbContextOptions;

        public AccountControllerTests()
        {
            // Build DbContextOptions
            dbContextOptions = new DbContextOptionsBuilder<ObjectContext>()
                .UseInMemoryDatabase(databaseName: "MyTestDb")
                .Options;
        }

        [Test]
        public async Task Login_Test_ReturnToHomeIndex_When_Valid_Credencials()
        {
            //Arrange
            var blogContext = new ObjectContext(dbContextOptions);
            var userRepo = new EfRepository<User>(blogContext);
            userRepo.Insert(new User() { Email = "dhrumit.patel@ics-global.in", Password = "Test@124", Active = true });

            var userService = new UserService(userRepo);
            var controller = new AccountController(userService, new MockUserSessionService(), userRepo);


            //Act
            var result = await controller.Login(new LoginModel
            {
                Email = "dhrumit.patel@ics-global.in",
                Password = "Test@124"
            }) as RedirectToActionResult;

            //Assert

            Assert.Multiple(() =>
            {
                Assert.That(result.ActionName, Is.EqualTo(nameof(HomeController.Index)));
                Assert.That(result.ControllerName, Is.EqualTo("Home"));
            });
        }

        [TestCase("", "Test@124")]
        [TestCase("test@gmail.com", "")]
        [TestCase("test@gmail.com", "dd")]
        public async Task Login_Test_ReturnLoginView_When_Invalid_ModelState(string email, string password)
        {
            //Arrange
            var blogContext = new ObjectContext(dbContextOptions);
            var userRepo = new EfRepository<User>(blogContext);
            userRepo.Insert(new User() { Email = "dhrumit.patel@ics-global.in", Password = "Test@124", Active = true });

            var userService = new UserService(userRepo);
            var controller = new AccountController(userService, new MockUserSessionService(), userRepo);

            //Act
            var result = await controller.Login(new LoginModel
            {
                Email = email,
                Password = password
            }) as ViewResult;

            //Assert
            Assert.That(controller.ModelState.IsValid, Is.EqualTo(false), "Valid Model State");
            Assert.That(result.ViewName, Is.Null);
        }
    }
}
