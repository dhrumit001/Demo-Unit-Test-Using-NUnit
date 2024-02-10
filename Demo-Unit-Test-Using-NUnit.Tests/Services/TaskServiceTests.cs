using Demo_Unit_Test_Using_NUnit.Core.Domain.Tasks;
using Demo_Unit_Test_Using_NUnit.Core.Domain.Users;
using Demo_Unit_Test_Using_NUnit.Data;
using Demo_Unit_Test_Using_NUnit.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Demo_Unit_Test_Using_NUnit.Tests.Services
{
    public class TaskServiceTests
    {
        public readonly DbContextOptions<ObjectContext> dbContextOptions;

        public TaskServiceTests()
        {
            // Build DbContextOptions
            dbContextOptions = new DbContextOptionsBuilder<ObjectContext>()
                .UseInMemoryDatabase(databaseName: "MyTestDb")
                .Options;
        }

        #region Test CreateTask

        [Test]
        public void CreateTask_Test_With_Past_TaskDate()
        {
            //Arrange
            var blogContext = new ObjectContext(dbContextOptions);
            var taskRepo = new EfRepository<Core.Domain.Tasks.Task>(blogContext);
            var service = new TaskService(taskRepo);
            var task = new Core.Domain.Tasks.Task { Title = "Demo on monday", Date = DateTime.Now, UserId = 1 };

            //Act Asset
            Assert.Throws(Is.TypeOf<Exception>().And.Message.EqualTo("Can not create task with past date"), () => service.CreateTask(task));
        }

        #endregion

        #region Test CreateTask

        [TestCase(1, null, ExpectedResult = 2)]
        [TestCase(2, null, ExpectedResult = 1)]
        [TestCase(2, "02/09/2024", ExpectedResult = 0)]
        [TestCase(2, "02/10/2024", ExpectedResult = 1)]
        public async Task<int> GetAllTasks_Test_With_UserId(int userId, DateTime? date = null)
        {
            //Arrange
            var blogContext = new ObjectContext(dbContextOptions);
            var taskRepo = new EfRepository<Core.Domain.Tasks.Task>(blogContext);
            var service = new TaskService(taskRepo);

            var tasks = taskRepo.Table.ToList();
            taskRepo.Delete(tasks);
            taskRepo.Insert(new Core.Domain.Tasks.Task { UserId = 1, Title = "Test", Description = "Desc", Date = DateTime.Now.AddDays(1) });
            taskRepo.Insert(new Core.Domain.Tasks.Task { UserId = 1, Title = "Test1", Description = "Desc", Date = DateTime.Now.AddDays(1) });
            taskRepo.Insert(new Core.Domain.Tasks.Task { UserId = 2, Title = "Test1", Description = "Desc", Date = DateTime.Now.AddDays(1) });

            //Act
            var result = await service.GetAllTasks(userId, date);

            //Act Asset
            return result.Count;
        }

        #endregion
    }
}
