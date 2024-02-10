using Demo_Unit_Test_Using_NUnit.Controllers;
using Demo_Unit_Test_Using_NUnit.Core.Domain.Tasks;
using Demo_Unit_Test_Using_NUnit.Data;
using Demo_Unit_Test_Using_NUnit.Models.Task;
using Demo_Unit_Test_Using_NUnit.Services;
using Demo_Unit_Test_Using_NUnit.Tests.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Unit_Test_Using_NUnit.Tests.Controllers
{
    public class TaskControllerTests
    {
        public readonly DbContextOptions<ObjectContext> dbContextOptions;

        public TaskControllerTests()
        {
            // Build DbContextOptions
            dbContextOptions = new DbContextOptionsBuilder<ObjectContext>()
                .UseInMemoryDatabase(databaseName: "MyTestDb")
                .Options;
        }

        [Test]
        public async System.Threading.Tasks.Task Index_Test_ReturnToHomeIndex_ReturnOnlyLoginUserRecords()
        {
            //Arrange
            var blogContext = new ObjectContext(dbContextOptions);
            var taskRepo = new EfRepository<Core.Domain.Tasks.Task>(blogContext);
            taskRepo.Insert(new Core.Domain.Tasks.Task { UserId = 1, Title = "Test", Description = "Desc", Date = DateTime.Now.AddDays(1) });
            taskRepo.Insert(new Core.Domain.Tasks.Task { UserId = 1, Title = "Test1", Description = "Desc", Date = DateTime.Now.AddDays(1) });
            taskRepo.Insert(new Core.Domain.Tasks.Task { UserId = 2, Title = "Test1", Description = "Desc", Date = DateTime.Now.AddDays(1) });
            var userSessionService = new MockUserSessionService();
            userSessionService.CreateSession(1, "test@gmail.com");
            var taskService = new TaskService(taskRepo);
            var controller = new TaskController(taskRepo, taskService, userSessionService);

            //Act
            var result = await controller.Index();

            //Assert
            var count = (((result as ViewResult).Model) as List<Core.Domain.Tasks.Task>).Count;
            Assert.That(count, Is.EqualTo(2));
        }

        [TestCase("test", "12/5/2026")]
        public void Create_Test_ReturnToHomeIndex_WhenModelStateIsValid(string title, string date)
        {
            //Arrange
            var blogContext = new ObjectContext(dbContextOptions);
            var taskRepo = new EfRepository<Core.Domain.Tasks.Task>(blogContext);
            var userSessionService = new MockUserSessionService();
            userSessionService.CreateSession(1, "test@gmail.com");
            var taskService = new TaskService(taskRepo);
            var controller = new TaskController(taskRepo, taskService, userSessionService);
            DateTime.TryParse(date, out DateTime dateTime);

            //Act
            var result = controller.Create(new Models.Task.TaskModel
            {
                Title = title,
                Date = dateTime
            }) as RedirectToActionResult;

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.ActionName, Is.EqualTo(nameof(HomeController.Index)));
                Assert.That(result.ControllerName, Is.EqualTo(null));
            });
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(4)]
        public void Delete_Test_ReturnBadRequest_WhenIdNotFound(int id)
        {
            //Arrange
            var blogContext = new ObjectContext(dbContextOptions);
            var taskRepo = new EfRepository<Core.Domain.Tasks.Task>(blogContext);

            var task = taskRepo.GetById(1);
            if (task is not null)
            {
                taskRepo.Delete(task);
            }
            taskRepo.Insert(new Core.Domain.Tasks.Task { Id = 1, Title = "Test", Description = "Desc", Date = DateTime.Now.AddDays(1) });
            var taskService = new TaskService(taskRepo);
            var userSessionService = new MockUserSessionService();
            userSessionService.CreateSession(1, "test@gmail.com");
            var controller = new TaskController(taskRepo, taskService, userSessionService);

            //Act
            var result = controller.Delete(id);

            //Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

    }
}
