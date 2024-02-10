using System.Globalization;

namespace Demo_Unit_Test_Using_NUnit.Core.Domain.Tasks
{
    public class Task : BaseEntity
    {
        public string Title { get; set; }

        public string? Description { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }
    }
}
