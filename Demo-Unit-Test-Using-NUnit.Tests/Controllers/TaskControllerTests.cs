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
        private DbContextOptions<ObjectContext> dbContextOptions;
        private IRepository<TaskDetail> _taskRepository;

        [SetUp]
        public void Setup()
        {
            // Build DbContextOptions
            dbContextOptions = new DbContextOptionsBuilder<ObjectContext>()
                .UseInMemoryDatabase(databaseName: "MyTestDb")
                .Options;

            var blogContext = new ObjectContext(dbContextOptions);
            _taskRepository = new EfRepository<TaskDetail>(blogContext);

            //Remove all records 
            _taskRepository.Delete(_taskRepository.Table.ToList());
        }

        [Test]
        public async Task Index_Test_ReturnToHomeIndex_ReturnOnlyLoginUserRecords()
        {
            //Arrange
            _taskRepository.Insert(new TaskDetail { UserId = 1, Title = "Test", Description = "Desc", Date = DateTime.Now.AddDays(1) });
            _taskRepository.Insert(new TaskDetail { UserId = 1, Title = "Test1", Description = "Desc", Date = DateTime.Now.AddDays(1) });
            _taskRepository.Insert(new TaskDetail { UserId = 2, Title = "Test1", Description = "Desc", Date = DateTime.Now.AddDays(1) });
            var userSessionService = new MockUserSessionService();
            userSessionService.CreateSession(1, "test@gmail.com");
            var taskService = new TaskService(_taskRepository);
            var controller = new TaskController(_taskRepository, taskService, userSessionService);

            //Act
            var result = await controller.Index();

            //Assert
            var count = ((result as ViewResult).Model as List<TaskDetail>).Count;
            Assert.That(count, Is.EqualTo(2));
        }

        [TestCase("test", "12/5/2026")]
        public void Create_Test_ReturnToHomeIndex_WhenModelStateIsValid(string title, string date)
        {
            //Arrange
            var userSessionService = new MockUserSessionService();
            userSessionService.CreateSession(1, "test@gmail.com");
            var taskService = new TaskService(_taskRepository);
            var controller = new TaskController(_taskRepository, taskService, userSessionService);
            DateTime.TryParse(date, out DateTime dateTime);

            //Act
            var result = controller.Create(new TaskModel
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
            _taskRepository.Insert(new TaskDetail { Id = 1, Title = "Test", Description = "Desc", Date = DateTime.Now.AddDays(1) });
            var taskService = new TaskService(_taskRepository);
            var userSessionService = new MockUserSessionService();
            userSessionService.CreateSession(1, "test@gmail.com");
            var controller = new TaskController(_taskRepository, taskService, userSessionService);

            //Act
            var result = controller.Delete(id);

            //Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

    }
}
