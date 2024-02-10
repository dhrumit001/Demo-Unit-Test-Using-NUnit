using System.ComponentModel.DataAnnotations;

namespace Demo_Unit_Test_Using_NUnit.Models.Task
{
    public class TaskModel
    {
        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
