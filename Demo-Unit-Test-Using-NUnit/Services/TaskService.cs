using Demo_Unit_Test_Using_NUnit.Core.Domain.Tasks;
using Demo_Unit_Test_Using_NUnit.Core.Domain.Users;
using Demo_Unit_Test_Using_NUnit.Data;
using Microsoft.EntityFrameworkCore;

namespace Demo_Unit_Test_Using_NUnit.Services
{
    public class TaskService
    {
        private readonly IRepository<Core.Domain.Tasks.Task> _taskRepository;
        public TaskService(IRepository<Core.Domain.Tasks.Task> taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public void CreateTask(Core.Domain.Tasks.Task task)
        {
            if (task.Date < DateTime.Now)
                throw new Exception("Can not create task with past date");

            _taskRepository.Insert(task);
        }

        public async Task<List<Core.Domain.Tasks.Task>> GetAllTasks(int userId, DateTime? taskDate = null)
        {
            var tasks = _taskRepository.Table
                .Where(u => u.UserId == userId);

            if (taskDate is not null)
                tasks = tasks
                    .Where(t => t.Date.Date == taskDate.Value.Date);

            return await tasks.ToListAsync();
        }
    }
}
